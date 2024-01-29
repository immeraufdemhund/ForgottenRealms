using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect7BAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_7b;

    private readonly ovr024 _ovr024;
    public Affect7BAction(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        gbl.damage_flags = DamageType.Acid;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(8, 2), player.actions.target);
    }
}
