namespace ForgottenRealms.Engine.Classes;

public interface IOSDisplay
{
    void Init(int height, int width);
    void RawCopy(byte[] videoRam, int videoRamSize);
}
