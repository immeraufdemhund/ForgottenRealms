using System.Collections.Generic;

namespace ForgottenRealms.Engine.Classes.DaxFiles;

public class DaxCache
{
    private static Dictionary<string, DaxFileCache> fileCache = new();

    public static byte[] LoadDax(string file_name, int block_id)
    {
        DaxFileCache dfc;

        file_name = file_name.ToLower();

        if (!fileCache.TryGetValue(file_name, out dfc))
        {
            dfc = new DaxFileCache(file_name);
            fileCache.Add(file_name, dfc);
        }

        return dfc.GetData(block_id);
    }
}
