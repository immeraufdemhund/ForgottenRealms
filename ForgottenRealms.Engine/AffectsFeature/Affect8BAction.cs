using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect8BAction : IAffectAction
{
    private readonly AttackTargetAction _attackTargetAction;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;

    public Affects ActionForAffect => Affects.affect_8b;
    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        gbl.spell_target = gbl.player_array[affect.affect_data];

        if (effect == Effect.Remove ||
            player.in_combat == false ||
            gbl.spell_target.in_combat == false)
        {
            _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            _ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);

            if (effect == Effect.Add)
            {
                affect.callAffectTable = false;

                _ovr024.remove_affect(affect, Affects.affect_8b, player);
            }
        }
        else
        {
            player.attack1_AttacksLeft = 2;
            player.attack2_AttacksLeft = 0;
            player.attack1_DiceCount = 2;
            player.attack1_DiceSize = 8;

            _attackTargetAction.AttackTarget(null, 1, gbl.spell_target, player);

            _ovr025.clear_actions(player);

            if (gbl.spell_target.in_combat == false)
            {
                _ovr024.remove_affect(null, Affects.affect_8b, player);
                _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                _ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);
            }
        }
    }
}
