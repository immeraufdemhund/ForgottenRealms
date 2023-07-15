using System.IO;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine;

public class GameFileLoader
{
    public static FileInfo GetFileInfo(string filename)
    {
        var fileInfo = new FileInfo(Path.Combine("CURSE", filename));

        if (fileInfo.Exists == false)
        {
            Logger.Log("Unable to find {0}", fileInfo.FullName);
        }

        return fileInfo;
    }
}
