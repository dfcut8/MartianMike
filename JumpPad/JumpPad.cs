using Godot;

public partial class JumpPad : Area2D
{
    private AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyExited(Node2D body)
    {
        animatedSprite2D.Play("idle");
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            animatedSprite2D.Play("jump");
            (body as Player).Jump();
        }
    }
}
