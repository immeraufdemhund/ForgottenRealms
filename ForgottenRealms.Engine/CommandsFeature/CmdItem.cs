using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Logging;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CmdItem
{
    private readonly IGameCommand _command;
    private readonly int _size;
    private readonly string _name;
    private readonly ovr008 _ovr008;

    public CmdItem(int size, string name, IGameCommand command, ovr008 ovr008)
    {
        _size = size;
        _name = name;
        _command = command;
        _ovr008 = ovr008;
    }

    public void Run() => _command.Execute();

    public string Name() => _name;

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
            _ovr008.vm_LoadCmdSets(_size);
        }
    }
}
