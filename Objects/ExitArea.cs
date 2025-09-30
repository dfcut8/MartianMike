using System;
using System.Threading.Tasks;

using Godot;

using MartianMike.Actors;

namespace MartianMike.Objects;

public partial class ExitArea : Area2D
{
    [Export] private PackedScene NextLevel;
    private AnimatedSprite2D animatedSprite2D;
    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
        animatedSprite2D.Play("idle");
        BodyEntered += OnBodyEntered;
    }

    private async void OnBodyEntered(Node body)
    {
        if (body is not Player p)
        {
            return;
        }
        animatedSprite2D.Play("pressed");
        p.IsActive = false;
        await Task.Delay(TimeSpan.FromMilliseconds(1000));
        if (NextLevel is not null)
        {
            GetTree().ChangeSceneToPacked(NextLevel);
        }
        else
        {
            GD.Print("Game Over!!!");
            GetTree().Paused = true;
        }
    }

    protected override void Dispose(bool disposing)
    {
        BodyEntered -= OnBodyEntered;
    }
}
