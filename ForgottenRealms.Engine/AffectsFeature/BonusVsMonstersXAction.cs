using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BonusVsMonstersXAction : IAffectAction
{
    public Affects ActionForAffect => Affects.bonus_vs_monsters_x;
    public void Execute(Effect effect, object param, Player player)
    {
        int bonus = 0;

        if (player.actions != null &&
            player.actions.target != null)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.monsterType == MonsterType.troll)
            {
                bonus = 1;
            }
            else if (gbl.spell_target.monsterType == MonsterType.type_9 || gbl.spell_target.monsterType == MonsterType.type_12)
            {
                bonus = 2;
            }
            else if (gbl.spell_target.monsterType == MonsterType.animated_dead)
            {
                bonus = 3;
            }
            else
            {
                bonus = 0;
            }
        }

        gbl.attack_roll += bonus;
        gbl.damage += bonus;
        gbl.damage_flags = DamageType.Magic | DamageType.Fire;
    }
}
