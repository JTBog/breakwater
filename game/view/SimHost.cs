using Breakwater.Sim.Core;
using Godot;

namespace Breakwater.Game.View;

/// <summary>
/// Owns the SimWorld and advances it on the fixed 20 Hz tick, independent of frame rate.
/// Exposes an interpolation alpha so views can blend between the last two ticks.
/// </summary>
public partial class SimHost : Node
{
    /// <summary>Speed multiplier: 0 = paused, 1/2/4 = normal speeds.</summary>
    [Export] public int Speed { get; set; } = 1;

    /// <summary>Stops a slow frame from triggering an ever-growing catch-up spiral.</summary>
    private const int MaxTicksPerFrame = 16;

    public SimWorld World { get; private set; } = null!;
    public float InterpolationAlpha { get; private set; }

    private double accumulator;

    public override void _Ready()
    {
        World = new SimWorld(seed: 1);
    }

    public override void _Process(double delta)
    {
        accumulator += delta * Speed;
        int ticks = 0;
        while (accumulator >= Clock.TickSeconds && ticks < MaxTicksPerFrame)
        {
            World.Tick();
            accumulator -= Clock.TickSeconds;
            ticks++;
        }
        if (ticks == MaxTicksPerFrame) accumulator = 0;
        InterpolationAlpha = (float)(accumulator / Clock.TickSeconds);
    }
}
