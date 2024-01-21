using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CmdItem
{
    private readonly IGameCommand _command;
    private readonly int _size;
    private readonly string _name;

    public CmdItem(int size, string name, IGameCommand command)
    {
        _size = size;
        _name = name;
        _command = command;
    }

    public void Run()
    {
        _command.Execute();
    }

    public string Name()
    {
        return _name;
    }

    internal void Skip()
    {
        if (gbl.printCommands == true)
        {
            Logger.Debug("SKIPPING: {0}", _name);
        }

        if (_size == 0)
        {
            gbl.ecl_offset += 1;
        }
        else
        {
            ovr008.vm_LoadCmdSets(_size);
        }
    }
}
