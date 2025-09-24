using Godot;
using System;

public partial class Trap : Node2D
{
    private Area2D area2D;
    public Action TrapTriggered;
    public override void _Ready()
    {
        area2D.BodyEntered += body =>
        {
            if (body is Player)
            {
                TrapTriggered?.Invoke();
            }
        };
    }
}
