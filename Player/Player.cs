using Godot;

public partial class Player : CharacterBody2D
{
    private AnimatedSprite2D animatedSprite2D;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionPressed("ui_right"))
        {
            //Velocity.X = 100;
            animatedSprite2D.Play("run");
            animatedSprite2D.FlipH = false;
        }
        else if (Input.IsActionPressed("player_left"))
        {
            //Velocity.X = -100;
            animatedSprite2D.Play("run");
            animatedSprite2D.FlipH = true;
        }
        else
        {
            //Velocity.X = 0;
            animatedSprite2D.Play("idle");
        }

        if (Input.IsActionJustPressed("player_jump"))
        {
            animatedSprite2D.Play("jump");
        }
    }
}
