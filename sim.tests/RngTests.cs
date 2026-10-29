using Breakwater.Sim.Core;

namespace Breakwater.Sim.Tests;

public class RngTests
{
    [Fact]
    public void SameSeedGivesSameSequence()
    {
        var a = new Rng(42);
        var b = new Rng(42);
        for (int i = 0; i < 1000; i++) Assert.Equal(a.NextULong(), b.NextULong());
    }

    [Fact]
    public void DifferentSeedsDiverge()
    {
        var a = new Rng(1);
        var b = new Rng(2);
        int equal = 0;
        for (int i = 0; i < 100; i++) if (a.NextULong() == b.NextULong()) equal++;
        Assert.Equal(0, equal);
    }

    [Fact]
    public void FloatsStayInUnitRangeAndAreRoughlyUniform()
    {
        var rng = new Rng(7);
        const int samples = 100_000;
        double sum = 0;
        for (int i = 0; i < samples; i++)
        {
            float f = rng.NextFloat();
            Assert.InRange(f, 0f, 0.99999999f);
            sum += f;
        }
        Assert.InRange(sum / samples, 0.49, 0.51);
    }

    [Fact]
    public void IntRangeRespectsBounds()
    {
        var rng = new Rng(9);
        for (int i = 0; i < 10_000; i++) Assert.InRange(rng.Range(-3, 5), -3, 4);
    }

    [Fact]
    public void RestoreReproducesSequence()
    {
        var rng = new Rng(123);
        rng.NextULong();
        var saved = rng.State;
        ulong expected = rng.NextULong();
        rng.Restore(saved);
        Assert.Equal(expected, rng.NextULong());
    }
}
