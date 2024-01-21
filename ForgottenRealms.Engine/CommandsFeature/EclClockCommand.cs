namespace ForgottenRealms.Engine.CommandsFeature;

public class EclClockCommand : IGameCommand
{
    public void Execute()
    {
        ovr008.vm_LoadCmdSets(2);
        int timeStep = ovr008.vm_GetCmdValue(1) & 0xff;
        int timeSlot = ovr008.vm_GetCmdValue(2) & 0xff;

        ovr021.step_game_time(timeSlot, timeStep);
    }
}