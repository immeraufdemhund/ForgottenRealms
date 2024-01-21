using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ProtectionCommand : IGameCommand
{
    public void Execute()
    {
        VmLog.WriteLine("CMD_Protection:");

        gbl.encounter_flags[0] = false;
        gbl.encounter_flags[1] = false;
        gbl.spriteChanged = false;
        ovr008.vm_LoadCmdSets(1);

        if (Cheats.skip_copy_protection == false)
        {
            ovr004.copy_protection();
        }

        ovr025.LoadPic();
    }
}
