using Godot;
using MartianMike.Core;

namespace MartianMike.Ui;

public partial class Hud : Control
{
    [Export] private GameManager gameManager;

    private Timer levelTimer;
    private Label timerLabel;

    override public void _Ready()
    {
        if (gameManager is null)
        {
            GD.PrintErr("GameManager is not assigned in the Hud.");
        }
        levelTimer = gameManager.GetNode<Timer>("%LevelTimer");
        timerLabel = GetNode<Label>("%TimerLabel");
    }

    override public void _Process(double delta)
    {
        timerLabel.Text = GetFormattedTime();
    }

    public string GetFormattedTime()
    {
        if (levelTimer is null)
        {
            return "00:00";
        }
        int totalSeconds = (int)levelTimer.TimeLeft;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:D2}:{seconds:D2}";
    }
}
