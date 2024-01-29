using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect75Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_75;

    private readonly ovr024 _ovr024;
    public Affect75Action(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item item = gbl.SelectedPlayer.activeItems.primaryWeapon;

        if (item != null && item.type == ItemType.Type_85)
        {
            gbl.damage = _ovr024.roll_dice_save(6, 1) + 1;
        }
    }
}
