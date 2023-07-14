namespace ForgottenRealms.Engine.Logging;

public class DebugWriter
{
    private bool closed;
    string filename;
    System.IO.TextWriter writer;
    object iolock = new object();

    public DebugWriter(string _filename)
    {
        filename = _filename;
    }

    public void WriteLine(string fmt, params object[] args)
    {
        if(closed) return;
        lock (iolock)
        {
            if (writer == null)
            {
                writer = new System.IO.StreamWriter(filename, true);
            }

            if (writer != null)
            {
                writer.WriteLine(fmt, args);
            }
        }
    }

    public void Write(string fmt, params object[] args)
    {
        if(closed) return;
        lock (iolock)
        {
            if (writer == null)
            {
                writer = new System.IO.StreamWriter(filename, true);
            }

            if (writer != null)
            {
                writer.Write(fmt, args);
            }
        }
    }

    public void Close()
    {
        if(closed) return;
        lock (iolock)
        {
            if (writer != null)
            {
                writer.Close();
                closed = true;
            }
        }
    }

}
