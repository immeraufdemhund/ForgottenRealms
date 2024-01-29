using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect78Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_78;

    private readonly AvoidMissleAttackAction _avoidMissleAttackAction;
    public Affect78Action(AvoidMissleAttackAction avoidMissleAttackAction)
    {
        _avoidMissleAttackAction = avoidMissleAttackAction;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item field_151 = player.activeItems.primaryWeapon;

        if (field_151 != null)
        {
            if (field_151.type == ItemType.Type_87 || field_151.type == ItemType.Type_88)
            {
                _avoidMissleAttackAction.AvoidMissleAttack(50, player);
            }
        }
    }
}
