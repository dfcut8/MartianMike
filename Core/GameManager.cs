using Godot;
using MartianMike.Actors;
using MartianMike.Objects;
using System;
using System.Threading.Tasks;

namespace MartianMike.Core;

public partial class GameManager : Node
{
    [Export] private StartArea startArea;
    private Area2D deathZone;
    private Player player;
    private CanvasLayer gameOverScreen;
    public override void _Ready()
    {
        GD.Print("GameManager ready.");
        player = GetNode<Player>("%Player");
        startArea = GetNode<StartArea>("%StartArea");
        deathZone = GetNode<Area2D>("%DeathZone");
        gameOverScreen = GetNode<CanvasLayer>("%GameOverScreen");
        gameOverScreen.Visible = false;
        gameOverScreen.ProcessMode = ProcessModeEnum.Disabled;
        gameOverScreen.GetNode<Button>("%TryAgain").Pressed += () =>
        {
            GetTree().ReloadCurrentScene();
            GetTree().Paused = false;
        };
        gameOverScreen.GetNode<Button>("%Quit").Pressed += () =>
        {
            GetTree().Quit();
        };

        GlobalEvents.TrapTriggered += OnTrapTriggered;
        GlobalEvents.ExitAreaReached += OnExitAreaReached;

        deathZone.BodyEntered += body =>
        {
            if (body is Player)
            {
                player.Position = startArea.GetSpawnPosition();
            }
        };

        // Need this because GameManager is another node and could be ready before StartArea
        startArea.Ready += () =>
        {
            player.Position = startArea.GetSpawnPosition();
            GD.Print("Player spawned at: " + player.Position);
        };
    }

    private async void OnExitAreaReached(ExitArea exitArea)
    {
        player.IsActive = false;
        await Task.Delay(TimeSpan.FromMilliseconds(1000));
        if (exitArea.NextLevel is not null)
        {
            GetTree().ChangeSceneToPacked(exitArea.NextLevel);
        }
        else
        {
            gameOverScreen.Visible = true;
            gameOverScreen.ProcessMode = ProcessModeEnum.WhenPaused;
            GetTree().Paused = true;
        }
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
        GlobalEvents.ExitAreaReached -= OnExitAreaReached;
    }
}
