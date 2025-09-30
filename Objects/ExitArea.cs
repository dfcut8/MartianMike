using Godot;

using MartianMike.Actors;
using MartianMike.Core;

namespace MartianMike.Objects;

public partial class ExitArea : Area2D
{
    [Export] public PackedScene NextLevel { get; private set; }
    private AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
        animatedSprite2D.Play("idle");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is not Player p)
        {
            return;
        }
        animatedSprite2D.Play("pressed");
        GlobalEvents.ExitAreaReached?.Invoke(this);
    }
}
