using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect39Action : IAffectAction
{
    private readonly ovr013 _ovr013;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public Affect39Action(ovr013 ovr013, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr013 = ovr013;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }
    public Affects ActionForAffect => Affects.affect_39;
    public void Execute(Effect effect, object param, Player player)
    {
        Player target = player.actions.target;

        if (gbl.bytes_1D2C9[1] == 2 &&
            target.in_combat == true &&
            target.HasAffect(Affects.clear_movement) == false &&
            target.HasAffect(Affects.reduce) == false)
        {
            target = player.actions.target;
            _ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + target.name, player);
            _ovr024.add_affect(false, _ovr033.GetPlayerIndex(target), 0, Affects.clear_movement, target);

            _ovr013.CallAffectTable(Effect.Add, null, target, Affects.clear_movement);
            _ovr024.add_affect(false, _ovr024.roll_dice(4, 2), 0, Affects.reduce, target);
            _ovr024.add_affect(true, _ovr033.GetPlayerIndex(target), 0, Affects.affect_8b, player);
        }
    }
}
