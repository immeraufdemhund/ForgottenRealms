using System;
using System.IO;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class SoundDriver
{
    public static void SetSound(bool On)
    {
        gbl.soundType = On ? SoundType.PC : SoundType.None;
    }

    public static void SetPicture(bool On)
    {
        gbl.PicsOn = On;
    }

    public static void SetAnimation(bool On)
    {
        gbl.AnimationsOn = On;
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

    private static void PlaySoundById(Sound sound)
    {
        int sampleId = (int)sound - 1;
        if (sounds[sampleId] != null)
        {
            // TODO: find a way to make this work.
            //sounds[sampleId].Play();
        }
        else
        {
        }
    }

    private static void PlaySoundFF()
    {
        foreach (var sp in sounds)
        {
            if (sp != null)
            {
                // TODO: find a way to make this work.
                //sp.Stop();
            }
        }
    }

    private static void PlaySound0()
    {
        foreach (var sp in sounds)
        {
            if (sp != null)
            {
                // TODO: find a way to make this work.
                //sp.Stop();
            }
        }
    }

    private static Stream?[] sounds;

    internal static void SoundInit(Func<string, Stream?> resources)
    {
        sounds = new Stream?[13];
        sounds[1] = resources.Invoke("missle");
        sounds[2] = resources.Invoke("magic_hit");
        sounds[4] = resources.Invoke("death");
        sounds[5] = resources.Invoke("sound_5");
        sounds[6] = resources.Invoke("hit");
        sounds[8] = resources.Invoke("miss");
        sounds[9] = resources.Invoke("step");
        sounds[10] = resources.Invoke("sound_10");
        sounds[12] = resources.Invoke("start_sound");
    }
}
