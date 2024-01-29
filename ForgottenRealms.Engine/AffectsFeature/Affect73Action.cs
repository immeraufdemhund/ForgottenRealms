using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect73Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_73;

    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    public Affect73Action(PlayerPrimaryWeapon playerPrimaryWeapon)
    {
        _playerPrimaryWeapon = playerPrimaryWeapon;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item weapon = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if (weapon != null)
        {
            if (gbl.ItemDataTable[weapon.type].field_7 == 0 ||
                (gbl.ItemDataTable[weapon.type].field_7 & 1) != 0)
            {
                gbl.damage /= 2;
            }
        }
    }
}
