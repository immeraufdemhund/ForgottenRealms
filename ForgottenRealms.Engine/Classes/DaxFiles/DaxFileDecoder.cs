namespace ForgottenRealms.Engine.Classes.DaxFiles;

public class DaxFileDecoder
{
    private readonly SoundDriver _soundDriver;

    public DaxFileDecoder(SoundDriver soundDriver)
    {
        _soundDriver = soundDriver;
    }

    public void LoadDecodeDax(out byte[] outData, out short decodeSize, int blockId, string fileName)
    {
        _soundDriver.PlaySound(Sound.sound_0);
        outData = DaxCache.LoadDax(fileName.ToLower(), blockId);
        decodeSize = outData == null ? (short)0 : (short)outData.Length;
    }
}
