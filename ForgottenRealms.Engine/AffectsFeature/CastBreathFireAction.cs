using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CastBreathFireAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cast_breath_fire;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public CastBreathFireAction(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.spell_41);
        gbl.spell_target = player.actions.target;

        if ((gbl.spell_target != null) &&
            (_ovr024.roll_dice(100, 1) <= 50) &&
            _ovr025.getTargetRange(gbl.spell_target, player) < 2)
        {
            gbl.damage_flags = DamageType.Fire;
            gbl.byte_1DA70 = true;
            _ovr025.clear_actions(player);

            _ovr025.DisplayPlayerStatusString(true, 10, "Breathes Fire", player);
            _ovr025.load_missile_icons(0x17);

            _ovr025.draw_missile_attack(0x1E, 1, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

            _ovr024.damage_person(_ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, gbl.spell_target), DamageOnSave.Half, 7, gbl.spell_target);
        }
    }
}
