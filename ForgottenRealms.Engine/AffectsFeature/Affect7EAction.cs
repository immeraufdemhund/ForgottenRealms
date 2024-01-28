using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect7EAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_7e;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;

    public Affect7EAction(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        player.actions.target = null;

        gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.animate_dead);

        gbl.spell_target = player.actions.target;

        if (gbl.spell_target != null)
        {
            _ovr025.DisplayPlayerStatusString(false, 10, "gazes...", player);

            _ovr025.load_missile_icons(0x12);

            _ovr025.draw_missile_attack(0x2d, 4, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

            if (_ovr024.RollSavingThrow(0, SaveVerseType.Petrification, gbl.spell_target) == false)
            {
                _ovr024.add_affect(false, 0xff, 0x3c, Affects.paralyze, gbl.spell_target);
                _ovr025.DisplayPlayerStatusString(false, 10, "is paralyzed", gbl.spell_target);
            }
        }
    }
}
