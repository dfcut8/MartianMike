using Godot;
using MartianMike.Core;
using System;

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
            return "00:00.000";
        }
        double timeLeft = levelTimer.TimeLeft;
        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);
        int milliseconds = (int)((timeLeft - Math.Floor(timeLeft)) * 1000);
        return $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
    }
}
