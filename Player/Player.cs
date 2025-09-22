using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private int gravity = 400;
    [Export] private int speed = 150;
    [Export] private int jumpVelocity = -200;
    private AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        HandlePlayerInputs(ref velocity);
        AddGravity(delta, ref velocity);
        Velocity = velocity;
        MoveAndSlide();
    }

    private void AddGravity(double delta, ref Vector2 v)
    {
        if (!IsOnFloor())
        {
            animatedSprite2D.Play("fall");
            v.Y += gravity * (float)delta;
        }
    }

    private void HandlePlayerInputs(ref Vector2 v)
    {
        if (Input.IsActionPressed("ui_right"))
        {
            v.X = speed;
            if (IsOnFloor())
            {
                animatedSprite2D.Play("run");
            }
            animatedSprite2D.FlipH = false;
        }
        else if (Input.IsActionPressed("player_left"))
        {
            v.X = -speed;
            if (IsOnFloor())
            {
                animatedSprite2D.Play("run");
            }
            animatedSprite2D.FlipH = true;
        }
        else
        {
            v.X = 0;
            animatedSprite2D.Play("idle");
        }

        if (Input.IsActionJustPressed("player_jump"))
        {
            v.Y = jumpVelocity;
            animatedSprite2D.Play("jump");
        }
    }
}
