using System;
using BenchmarkDotNet.Running;
using Benchmarks;
using FypProject.Repository;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<BenchmarkTest>();
            // BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            BenchmarkTest test = new BenchmarkTest();

            test.GetAppointmentList();

        }
    }
}
