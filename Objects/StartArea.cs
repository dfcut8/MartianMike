using Godot;

namespace MartianMike.Objects;

public partial class StartArea : StaticBody2D
{
    private Marker2D spawnMarker;
    public override void _Ready()
    {
        GD.Print("StartArea ready.");
        spawnMarker = GetNode<Marker2D>("SpawnMarker");
    }

    public Vector2 GetSpawnPosition()
    {
        return spawnMarker.GlobalPosition;
    }
}
