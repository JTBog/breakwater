namespace Breakwater.Sim.Core;

/// <summary>
/// Fixed-tick simulation clock plus the faster-running calendar (PLAN.md §5.1).
/// Physics and combat use sim time; deadlines, day/night and schedules use calendar time.
/// </summary>
public sealed class Clock
{
    public const int TicksPerSecond = 20;
    public const float TickSeconds = 1f / TicksPerSecond;

    /// <summary>Calendar seconds that pass per sim second. Tunable (PLAN.md §9 open question 1).</summary>
    public float CalendarRate { get; }

    public long Tick { get; private set; }

    /// <summary>Calendar time at tick 0, in seconds since day 0 00:00.</summary>
    public double CalendarStartSeconds { get; }

    public Clock(float calendarRate = 30f, double calendarStartSeconds = 6 * 3600)
    {
        CalendarRate = calendarRate;
        CalendarStartSeconds = calendarStartSeconds;
    }

    public double SimSeconds => Tick * (double)TickSeconds;

    public double CalendarSeconds => CalendarStartSeconds + SimSeconds * CalendarRate;

    public int Day => (int)(CalendarSeconds / 86400);

    /// <summary>Hour of day in [0, 24).</summary>
    public double HourOfDay => CalendarSeconds % 86400 / 3600;

    internal void Advance() => Tick++;

    internal void RestoreTick(long tick) => Tick = tick;
}
