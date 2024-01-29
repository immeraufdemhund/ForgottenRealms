using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class AvoidMissleAttackAction
{
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    public AvoidMissleAttackAction(ovr024 ovr024, ovr025 ovr025)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public void AvoidMissleAttack(int percentage, Player player)
    {
        if (gbl.SelectedPlayer.activeItems.primaryWeapon != null &&
            _ovr025.getTargetRange(player, gbl.SelectedPlayer) == 0 &&
            _ovr024.roll_dice(100, 1) <= percentage)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "Avoids it", player);
            gbl.damage = 0;
            gbl.attack_roll = -1;
            gbl.bytes_1D2C9[1] -= 1;
        }
    }
}
