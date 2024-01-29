using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect55Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_55;
    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    public Affect55Action(PlayerPrimaryWeapon playerPrimaryWeapon)
    {
        _playerPrimaryWeapon = playerPrimaryWeapon;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item item = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if (item != null &&
            gbl.ItemDataTable[item.type].field_7 == 1)
        {
            gbl.damage = 1;
        }
    }
}
