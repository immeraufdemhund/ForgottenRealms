using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class SoundDriver
{
    private readonly ISoundDevice _soundDevice;

    public SoundDriver(ISoundDevice soundDevice)
    {
        _soundDevice = soundDevice;
    }

    public void SetSound(bool On)
    {
        gbl.soundType = On ? SoundType.PC : SoundType.None;
    }

    public void PlaySound(Sound sound)
    {
        if (gbl.soundType != SoundType.PC)
        {
            return;
        }

        if (sound == Sound.sound_0)
        {
            PlaySound0();
        }
        else if (sound == Sound.sound_1)
        {
        }
        else if (sound == Sound.sound_FF) // off maybe.
        {
            PlaySoundFF();
        }
        else if (sound >= Sound.sound_2 && sound <= Sound.sound_e)
        {
            PlaySoundById(sound);
        }
        else if (sound == Sound.sound_f)
        {
        }
    }

    private void PlaySoundById(Sound sound)
    {
        var sampleId = (int)sound - 1;
        _soundDevice.PlaySoundById(sampleId);
    }

    private void PlaySoundFF()
    {
        _soundDevice.Stop();
    }

    private void PlaySound0()
    {
        _soundDevice.Stop();
    }
}
