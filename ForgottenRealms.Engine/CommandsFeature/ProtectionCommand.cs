using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class ProtectionCommand : IGameCommand
{
    private readonly ovr004 _ovr004;
    private readonly ovr008 _ovr008;
    private readonly ovr025 _ovr025;
    public ProtectionCommand(ovr004 ovr004, ovr008 ovr008, ovr025 ovr025)
    {
        _ovr004 = ovr004;
        _ovr008 = ovr008;
        _ovr025 = ovr025;
    }

    public void Execute()
    {
        VmLog.WriteLine("CMD_Protection:");

        gbl.encounter_flags[0] = false;
        gbl.encounter_flags[1] = false;
        gbl.spriteChanged = false;
        _ovr008.vm_LoadCmdSets(1);

        if (Cheats.skip_copy_protection == false)
        {
            _ovr004.copy_protection();
        }

        _ovr025.LoadPic();
    }
}
