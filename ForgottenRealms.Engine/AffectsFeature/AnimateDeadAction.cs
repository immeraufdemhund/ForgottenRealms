using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AnimateDeadAction : IAffectAction
{
    public Affects ActionForAffect => Affects.animate_dead;

    private readonly ovr024 _ovr024;
    public AnimateDeadAction(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        affect.callAffectTable = false;

        if (gbl.cureSpell == false)
        {
            _ovr024.KillPlayer("collapses", Status.dead, player);
        }

        player.combat_team = (CombatTeam)(affect.affect_data >> 4);
        player.quick_fight = QuickFight.True;
        player.field_E9 = 0;

        player.attackLevel = (byte)player.SkillLevel(SkillType.Fighter);
        player.base_movement = 0x0C;

        if (player.control_morale == Control.PC_Berzerk)
        {
            player.control_morale = Control.PC_Base;
        }

        player.monsterType = 0;
    }
}
