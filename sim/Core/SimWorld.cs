namespace Breakwater.Sim.Core;

/// <summary>
/// Root of the simulation. Owns the clock and RNG; systems are added here as phases land.
/// The view only reads from it and submits commands; it never mutates state directly.
/// </summary>
public sealed class SimWorld
{
    public ulong Seed { get; }
    public Clock Clock { get; }
    public Rng Rng { get; }

    public SimWorld(ulong seed, float calendarRate = 30f)
    {
        Seed = seed;
        Clock = new Clock(calendarRate);
        Rng = new Rng(seed);
    }

    public void Tick()
    {
        Clock.Advance();
    }

    public void Run(long ticks)
    {
        for (long i = 0; i < ticks; i++) Tick();
    }

    /// <summary>
    /// FNV-1a hash over all simulation state. Two runs with the same seed and commands must
    /// produce equal hashes; the determinism tests rely on every system contributing here.
    /// </summary>
    public ulong StateHash()
    {
        var hash = new StateHasher();
        hash.Add(Clock.Tick);
        var (a, b, c, d) = Rng.State;
        hash.Add(a);
        hash.Add(b);
        hash.Add(c);
        hash.Add(d);
        return hash.Value;
    }
}
