// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.Benchmarks;

using System;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser(true)]
public class HashCalculatorBenchmarks
{
    private byte[] data = null!; // Initialized in Setup method
    private Stream stream = null!; // Initialized in Setup method
    private string tempFilePath = null!; // Initialized in Setup method

    [GlobalSetup]
    public void GlobalSetup()
    {
        this.data = new byte[134_217_728];
        new Random(42).NextBytes(this.data);
        this.stream = new MemoryStream(this.data);
        this.stream.Flush();

        this.tempFilePath = Path.GetTempFileName();
        File.WriteAllBytes(this.tempFilePath, this.data);
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        this.stream.Dispose();
        File.Delete(this.tempFilePath);
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _ = this.stream.Seek(0, SeekOrigin.Begin);
    }

    [Benchmark]
    public HashCalculatorResult CalculateFromStream()
    {
        var calculator = new HashCalculator();
        return calculator.Calculate(["CRC32"], this.stream, this.stream.Length);
    }

    [Benchmark]
    public HashCalculatorResult CalculateFromPath()
    {
        var calculator = new HashCalculator();
        return calculator.Calculate(["CRC32"], this.tempFilePath);
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

    [Benchmark]
    public HashCalculatorResult CalculateWithProgressFromPath()
    {
        var calculator = new HashCalculator();
        var percent = 0;
        calculator.Progress += OnProgress;
        return calculator.Calculate(["CRC32"], this.tempFilePath);

        void OnProgress(object? sender, HashCalculatorProgressEventArgs e)
        {
            percent = e.ProgressPercentage;
        }
    }
}
