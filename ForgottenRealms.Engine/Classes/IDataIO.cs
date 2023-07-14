namespace ForgottenRealms.Engine.Classes;

internal interface IDataIO
{
    void Write(byte[] data, int offset);
    void Read(byte[] data, int offset);
}
