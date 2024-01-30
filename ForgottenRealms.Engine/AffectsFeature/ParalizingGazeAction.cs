using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ParalizingGazeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.paralizing_gaze;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public ParalizingGazeAction(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        player.actions.target = null;

        gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.spell_41);

        if (player.actions.target != null)
        {
            gbl.spell_target = player.actions.target;

            _ovr025.DisplayPlayerStatusString(false, 10, "gazes...", player);
            _ovr025.load_missile_icons(0x12);

            _ovr025.draw_missile_attack(0x2d, 4, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

            if (player.HasAffect(Affects.affect_7f) == true)
            {
                Item item = gbl.spell_target.items.Find(i => i.readied && (i.namenum1 == 0x76 || i.namenum2 == 0x76 || i.namenum3 == 0x76));

                if (item != null)
                {
                    _ovr025.DisplayPlayerStatusString(false, 12, "reflects it!", gbl.spell_target);

                    _ovr025.draw_missile_attack(0x2d, 4, _ovr033.PlayerMapPos(player), _ovr033.PlayerMapPos(gbl.spell_target));
                    gbl.spell_target = player;
                }
            }

            if (_ovr024.RollSavingThrow(0, SaveVerseType.Petrification, gbl.spell_target) == false)
            {
                _ovr024.KillPlayer("is Stoned", Status.stoned, gbl.spell_target);
            }
        }
    }
}
