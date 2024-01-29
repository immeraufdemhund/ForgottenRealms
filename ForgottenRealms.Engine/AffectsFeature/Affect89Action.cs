using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect89Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_89;

    private readonly ovr032 _ovr032;
    public Affect89Action(ovr032 ovr032)
    {
        _ovr032 = ovr032;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (effect == Effect.Add)
        {
            player.quick_fight = QuickFight.True;

            if (player.control_morale < Control.NPC_Base ||
                player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Berzerk;
            }
            else
            {
                player.control_morale = Control.NPC_Berzerk;
            }

            player.actions.target = null;

            var scl = _ovr032.Rebuild_SortedCombatantList(player, 0xff, p => true);

            player.actions.target = scl[0].player;
            player.combat_team = player.actions.target.OppositeTeam();
        }
        else
        {
            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = 0;
            }

            player.combat_team = (CombatTeam)affect.affect_data;
        }
    }
}
