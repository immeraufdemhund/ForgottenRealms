using System;
using System.IO;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class seg044
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

    internal static void PlaySound(Sound arg_0) /*sub_120E0*/
    {
        if (gbl.soundType == SoundType.PC)
        {
            if (arg_0 == Sound.sound_0)
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
            else if (arg_0 == Sound.sound_1)
            {
            }
            else if (arg_0 == Sound.sound_FF) // off maybe.
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
            else if (arg_0 >= Sound.sound_2 && arg_0 <= Sound.sound_e)
            {
                int sampleId = (int)arg_0 - 1;
                if (sounds[sampleId] != null)
                {
                    // TODO: find a way to make this work.
                    //sounds[sampleId].Play();
                }
                else
                {
                }
            }
            else if (arg_0 == Sound.sound_f)
            {
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
