using Godot;

public partial class AudioManager : Node
{
    private AudioStreamPlayer backgroundMusicPlayer;
    private AudioStreamPlayer soundEffectPlayer;

    private static AudioManager? instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance is null)
                GD.PushWarning("AudioManager instance is not initialized yet.");
            return instance!;
        }
    }

    public override void _Ready()
    {
        if (instance != null && instance != this)
        {
            QueueFree();
            return;
        }
        instance = this;

        backgroundMusicPlayer = GetNode<AudioStreamPlayer>("%BackgroundMusicPlayer");
        soundEffectPlayer = GetNode<AudioStreamPlayer>("%SoundEffectPlayer");

        PlayBackgroundMusic();
    }

    public void PlaySoundEffect(AudioStream soundEffect, float volumeDb = 0f)
    {
        soundEffectPlayer.Stream = soundEffect;
        soundEffectPlayer.VolumeDb = volumeDb;
        soundEffectPlayer.Play();

    }

    private void PlayBackgroundMusic()
    {
        backgroundMusicPlayer.ProcessMode = ProcessModeEnum.Always;

        // TODO: Make volume adjustable in settings menu
        backgroundMusicPlayer.VolumeDb = -10;
        backgroundMusicPlayer.Play();
    }
}
