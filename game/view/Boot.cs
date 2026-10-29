using Godot;

namespace Breakwater.Game.View;

/// <summary>
/// Autoload that reads launch options passed after "--" on the command line:
///   --scenario=NAME   which scenario Main builds (default "empty")
///   --preset=NAME     camera preset (default "default")
///   --shot=PATH       save a PNG of the screen after --seconds, then quit
///   --seconds=N       how long to run before the screenshot (default 2)
/// scripts/shot.sh wraps this so Claude can look at the result.
/// </summary>
public partial class Boot : Node
{
    public static string Scenario { get; private set; } = "empty";
    public static string CameraPreset { get; private set; } = "default";
    public static string? ShotPath { get; private set; }
    public static double ShotSeconds { get; private set; } = 2.0;

    private double elapsed;
    private bool shotTaken;

    public override void _EnterTree()
    {
        foreach (string arg in OS.GetCmdlineUserArgs())
        {
            int eq = arg.IndexOf('=');
            if (!arg.StartsWith("--") || eq < 0) continue;
            string key = arg[2..eq];
            string value = arg[(eq + 1)..];
            switch (key)
            {
                case "scenario": Scenario = value; break;
                case "preset": CameraPreset = value; break;
                case "shot": ShotPath = value; break;
                case "seconds": ShotSeconds = double.Parse(value, System.Globalization.CultureInfo.InvariantCulture); break;
            }
        }
        ProcessMode = ProcessModeEnum.Always;
    }

    public override void _Process(double delta)
    {
        if (ShotPath is null || shotTaken) return;
        elapsed += delta;
        if (elapsed < ShotSeconds) return;
        shotTaken = true;
        TakeShotAndQuit();
    }

    private async void TakeShotAndQuit()
    {
        // Wait for a fully drawn frame so the capture isn't blank or half-updated.
        await ToSignal(RenderingServer.Singleton, RenderingServer.SignalName.FramePostDraw);
        Image image = GetViewport().GetTexture().GetImage();
        string path = ShotPath!;
        string? dir = System.IO.Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir)) System.IO.Directory.CreateDirectory(dir);
        Error err = image.SavePng(path);
        GD.Print(err == Error.Ok ? $"[shot] saved {path}" : $"[shot] FAILED {err} {path}");
        GetTree().Quit(err == Error.Ok ? 0 : 1);
    }
}
