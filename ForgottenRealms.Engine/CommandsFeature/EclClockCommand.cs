namespace ForgottenRealms.Engine.CommandsFeature;

public class EclClockCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr021 _ovr021;
    public EclClockCommand(ovr008 ovr008, ovr021 ovr021)
    {
        _ovr008 = ovr008;
        _ovr021 = ovr021;
    }

    public void Execute()
    {
        _ovr008.vm_LoadCmdSets(2);
        var timeStep = _ovr008.vm_GetCmdValue(1) & 0xff;
        var timeSlot = _ovr008.vm_GetCmdValue(2) & 0xff;

        _ovr021.step_game_time(timeSlot, timeStep);
    }
}
