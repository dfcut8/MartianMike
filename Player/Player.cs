using Godot;

public partial class Player : CharacterBody2D
{
    [Export] private int gravity = 400;
    [Export] private int speed = 150;
    [Export] private int jumpVelocity = -200;
    private AnimatedSprite2D animatedSprite2D;
    private Area2D deathZone;

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("%AnimatedSprite2D");
        deathZone = GetNode<Area2D>("%DeathZone");
        deathZone.BodyEntered += body =>
        {
            if (body is Player)
            {
                // TODO: Update to handle lives and game over screen
                GetTree().ReloadCurrentScene();
            }
        };
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
            v.Y += gravity * (float)delta;
        }
    }

    private void HandlePlayerInputs(ref Vector2 v)
    {
        if (Input.IsActionJustPressed("player_jump") && IsOnFloor())
        {
            v.Y = jumpVelocity;
            animatedSprite2D.Play("jump");
        }

        var direction = Input.GetAxis("player_left", "player_right") * speed;
        v.X = direction;
        if (direction != 0)
        {
            if (IsOnFloor())
            {
                animatedSprite2D.Play("run");
            }
            animatedSprite2D.FlipH = direction < 0;
        }
        else if (IsOnFloor())
        {
            animatedSprite2D.Play("idle");
        }

        if (!IsOnFloor() && v.Y > 0)
        {
            animatedSprite2D.Play("fall");
        }
        else if (IsOnFloor() && v.Y < 0)
        {
            animatedSprite2D.Play("jump");
        }
    }
}