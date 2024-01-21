using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ExitCommand : IGameCommand
{
    public void Execute()
    {
        VmLog.WriteLine("CMD_Exit: byte_1AB0A {0}", gbl.restore_player_ptr);
        VmLog.WriteLine("");

        if (gbl.restore_player_ptr == true)
        {
            gbl.SelectedPlayer = gbl.LastSelectedPlayer;
            gbl.restore_player_ptr = false;
        }

        gbl.encounter_flags[0] = false;
        gbl.encounter_flags[1] = false;

        gbl.spriteChanged = false;
        gbl.stopVM = true;

        gbl.ecl_offset++;

        if (gbl.vmCallStack.Count > 0)
        {
            //System.Console.Write("  vmCallStack:");
            //foreach (ushort us in gbl.vmCallStack)
            //{
            //    System.Console.Write(" {0,4:X", us);
            //}
            //System.Console.WriteLine();

            gbl.vmCallStack.Clear();
        }

        gbl.textYCol = 0x11;
        gbl.textXCol = 1;
    }
}
