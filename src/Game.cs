namespace MartianMike;

using Godot;

public partial class Game : Control
{
    public Button StartButton { get; private set; } = default!;
    public int ButtonPresses { get; private set; }

    public override void _Ready()
    {
        StartButton = GetNode<Button>("%StartButton");
        StartButton.Pressed += OnStartButtonPressed;
    }


    public void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("src/Levels/Level1.tscn");
    }

    protected override void Dispose(bool disposing)
    {
        StartButton.Pressed -= OnStartButtonPressed;
    }
}
