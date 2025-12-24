// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.Benchmarks;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[MemoryDiagnoser(true)]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class HashCalculatorBenchmarks
{
    private byte[] data = null!; // Initialized in Setup method
    private Stream stream = null!; // Initialized in Setup method

    [GlobalSetup]
    public void GlobalSetup()
    {
        this.data = new byte[134_217_728];
        new Random(42).NextBytes(this.data);
        this.stream = new MemoryStream(this.data);
        this.stream.Flush();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        this.stream.Dispose();
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _ = this.stream.Seek(0, SeekOrigin.Begin);
    }

    [Benchmark]
    public async Task<HashCalculatorResult> CalculateAsyncFromStream()
    {
        return await HashCalculator.CalculateAsync(["CRC32"], this.stream, this.stream.Length, CancellationToken.None);
    }

    [Benchmark]
    public async Task<HashCalculatorResult> CalculateAsyncWithProgressFromStream()
    {
        var percent = 0;

        return await HashCalculator.CalculateAsync(["CRC32"], this.stream, this.stream.Length, OnProgress, CancellationToken.None);

        void OnProgress(HashCalculatorProgress e)
        {
            percent = e.ProgressPercentage;
        }
    }

    [Benchmark]
    public HashCalculatorResult CalculateFromStream()
    {
        var calculator = new HashCalculator();
        return calculator.Calculate(["CRC32"], this.stream, this.stream.Length);
    }

    [Benchmark]
    public HashCalculatorResult CalculateWithProgressFromStream()
    {
        var calculator = new HashCalculator();
        var percent = 0;
        calculator.Progress += OnProgress;
        return calculator.Calculate(["CRC32"], this.stream, this.stream.Length);

        void OnProgress(object? sender, HashCalculatorProgressEventArgs e)
        {
            percent = e.ProgressPercentage;
        }
    }
}
