using System;
using System.IO;
using System.Media;
using System.Resources;
using ForgottenRealms.Engine;
using Microsoft.Extensions.Logging;

namespace ForgottenRealms;

public class WpfSoundDevice : ISoundDevice, IDisposable
{
    private readonly SoundPlayer _mediaPlayer;
    private readonly Stream[] sounds;
    public WpfSoundDevice(SoundPlayer mediaPlayer, ILogger<WpfSoundDevice> logger)
    {
        _mediaPlayer = mediaPlayer;
        sounds = new Stream[16];
        logger.LogInformation("Loading sounds...");
        sounds[1] = Resource.ResourceManager.GetStream("missle");
        sounds[2] = Resource.ResourceManager.GetStream("magic_hit");
        sounds[4] = Resource.ResourceManager.GetStream("death");
        sounds[5] = Resource.ResourceManager.GetStream("sound_5");
        sounds[6] = Resource.ResourceManager.GetStream("hit");
        sounds[8] = Resource.ResourceManager.GetStream("miss");
        sounds[9] = Resource.ResourceManager.GetStream("step");
        sounds[10] = Resource.ResourceManager.GetStream("sound_10");
        sounds[12] = Resource.ResourceManager.GetStream("start_sound");
    }
    public void PlaySoundById(int sampleId)
    {
        _mediaPlayer.Stream = sounds[sampleId];
        _mediaPlayer.Play();
    }

    public void Stop()
    {
        _mediaPlayer.Stop();
    }

    public void Dispose() => _mediaPlayer.Dispose();
}
