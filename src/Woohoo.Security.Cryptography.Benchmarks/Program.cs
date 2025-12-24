// Copyright (c) Hugues Valois. All rights reserved.
// Licensed under the MIT license. See LICENSE in the project root for license information.

namespace Woohoo.Security.Cryptography.Benchmarks;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

internal class Program
{
    static void Main(string[] args)
    {
        //var config = DefaultConfig.Instance
        //    .AddJob(Job.MediumRun
        //        .WithLaunchCount(1)
        //        .WithToolchain(InProcessEmitToolchain.Instance)); // Or InProcessNoEmitToolchain.Instance

        //_ = BenchmarkRunner.Run<HashCalculatorBenchmarks>(config);

        _ = BenchmarkRunner.Run<HashCalculatorBenchmarks>();
    }
}
