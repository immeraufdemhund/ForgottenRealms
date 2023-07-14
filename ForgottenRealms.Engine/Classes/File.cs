using System.IO;

namespace ForgottenRealms.Engine.Classes;

/// <summary>
/// Summary description for File.
/// </summary>
public class File
{
    public File()
    {
        // TODO tidy-up this pascal based concept.
    }

    public string name;

    public FileStream stream;

    public void Assign(string fileString)
    {
        name = fileString;
        stream = System.IO.File.Open(fileString, FileMode.OpenOrCreate);
    }
}
