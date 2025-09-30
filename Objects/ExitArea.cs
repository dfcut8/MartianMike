using Godot;

using MartianMike.Actors;

public partial class ExitArea : Area2D
{
    private AnimatedSprite2D animatedSprite2D;
    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
        animatedSprite2D.Play("idle");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            animatedSprite2D.Play("pressed");
            (body as Player).IsActive = false;
        }
    }

    protected override void Dispose(bool disposing)
    {
        BodyEntered -= OnBodyEntered;
    }
}
