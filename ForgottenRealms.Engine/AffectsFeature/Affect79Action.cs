using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect79Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_79;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public Affect79Action(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        gbl.spell_target = player.actions.target;

        if (_ovr024.roll_dice(100, 1) <= 25)
        {
            if (_ovr025.getTargetRange(gbl.spell_target, player) < 4)
            {
                _ovr025.clear_actions(player);

                _ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);

                _ovr025.load_missile_icons(0x17);

                _ovr025.draw_missile_attack(0x1e, 1, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

                int damage = _ovr024.roll_dice_save(4, 8);
                bool saved = _ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, gbl.spell_target);

                _ovr024.damage_person(saved, DamageOnSave.Half, damage, gbl.spell_target);

                _ovr024.remove_affect(affect, Affects.affect_79, player);
                _ovr024.remove_affect(null, Affects.ankheg_acid_attack, player);
            }
        }
    }
}
