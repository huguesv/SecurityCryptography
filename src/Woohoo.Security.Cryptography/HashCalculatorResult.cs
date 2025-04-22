// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography;

using System;
using System.Collections.Generic;

/// <summary>
/// Result of a calculation.
/// </summary>
public class HashCalculatorResult
{
    /// <summary>
    /// Gets collection of checksums.
    /// </summary>
    public SortedDictionary<string, byte[]> Checksums { get; } = new SortedDictionary<string, byte[]>();

    /// <summary>
    /// Gets or sets calculation time.
    /// </summary>
    public TimeSpan CalculationTime { get; set; }
}
