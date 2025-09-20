using Godot;

public partial class Main : Control
{
    private Button startButton;
    private Button exitButton;

    public override void _Ready()
    {
        startButton = GetNode<Button>("%Start");
        exitButton = GetNode<Button>("%Exit");

        startButton.Pressed += OnStartButtonPressed;
        exitButton.Pressed += OnExitButtonPressed;
    }

    private void OnExitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("Levels/Level1.tscn");
    }
}
