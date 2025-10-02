using Godot;

public partial class AudioManager : Node
{
    private AudioStreamPlayer backgroundMusicPlayer;

    public override void _Ready()
    {
        backgroundMusicPlayer = GetNode<AudioStreamPlayer>("%BackgroundMusicPlayer");

        // TODO: Make volume adjustable in settings menu
        backgroundMusicPlayer.VolumeDb = -10;
        backgroundMusicPlayer.Play();
    }
}
