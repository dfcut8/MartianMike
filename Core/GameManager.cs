using Godot;
using MartianMike.Actors;

namespace MartianMike.Core;

public partial class GameManager : Node
{
    private Marker2D playerRespawn;
    private Area2D deathZone;
    private Player player;

    public override void _Ready()
    {
        GD.Print("GameManager ready.");
        player = GetNode<Player>("%Player");
        playerRespawn = GetNode<Marker2D>("%PlayerRespawn");
        deathZone = GetNode<Area2D>("%DeathZone");
        deathZone.BodyEntered += body =>
        {
            if (body is Player)
            {
                player.Position = playerRespawn.Position;
            }
        };
        GlobalEvents.TrapTriggered += OnTrapTriggered;
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
        player.Position = playerRespawn.Position;
    }

    protected override void Dispose(bool disposing)
    {
        GlobalEvents.TrapTriggered -= OnTrapTriggered;
    }
}
