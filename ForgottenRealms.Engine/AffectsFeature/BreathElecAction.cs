using System;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BreathElecAction : IAffectAction
{
    public Affects ActionForAffect => Affects.breath_elec;

    private readonly ElectricalDamageMath _electricalDamageMath;
    private readonly Subroutine5FA44 _subroutine5Fa44;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public BreathElecAction(ElectricalDamageMath electricalDamageMath, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033, Subroutine5FA44 subroutine5Fa44)
    {
        _electricalDamageMath = electricalDamageMath;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
        _subroutine5Fa44 = subroutine5Fa44;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (gbl.combat_round == 0 ||
            _ovr024.roll_dice(100, 1) > 50)
        {
            gbl.damage_flags = DamageType.DragonBreath | DamageType.Electricity;
            var var_2 = _ovr033.PlayerMapPos(player);

            _ovr025.DisplayPlayerStatusString(true, 10, "Breathes!", player);

            gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.lightning_bolt);

            gbl.targetPos.x = var_2.x + Math.Sign(gbl.targetPos.x - var_2.x);
            gbl.targetPos.y = var_2.y + Math.Sign(gbl.targetPos.y - var_2.y);

            if (gbl.targetPos.x == (var_2.x + 1))
            {
                gbl.targetPos.x++;
            }

            if (gbl.targetPos.y == (var_2.y + 1))
            {
                gbl.targetPos.y++;
            }

            _ovr024.remove_invisibility(player);
            _ovr025.load_missile_icons(0x13);

            _ovr025.draw_missile_attack(0x32, 4, gbl.targetPos, var_2);
            _electricalDamageMath.DoElecDamage(false, 0, SaveVerseType.BreathWeapon, player.hit_point_max, gbl.targetPos);
            _subroutine5Fa44.sub_5FA44(0, SaveVerseType.BreathWeapon, player.hit_point_max, 10);

            if (affect.affect_data > 0xFD)
            {
                affect.affect_data -= 1;
            }
            else
            {
                _ovr024.remove_affect(affect, Affects.breath_elec, player);
            }
            _ovr025.clear_actions(player);
        }
    }
}
