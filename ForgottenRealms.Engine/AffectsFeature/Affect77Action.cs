using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect77Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_77;

    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    public Affect77Action(PlayerPrimaryWeapon playerPrimaryWeapon)
    {
        _playerPrimaryWeapon = playerPrimaryWeapon;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item weapon = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if ((weapon == null || weapon.plus == 0) &&
            (gbl.SelectedPlayer.race > 0 || gbl.SelectedPlayer.HitDice < 4))
        {
            gbl.damage = 0;
        }
    }
}
