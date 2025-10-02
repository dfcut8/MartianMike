using Godot;

namespace MartianMike.Actors;

public partial class Player : CharacterBody2D
{
    [Export] private int gravity = 400;
    [Export] private int speed = 150;
    [Export] private int jumpVelocity = -200;
    [Export] private AudioStream jumpSoundEffect;
    private AnimatedSprite2D animatedSprite2D;

    public bool IsActive { get; set; } = true;

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

    /// <summary>
    /// <para>Makes the player character jump using the default jump velocity. Updates the vertical velocity and plays the jump animation.</para>
    /// </summary>
    public void Jump()
    {
        var velocity = Velocity;
        Jump(ref velocity);
    }

    /// <summary>
    /// Makes the player character jump using a custom jump velocity.
    /// Updates the vertical velocity and plays the jump animation.
    /// </summary>
    /// <param name="customJumpVelocity">The custom vertical velocity to apply for the jump.</param>
    public void Jump(int customJumpVelocity)
    {
        var velocity = Velocity;
        Jump(ref velocity, customJumpVelocity);
    }

    private void Jump(ref Vector2 velocity)
    {
        velocity.Y = jumpVelocity;
        animatedSprite2D.Play("jump");
        AudioManager.Instance.PlaySoundEffect(jumpSoundEffect);
        Velocity = velocity;
    }

    /// <summary>
    /// Sets the vertical velocity to a custom jump velocity and plays the jump animation.
    /// </summary>
    /// <param name="velocity">Reference to the velocity vector to modify.</param>
    /// <param name="customJumpVelocity">The custom vertical velocity to apply for the jump.</param>
    private void Jump(ref Vector2 velocity, int customJumpVelocity)
    {
        velocity.Y = customJumpVelocity;
        animatedSprite2D.Play("jump");
        Velocity = velocity;
    }

    /// <summary>
    /// Applies gravity to the player's vertical velocity if not on the floor.
    /// </summary>
    /// <param name="delta">The frame's time step.</param>
    /// <param name="v">Reference to the velocity vector to modify.</param>
    private void AddGravity(double delta, ref Vector2 v)
    {
        if (!IsOnFloor())
        {
            v.Y += gravity * (float)delta;
        }
    }

    /// <summary>
    /// Handles player input for movement and jumping, and updates animation states accordingly.
    /// </summary>
    /// <param name="v">Reference to the velocity vector to modify.</param>
    private void HandlePlayerInputs(ref Vector2 v)
    {
        var velocity = Velocity;
        if (Input.IsActionJustPressed("player_jump") && IsOnFloor())
        {
            Jump(ref v);
        }

        var direction = 0f;
        if (IsActive)
        {
            direction = Input.GetAxis("player_left", "player_right") * speed;
        }

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