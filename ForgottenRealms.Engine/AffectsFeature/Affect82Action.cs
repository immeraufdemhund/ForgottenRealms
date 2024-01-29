using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect82Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_82;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public Affect82Action(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Item item;

        if (_ovr025.GetCurrentAttackItem(out item, gbl.SelectedPlayer) == true &&
            item != null &&
            item.type == ItemType.Quarrel &&
            item.namenum3 == 0x87)
        {
            player.health_status = Status.gone;
            player.in_combat = false;
            player.hit_point_current = 0;
            _ovr024.RemoveCombatAffects(player);
            _ovr024.CheckAffectsEffect(player, CheckType.Death);

            if (player.in_combat == true)
            {
                _ovr033.CombatantKilled(player);
            }
        }
    }
}
