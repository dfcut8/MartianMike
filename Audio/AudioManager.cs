using Godot;

public partial class AudioManager : Node
{
    private AudioStreamPlayer backgroundMusicPlayer;

    public override void _Ready()
    {
        backgroundMusicPlayer = GetNode<AudioStreamPlayer>("%BackgroundMusicPlayer");
        backgroundMusicPlayer.ProcessMode = ProcessModeEnum.Always;

        // TODO: Make volume adjustable in settings menu
        backgroundMusicPlayer.VolumeDb = -10;
        backgroundMusicPlayer.Play();
    }
}
