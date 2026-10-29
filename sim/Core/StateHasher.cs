namespace Breakwater.Sim.Core;

/// <summary>Incremental FNV-1a 64-bit hash used for determinism checks.</summary>
public struct StateHasher
{
    private const ulong Offset = 0xcbf29ce484222325UL;
    private const ulong Prime = 0x100000001b3UL;

    private ulong value;
    private bool started;

    public ulong Value => started ? value : Offset;

    public void Add(ulong x)
    {
        if (!started) { value = Offset; started = true; }
        for (int i = 0; i < 8; i++)
        {
            value ^= (byte)(x >> (i * 8));
            value *= Prime;
        }
    }

    public void Add(long x) => Add(unchecked((ulong)x));

    public void Add(int x) => Add(unchecked((ulong)(uint)x));

    public void Add(float x) => Add(BitConverter.SingleToUInt32Bits(x));

    public void Add(uint x) => Add((ulong)x);
}
