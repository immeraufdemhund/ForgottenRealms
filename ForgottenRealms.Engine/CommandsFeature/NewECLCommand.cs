﻿using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class NewECLCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    public NewECLCommand(ovr008 ovr008)
    {
        _ovr008 = ovr008;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(1);

        var block_id = (byte)_ovr008.vm_GetCmdValue(1);

        VmLog.WriteLine("CMD_NewECL: block_id {0}", block_id);

        gbl.area_ptr.LastEclBlockId = gbl.EclBlockId;
        gbl.EclBlockId = block_id;

        _ovr008.load_ecl_dax(block_id);
        _ovr008.vm_init_ecl();
        gbl.stopVM = true;
        gbl.vmFlag01 = true;

        gbl.encounter_flags[0] = false;
        gbl.encounter_flags[1] = false;
    }
}
