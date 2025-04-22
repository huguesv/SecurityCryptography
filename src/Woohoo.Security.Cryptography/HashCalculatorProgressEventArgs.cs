// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography;

using System;

/// <summary>
/// Calculation event data.
/// </summary>
public class HashCalculatorProgressEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HashCalculatorProgressEventArgs"/> class.
    /// </summary>
    /// <param name="filePath">File path.</param>
    /// <param name="progressPercent">Percentage of completion, from 0 to 100.</param>
    /// <param name="progressBytes">Number of bytes processed.</param>
    /// <param name="length">File length.</param>
    public HashCalculatorProgressEventArgs(string filePath, int progressPercent, long progressBytes, long length)
    {
        this.FilePath = filePath;
        this.ProgressPercentage = progressPercent;
        this.ProgressBytes = progressBytes;
        this.Length = length;
    }

    /// <summary>
    /// Gets or sets file path.
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Gets or sets percentage of completion, from 0 to 100.
    /// </summary>
    public int ProgressPercentage { get; set; }

    /// <summary>
    /// Gets or sets number of bytes processed.
    /// </summary>
    public long ProgressBytes { get; set; }

    /// <summary>
    /// Gets or sets the file length.
    /// </summary>
    public long Length { get; set; }
}
