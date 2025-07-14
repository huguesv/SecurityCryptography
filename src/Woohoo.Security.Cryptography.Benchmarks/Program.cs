// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.Benchmarks;

using BenchmarkDotNet.Running;

internal class Program
{
    static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<HashCalculatorBenchmarks>();
    }
}
