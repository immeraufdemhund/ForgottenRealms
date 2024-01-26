namespace ForgottenRealms.Engine.Classes.DaxFiles;

public class DaxBlockReader
{
    private readonly DaxFileDecoder _daxFileDecoder;

    public DaxBlockReader(DaxFileDecoder daxFileDecoder)
    {
        _daxFileDecoder = daxFileDecoder;
    }

    public DaxBlock LoadDax(byte maskColor, byte masked, int blockId, string fileName)
    {
        _daxFileDecoder.LoadDecodeDax(out var pictureData, out var pictureSize, blockId, fileName + ".dax");

        if (pictureSize != 0)
        {
            return new DaxBlock(pictureData, masked, maskColor);
        }

        return null;
    }
}
