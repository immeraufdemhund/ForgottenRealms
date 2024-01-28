using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect80Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_80;

    private readonly AreaDamageTargetsBuilder _areaDamageTargetsBuilder;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public Affect80Action(AreaDamageTargetsBuilder areaDamageTargetsBuilder, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _areaDamageTargetsBuilder = areaDamageTargetsBuilder;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }
    public void Execute(Effect effect, object param, Player player)
    {
        DragonBreathFire(effect, param, player);
    }
    private void DragonBreathFire(Effect arg_0, object param, Player attacker) // spell_breathes_fire
    {
        Affect affect = (Affect)param;

        if (gbl.combat_round == 0)
        {
            affect.affect_data = 3;
        }

        if (affect.affect_data > 0)
        {
            gbl.damage_flags = DamageType.DragonBreath | DamageType.Fire;
            var attackPos = _ovr033.PlayerMapPos(attacker);

            gbl.byte_1DA70 = gbl.SpellCastFunction(QuickFight.True, (int)Spells.spell_3d);

            if (gbl.byte_1DA70 == true)
            {
                _areaDamageTargetsBuilder.BuildAreaDamageTargets(9, 3, gbl.targetPos, attackPos);

                if (gbl.spellTargets.Count > 0)
                {
                    _ovr025.DisplayPlayerStatusString(true, 10, "breathes fire", attacker);
                    _ovr025.load_missile_icons(0x12);

                    _ovr025.draw_missile_attack(0x1E, 1, _ovr033.PlayerMapPos(gbl.spellTargets[0]), _ovr033.PlayerMapPos(attacker));

                    foreach (var target in gbl.spellTargets)
                    {
                        bool saves = _ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, target);

                        _ovr024.damage_person(saves, DamageOnSave.Half, attacker.hit_point_max, target);
                    }

                    affect.affect_data -= 1;
                    _ovr025.clear_actions(attacker);
                }
            }
        }
    }
}
