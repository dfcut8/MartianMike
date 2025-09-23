using Godot;

public partial class JumpPad : Area2D
{
    [Export] private int jumpPadVelocity = -400;
    private AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player)
        {
            animatedSprite2D.Play("jump");
            (body as Player).Jump(jumpPadVelocity);
        }
    }
}
