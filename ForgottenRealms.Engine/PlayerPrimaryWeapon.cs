using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class PlayerPrimaryWeapon
{
    private readonly ovr025 _ovr025;
    public PlayerPrimaryWeapon(ovr025 ovr025)
    {
        _ovr025 = ovr025;
    }

    public Item get_primary_weapon(Player player)
    {
        Item item = null;

        if (player.activeItems.primaryWeapon != null)
        {
            bool item_found = _ovr025.GetCurrentAttackItem(out item, player);

            if (item_found == false || item == null)
            {
                item = player.activeItems.primaryWeapon;
            }
        }

        return item;
    }
}
