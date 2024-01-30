using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class OwlbearHugCheckAction : IAffectAction
{
    public Affects ActionForAffect => Affects.owlbear_hug_check;

    private readonly ovr013 _ovr013;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public OwlbearHugCheckAction(ovr013 ovr013, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr013 = ovr013;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        if (gbl.attack_roll >= 18)
        {
            gbl.spell_target = player.actions.target;
            _ovr025.DisplayPlayerStatusString(true, 12, "hugs " + gbl.spell_target.name, player);

            _ovr024.add_affect(false, _ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.clear_movement, gbl.spell_target);
            _ovr013.CallAffectTable(Effect.Add, null, gbl.spell_target, Affects.clear_movement);

            _ovr024.add_affect(true, _ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.owlbear_hug_round_attack, player);
        }
    }
}
