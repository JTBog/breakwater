using System.Diagnostics;
using Breakwater.Sim.Core;

// Headless soak runner: simulates N calendar hours as fast as possible and reports stats.
// Usage: SimRunner <scenario> <calendarHours> [seed]
string scenario = args.Length > 0 ? args[0] : "empty";
double hours = args.Length > 1 ? double.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture) : 1;
ulong seed = args.Length > 2 ? ulong.Parse(args[2]) : 1;

if (scenario != "empty")
{
    Console.Error.WriteLine($"Unknown scenario '{scenario}'. Available: empty");
    return 2;
}

var world = new SimWorld(seed);
long ticks = (long)(hours * 3600 / world.Clock.CalendarRate * Clock.TicksPerSecond);
var stopwatch = Stopwatch.StartNew();
world.Run(ticks);
stopwatch.Stop();

Console.WriteLine($"scenario={scenario} seed={seed} calendarHours={hours} ticks={ticks}");
Console.WriteLine($"wall={stopwatch.Elapsed.TotalSeconds:F2}s msPerTick={stopwatch.Elapsed.TotalMilliseconds / Math.Max(1, ticks):F4}");
Console.WriteLine($"day={world.Clock.Day} hour={world.Clock.HourOfDay:F2} stateHash={world.StateHash():X16}");
return 0;
