using Godot;

using MartianMike.Actors;

namespace MartianMike.Core;

public partial class GameManager : Node
{
    [Export] private StartArea startArea;
    private Area2D deathZone;
    private Player player;

    public override void _Ready()
    {
        GD.Print("GameManager ready.");
        player = GetNode<Player>("%Player");
        // startArea = GetNode<StartArea>("%StartArea");
        deathZone = GetNode<Area2D>("%DeathZone");
        deathZone.BodyEntered += body =>
        {
            if (body is Player)
            {
                player.Position = startArea.GetSpawnPosition();
            }
        };
        GlobalEvents.TrapTriggered += OnTrapTriggered;

        // Need this because GameManager is another node and could be ready before StartArea
        startArea.Ready += () =>
        {
            player.Position = startArea.GetSpawnPosition();
            GD.Print("Player spawned at: " + player.Position);
        };
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("global_reset_level"))
        {
            GD.Print("Reloading current level...");
            GetTree().ReloadCurrentScene();
        }
        if (Input.IsActionJustPressed("global_quit"))
        {
            GD.Print("Quitting the game...");
            GetTree().Quit();
        }
    }

    private void OnTrapTriggered()
    {
        player.Position = startArea.GetSpawnPosition();
    }

    protected override void Dispose(bool disposing)
    {
        GlobalEvents.TrapTriggered -= OnTrapTriggered;
    }
}
