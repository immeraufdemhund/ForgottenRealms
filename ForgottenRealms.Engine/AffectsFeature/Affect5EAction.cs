using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect5EAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_5e;
    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    public Affect5EAction(PlayerPrimaryWeapon playerPrimaryWeapon)
    {
        _playerPrimaryWeapon = playerPrimaryWeapon;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item item = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if (item != null &&
            (gbl.ItemDataTable[item.type].field_7 & 0x81) != 0)
        {
            gbl.damage /= 2;
        }
    }
}
