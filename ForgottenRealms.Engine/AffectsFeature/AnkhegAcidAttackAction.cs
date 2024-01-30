using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AnkhegAcidAttackAction : IAffectAction
{
    public Affects ActionForAffect => Affects.ankheg_acid_attack;

    private readonly ovr024 _ovr024;
    public AnkhegAcidAttackAction(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        gbl.damage_flags = DamageType.Acid;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(4, 1), player.actions.target);
    }
}
