using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CastThrowLighteningAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cast_throw_lightening;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    private readonly ElectricalDamageMath _electricalDamageMath;
    private readonly Subroutine5FA44 _subroutine5FA44;
    public CastThrowLighteningAction(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033, ElectricalDamageMath electricalDamageMath, Subroutine5FA44 subroutine5Fa44)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
        _electricalDamageMath = electricalDamageMath;
        _subroutine5FA44 = subroutine5Fa44;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        bool var_1 = false; /* Simeon */

        if (gbl.combat_round < 4)
        {
            var pos = _ovr033.PlayerMapPos(player);

            _ovr025.DisplayPlayerStatusString(true, 10, "throws lightning", player);
            gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.lightning_bolt);

            _ovr024.remove_invisibility(player);
            _ovr025.load_missile_icons(0x13);
            _ovr025.draw_missile_attack(0x32, 4, gbl.targetPos, pos);

            var_1 = _electricalDamageMath.DoElecDamage(var_1, 0, SaveVerseType.Spell, _ovr024.roll_dice_save(6, 16), gbl.targetPos);
            _subroutine5FA44.sub_5FA44(0, 0, _ovr024.roll_dice_save(6, 16), 10);
            var_1 = true;
            _ovr025.clear_actions(player);
        }
    }
}
