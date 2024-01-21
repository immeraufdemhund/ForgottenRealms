using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ReturnCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;
        if (gbl.vmCallStack.Count > 0)
        {
            ushort newOffset = gbl.vmCallStack.Peek();
            VmLog.WriteLine("CMD_Return: was: {0:X} now: {1:X}", gbl.ecl_offset, newOffset);
            gbl.vmCallStack.Pop();
            gbl.ecl_offset = newOffset;
        }
        else
        {
            VmLog.Write("CMD_Return: call stack empty ");
            new ExitCommand().Execute();
        }
    }
}