using Godot;

public partial class GameManager : Node
{
    private Marker2D playerRespawn;
    private Area2D deathZone;

    public override void _Ready()
    {
        playerRespawn = GetNode<Marker2D>("%PlayerRespawn");
        deathZone = GetNode<Area2D>("%DeathZone");
        deathZone.BodyEntered += body =>
        {
            if (body is Player)
            {
                body.Position = playerRespawn.Position;
            }
        };
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("global_reset_level"))
        {
            GD.Print("Reloading current level...");
            GetTree().ReloadCurrentScene();
        }
        if (Input.IsActionJustPressed("global_quit"))
        {
            GD.Print("Quitting the game...");
            GetTree().Quit();
        }
    }
}
