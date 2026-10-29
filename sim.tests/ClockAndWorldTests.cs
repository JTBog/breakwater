using Breakwater.Sim.Core;

namespace Breakwater.Sim.Tests;

public class ClockAndWorldTests
{
    [Fact]
    public void TwentyTicksIsOneSimSecond()
    {
        var world = new SimWorld(1);
        world.Run(Clock.TicksPerSecond);
        Assert.Equal(1.0, world.Clock.SimSeconds, 6);
    }

    [Fact]
    public void CalendarRunsAtCalendarRate()
    {
        var world = new SimWorld(1, calendarRate: 30f);
        double start = world.Clock.CalendarSeconds;
        world.Run(Clock.TicksPerSecond * 120); // two sim minutes
        Assert.Equal(3600.0, world.Clock.CalendarSeconds - start, 3); // one calendar hour
    }

    [Fact]
    public void SameSeedSameStateHash()
    {
        var a = new SimWorld(99);
        var b = new SimWorld(99);
        a.Run(500);
        b.Run(500);
        Assert.Equal(a.StateHash(), b.StateHash());
    }

    [Fact]
    public void DifferentSeedDifferentStateHash()
    {
        var a = new SimWorld(1);
        var b = new SimWorld(2);
        a.Run(10);
        b.Run(10);
        Assert.NotEqual(a.StateHash(), b.StateHash());
    }
}
