using Godot;
using MartianMike.Core;

public partial class Trap : Node2D
{
    private Area2D area2D;

    public override void _Ready()
    {
        area2D = GetNode<Area2D>("Area2D");
        area2D.BodyEntered += body =>
        {
            if (body is Player)
            {
                GlobalEvents.TrapTriggered?.Invoke();
            }
        };
    }
}
