namespace ForgottenRealms.Engine.Classes.DaxFiles;

public class DaxFileDecoder
{
    public void LoadDecodeDax(out byte[] outData, out short decodeSize, int blockId, string fileName)
    {
        seg044.PlaySound(Sound.sound_0);
        outData = DaxCache.LoadDax(fileName.ToLower(), blockId);
        decodeSize = outData == null ? (short)0 : (short)outData.Length;
    }
}
