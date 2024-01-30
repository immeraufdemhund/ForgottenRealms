using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BreathAcidAction : IAffectAction
{
    public Affects ActionForAffect => Affects.breath_acid;

    private readonly AreaDamageTargetsBuilder _areaDamageTargetsBuilder;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public BreathAcidAction(AreaDamageTargetsBuilder areaDamageTargetsBuilder, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _areaDamageTargetsBuilder = areaDamageTargetsBuilder;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        gbl.byte_1DA70 = false;

        if (gbl.combat_round == 0)
        {
            affect.affect_data = 3;
        }

        if (affect.affect_data > 0)
        {
            gbl.damage_flags = DamageType.DragonBreath | DamageType.Acid;

            var attackerPos = _ovr033.PlayerMapPos(player);

            gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.spell_3d);

            if (gbl.byte_1DA70 == true)
            {
                _areaDamageTargetsBuilder.BuildAreaDamageTargets(6, 1, gbl.targetPos, attackerPos);
            }

            if (gbl.spellTargets.Exists(target => player.OppositeTeam() == target.combat_team))
            {
                gbl.byte_1DA70 = false;
            }

            if (gbl.byte_1DA70 == true &&
                gbl.spellTargets.Count > 0)
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "breathes acid", player);
                _ovr025.load_missile_icons(0x12);

                _ovr025.draw_missile_attack(0x1E, 1, _ovr033.PlayerMapPos(gbl.spellTargets[0]), _ovr033.PlayerMapPos(player));

                foreach (var target in gbl.spellTargets)
                {
                    bool save_made = _ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, target);
                    _ovr024.damage_person(save_made, DamageOnSave.Half, player.hit_point_max, target);
                }

                affect.affect_data--;

                _ovr025.clear_actions(player);
            }
        }
    }
}
