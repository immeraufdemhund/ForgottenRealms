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

    internal static void SoundInit()
    {
        var resources = new System.Resources.ResourceManager("Main.Resource", System.Reflection.Assembly.GetEntryAssembly());

        sounds = new Stream?[13];
        sounds[1] = resources.GetStream("missle");
        sounds[2] = resources.GetStream("magic_hit");
        sounds[4] = resources.GetStream("death");
        sounds[5] = resources.GetStream("sound_5");
        sounds[6] = resources.GetStream("hit");
        sounds[8] = resources.GetStream("miss");
        sounds[9] = resources.GetStream("step");
        sounds[10] = resources.GetStream("sound_10");
        sounds[12] = resources.GetStream("start_sound");
    }
}
