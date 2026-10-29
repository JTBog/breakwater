using Godot;

namespace Breakwater.Game.View;

/// <summary>
/// Named camera setups used by scripts/shot.sh so screenshots are repeatable.
/// "default" matches the high-angle battlefield view in reference/observations.md (~55° pitch).
/// </summary>
public static class CameraPresets
{
    public static Camera3D Create(string preset)
    {
        var camera = new Camera3D { Fov = 35, Far = 20000 };
        switch (preset)
        {
            case "ops":
                camera.Position = new Vector3(0, 400, 120);
                camera.RotationDegrees = new Vector3(-75, 0, 0);
                break;
            case "close":
                camera.Position = new Vector3(0, 40, 30);
                camera.RotationDegrees = new Vector3(-55, 0, 0);
                break;
            default:
                if (preset != "default") GD.PushWarning($"Unknown camera preset '{preset}', using default.");
                camera.Position = new Vector3(0, 110, 80);
                camera.RotationDegrees = new Vector3(-55, 0, 0);
                break;
        }
        return camera;
    }
}
