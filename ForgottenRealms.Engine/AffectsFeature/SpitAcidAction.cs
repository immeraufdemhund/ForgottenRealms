using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SpitAcidAction : IAffectAction
{
    public Affects ActionForAffect => Affects.spit_acid;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public SpitAcidAction(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.spell_41);

        gbl.spell_target = player.actions.target;

        int roll = _ovr024.roll_dice(100, 1);

        if (_ovr025.getTargetRange(gbl.spell_target, player) < 7 &&
            gbl.spell_target != null)
        {
            if (roll <= 30)
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);
                _ovr025.load_missile_icons(0x17);

                _ovr025.draw_missile_attack(30, 1, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

                _ovr024.damage_person(_ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, gbl.spell_target), DamageOnSave.Half, player.hit_point_max, gbl.spell_target);
            }
            else
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid and Misses", player);
            }
        }
    }
}
