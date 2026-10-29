namespace Breakwater.Sim.Core;

/// <summary>
/// Deterministic xoshiro256** generator. The only randomness source allowed in the sim,
/// so a seed always reproduces a run (replays, soak-test failures, save/load).
/// </summary>
public sealed class Rng
{
    private ulong s0, s1, s2, s3;

    public Rng(ulong seed)
    {
        // SplitMix64 expands the seed so nearby seeds still give unrelated streams.
        ulong x = seed;
        s0 = SplitMix64(ref x);
        s1 = SplitMix64(ref x);
        s2 = SplitMix64(ref x);
        s3 = SplitMix64(ref x);
    }

    public ulong NextULong()
    {
        ulong result = RotateLeft(s1 * 5, 7) * 9;
        ulong t = s1 << 17;
        s2 ^= s0;
        s3 ^= s1;
        s1 ^= s2;
        s0 ^= s3;
        s2 ^= t;
        s3 = RotateLeft(s3, 45);
        return result;
    }

    public uint NextUInt() => (uint)(NextULong() >> 32);

    /// <summary>Uniform in [0, 1).</summary>
    public float NextFloat() => (NextULong() >> 40) * (1.0f / (1UL << 24));

    /// <summary>Uniform in [min, max).</summary>
    public float Range(float min, float max) => min + (max - min) * NextFloat();

    /// <summary>Uniform integer in [min, maxExclusive).</summary>
    public int Range(int min, int maxExclusive)
    {
        if (maxExclusive <= min) return min;
        ulong span = (ulong)(maxExclusive - min);
        return min + (int)(NextULong() % span);
    }

    public bool Chance(float probability) => NextFloat() < probability;

    /// <summary>Snapshot for save games and determinism hashing.</summary>
    public (ulong, ulong, ulong, ulong) State => (s0, s1, s2, s3);

    public void Restore((ulong, ulong, ulong, ulong) state) => (s0, s1, s2, s3) = state;

    private static ulong SplitMix64(ref ulong x)
    {
        ulong z = x += 0x9E3779B97F4A7C15UL;
        z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
        z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
        return z ^ (z >> 31);
    }

    private static ulong RotateLeft(ulong x, int k) => (x << k) | (x >> (64 - k));
}
