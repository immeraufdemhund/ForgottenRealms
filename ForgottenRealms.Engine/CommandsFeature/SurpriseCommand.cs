namespace ForgottenRealms.Engine.CommandsFeature;

public class SurpriseCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(4);
        byte val_a = 0;

        byte var_8 = (byte)ovr008.vm_GetCmdValue(1);
        byte var_7 = (byte)ovr008.vm_GetCmdValue(2);
        byte var_6 = (byte)ovr008.vm_GetCmdValue(3);
        byte var_5 = (byte)ovr008.vm_GetCmdValue(4);

        byte var_9 = (byte)((var_5 + 2) - var_8);
        byte var_A = (byte)((var_7 + 2) - var_6);

        byte var_1 = ovr024.roll_dice(6, 1);
        byte var_2 = ovr024.roll_dice(6, 1);

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

        ovr008.vm_SetMemoryValue(val_a, 0x2cb);
    }
}