using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class OwlbearHugRoundAttackAction : IAffectAction
{
    public Affects ActionForAffect => Affects.owlbear_hug_round_attack;

    private readonly AttackTargetAction _attackTargetAction;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;

    public OwlbearHugRoundAttackAction(AttackTargetAction attackTargetAction, ovr024 ovr024, ovr025 ovr025)
    {
        _attackTargetAction = attackTargetAction;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        var affect = (Affect)param;

        gbl.spell_target = gbl.player_array[affect.affect_data];

        if (effect == Effect.Remove ||
            player.in_combat == false ||
            gbl.spell_target.in_combat == false)
        {
            _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            if (effect == Effect.Add)
            {
                affect.callAffectTable = false;
                _ovr024.remove_affect(affect, Affects.owlbear_hug_round_attack, player);
            }
        }
        else
        {
            player.attack1_AttacksLeft = 1;
            player.attack2_AttacksLeft = 0;
            player.attack1_DiceCount = 2;
            player.attack1_DiceSize = 8;


            _attackTargetAction.AttackTarget(null, 2, gbl.spell_target, player);

            _ovr025.clear_actions(player);

            if (gbl.spell_target.in_combat == false)
            {
                _ovr024.remove_affect(null, Affects.owlbear_hug_round_attack, player);
                _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            }
        }
    }
}
