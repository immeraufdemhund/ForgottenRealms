using System;

namespace ForgottenRealms.Engine;

public interface ISoundDevice
{
    void PlaySoundById(int soundId);
    void Stop();
}
[Obsolete]
public class NullSoundDevice : ISoundDevice
{
    public void PlaySoundById(int soundId)
    {
    }

    public void Stop()
    {
    }
}
