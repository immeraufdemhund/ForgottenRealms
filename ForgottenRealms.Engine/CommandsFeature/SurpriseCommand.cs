namespace ForgottenRealms.Engine.CommandsFeature;

public class SurpriseCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr024 _ovr024;
    public SurpriseCommand(ovr008 ovr008, ovr024 ovr024)
    {
        _ovr008 = ovr008;
        _ovr024 = ovr024;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(4);
        byte val_a = 0;

        var var_8 = (byte)_ovr008.vm_GetCmdValue(1);
        var var_7 = (byte)_ovr008.vm_GetCmdValue(2);
        var var_6 = (byte)_ovr008.vm_GetCmdValue(3);
        var var_5 = (byte)_ovr008.vm_GetCmdValue(4);

        var var_9 = (byte)(var_5 + 2 - var_8);
        var var_A = (byte)(var_7 + 2 - var_6);

        var var_1 = _ovr024.roll_dice(6, 1);
        var var_2 = _ovr024.roll_dice(6, 1);

        if (var_1 <= var_9)
        {
            if (var_2 <= var_A)
            {
                val_a = 3;
            }
            else
            {
                val_a = 1;
            }
        }

        if (var_2 <= var_A)
        {
            val_a = 2;
        }

        _ovr008.vm_SetMemoryValue(val_a, 0x2cb);
    }
}
