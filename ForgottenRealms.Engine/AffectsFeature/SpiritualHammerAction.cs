using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SpiritualHammerAction : IAffectAction
{
    public Affects ActionForAffect => Affects.spiritual_hammer;

    private readonly ovr020 _ovr020;
    private readonly ovr025 _ovr025;
    public SpiritualHammerAction(ovr020 ovr020, ovr025 ovr025)
    {
        _ovr020 = ovr020;
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item item = player.items.Find(i => i.type == ItemType.Hammer && i.namenum3 == 0xf3);
        bool item_found = item != null;

        if (effect == Effect.Remove && item != null)
        {
            _ovr025.lose_item(item, player);
        }

        if (effect == Effect.Add &&
            item_found == false &&
            player.items.Count < Player.MaxItems)
        {
            item = new Item(Affects.affect_78, Affects.spiritual_hammer, 0, 0, 0, 0, false, 0, false, 0, 1, 243, 20, 0, ItemType.Hammer, true);

            player.items.Add(item);
            _ovr020.ready_Item(item);

            _ovr025.DisplayPlayerStatusString(true, 10, "Gains an item", player);
        }

        _ovr025.reclac_player_values(player);
    }
}
