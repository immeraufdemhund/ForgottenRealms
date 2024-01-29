using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect74Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_74;

    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    public Affect74Action(PlayerPrimaryWeapon playerPrimaryWeapon)
    {
        _playerPrimaryWeapon = playerPrimaryWeapon;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item weapon = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if (weapon != null &&
            weapon.plus > 0)
        {
            gbl.damage /= 2;
        }
    }
}
