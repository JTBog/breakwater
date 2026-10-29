using Godot;

namespace Breakwater.Game.View;

/// <summary>
/// Entry scene. Builds the scenario named on the command line (see Boot) and a debug overlay.
/// Scenarios grow into data-driven definitions in Phase 1.7; for now they are code.
/// </summary>
public partial class Main : Node3D
{
    private SimHost simHost = null!;
    private Label debugLabel = null!;

    public override void _Ready()
    {
        simHost = new SimHost { Name = "SimHost" };
        AddChild(simHost);

        switch (Boot.Scenario)
        {
            case "empty":
                BuildEmptyScenario();
                break;
            default:
                GD.PushError($"Unknown scenario '{Boot.Scenario}', falling back to 'empty'.");
                BuildEmptyScenario();
                break;
        }

        AddChild(CameraPresets.Create(Boot.CameraPreset));
        BuildDebugOverlay();
    }

    public override void _Process(double delta)
    {
        var clock = simHost.World.Clock;
        int hour = (int)clock.HourOfDay;
        int minute = (int)((clock.HourOfDay - hour) * 60);
        debugLabel.Text =
            $"BREAKWATER  ·  scenario {Boot.Scenario}\n" +
            $"DAY {clock.Day}  {hour:00}:{minute:00}  ·  tick {clock.Tick}  ·  {Engine.GetFramesPerSecond()} fps";
    }

    private void BuildEmptyScenario()
    {
        var environment = new Godot.Environment
        {
            BackgroundMode = Godot.Environment.BGMode.Color,
            BackgroundColor = new Color(0.55f, 0.62f, 0.68f),
            AmbientLightSource = Godot.Environment.AmbientSource.Color,
            AmbientLightColor = new Color(0.6f, 0.65f, 0.7f),
            AmbientLightEnergy = 0.6f,
        };
        AddChild(new WorldEnvironment { Environment = environment });

        var sun = new DirectionalLight3D
        {
            RotationDegrees = new Vector3(-55, 35, 0),
            ShadowEnabled = true,
            // The battlefield camera sits ~140 m from the ground; the 100 m default would drop shadows.
            DirectionalShadowMaxDistance = 400,
            LightEnergy = 1.2f,
        };
        AddChild(sun);

        var ground = new MeshInstance3D
        {
            Mesh = new PlaneMesh { Size = new Vector2(200, 200) },
            MaterialOverride = new StandardMaterial3D { AlbedoColor = new Color(0.32f, 0.33f, 0.35f) },
        };
        AddChild(ground);

        // A few placeholder blocks so lighting, shadows and the camera angle are visible.
        var rng = new RandomNumberGenerator { Seed = 1 };
        for (int i = 0; i < 12; i++)
        {
            float height = rng.RandfRange(4, 18);
            var block = new MeshInstance3D
            {
                Mesh = new BoxMesh { Size = new Vector3(rng.RandfRange(6, 14), height, rng.RandfRange(6, 14)) },
                MaterialOverride = new StandardMaterial3D { AlbedoColor = new Color(0.78f, 0.74f, 0.66f) },
                Position = new Vector3(rng.RandfRange(-60, 60), height / 2, rng.RandfRange(-60, 60)),
            };
            AddChild(block);
        }
    }

    private void BuildDebugOverlay()
    {
        var layer = new CanvasLayer();
        debugLabel = new Label { Position = new Vector2(16, 12) };
        debugLabel.AddThemeColorOverride("font_color", new Color(0.95f, 0.95f, 0.9f));
        debugLabel.AddThemeColorOverride("font_shadow_color", new Color(0, 0, 0, 0.8f));
        debugLabel.AddThemeFontSizeOverride("font_size", 22);
        layer.AddChild(debugLabel);
        AddChild(layer);
    }
}
