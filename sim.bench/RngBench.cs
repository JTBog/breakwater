using BenchmarkDotNet.Attributes;
using Breakwater.Sim.Core;

namespace Breakwater.Sim.Bench;

[MemoryDiagnoser]
public class RngBench
{
    private readonly Rng rng = new(1);

    [Benchmark]
    public float MillionFloats()
    {
        float sum = 0;
        for (int i = 0; i < 1_000_000; i++) sum += rng.NextFloat();
        return sum;
    }
}
