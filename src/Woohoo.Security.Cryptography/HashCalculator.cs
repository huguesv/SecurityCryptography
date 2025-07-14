// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Hash calculator.
/// </summary>
public class HashCalculator
{
    private readonly object cancelLock = new();
    private bool cancel;

    /// <summary>
    /// Calculation progress event.
    /// </summary>
    public event EventHandler<HashCalculatorProgressEventArgs>? Progress;

    /// <summary>
    /// Convert byte data to an hexadecimal string.
    /// </summary>
    /// <param name="data">Byte data.</param>
    /// <returns>Hexadecimal string.</returns>
    public static string HexToString(byte[] data)
    {
        Requires.NotNull(data);

        return BitConverter.ToString(data).Replace("-", string.Empty).ToLowerInvariant();
    }

    /// <summary>
    /// Request cancellation of the calculation in progress.
    /// </summary>
    public void Cancel()
    {
        lock (this.cancelLock)
        {
            this.cancel = true;
        }
    }

    /// <summary>
    /// Calculate checksums for the specified file path.
    /// </summary>
    /// <param name="hashNames">Checksum algorithm names.</param>
    /// <param name="path">File path to calculate checksums.</param>
    /// <returns>Calculation result.</returns>
    public HashCalculatorResult Calculate(string[] hashNames, string path)
    {
        Requires.NotNull(hashNames);
        Requires.NotNullOrEmpty(path);

        var info = new FileInfo(path);

        using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            return this.Calculate(hashNames, stream, info.Length, path);
        }
    }

    /// <summary>
    /// Calculate checksums for the specified file path.
    /// </summary>
    /// <param name="hashNames">Checksum algorithm names.</param>
    /// <param name="stream">Stream to calculate checksums.</param>
    /// <param name="length">Length of stream.</param>
    /// <returns>Calculation result.</returns>
    public HashCalculatorResult Calculate(string[] hashNames, Stream stream, long length)
    {
        Requires.NotNull(hashNames);
        Requires.NotNull(stream);

        return this.Calculate(hashNames, stream, length, string.Empty);
    }

    private HashCalculatorResult Calculate(string[] hashNames, Stream stream, long length, string filePath)
    {
        lock (this.cancelLock)
        {
            this.cancel = false;
        }

        var algorithms = new List<HashAlgorithm>();
        var algorithmNames = new Dictionary<HashAlgorithm, string>();

        foreach (var hashName in hashNames)
        {
            var algo = HashAlgorithmFactory.Create(hashName);
            algorithms.Add(algo);
            algorithmNames.Add(algo, hashName);
        }

        try
        {
            // 512 KB achieves a good balance of speed/memory usage
            const int BlockLength = 524288;
            var remain = length;
            var count = remain < BlockLength ? (int)remain : BlockLength;
            long offset = 0;
            var progress = 0;
            var startTime = DateTime.Now;
            var data = new byte[BlockLength];

            this.Progress?.Invoke(this, new HashCalculatorProgressEventArgs(filePath, 0, 0, length));

            while (count > 0)
            {
                lock (this.cancelLock)
                {
                    if (this.cancel)
                    {
                        return new HashCalculatorResult();
                    }
                }

                var read = stream.Read(data, 0, count);
                if (read == count)
                {
                    _ = Parallel.ForEach(algorithms, algo => algo.TransformBlock(data, 0, count, null, 0));

                    offset += count;
                    remain = length - offset;
                    count = remain < BlockLength ? (int)remain : BlockLength;

                    var currentProgress = (int)((double)offset * 100 / length);
                    if (progress != currentProgress)
                    {
                        progress = currentProgress;
                        this.Progress?.Invoke(this, new HashCalculatorProgressEventArgs(filePath, progress, offset, length));
                    }
                }
                else
                {
                    throw new IOException("Could not read requested bytes.");
                }
            }

            var result = new HashCalculatorResult();

            foreach (var algo in algorithms)
            {
                _ = algo.TransformFinalBlock([], 0, 0);
                result.Checksums.Add(algorithmNames[algo], (byte[])(algo.Hash?.Clone() ?? throw new NotSupportedException($"Unable to calculate hash for algorithm: {algo.GetType().Name}")));
            }

            result.CalculationTime = DateTime.Now - startTime;

            this.Progress?.Invoke(this, new HashCalculatorProgressEventArgs(filePath, 100, length, length));

            return result;
        }
        finally
        {
            foreach (var algo in algorithms)
            {
                algo.Dispose();
            }
        }
    }
}
