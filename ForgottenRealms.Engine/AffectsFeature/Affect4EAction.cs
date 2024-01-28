using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect4EAction : IAffectAction
{
    private readonly ovr024 _ovr024;
    public Affects ActionForAffect => Affects.affect_4e;
    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (_ovr024.combat_heal(player.hit_point_current, player) == false)
        {
            AddAffect(1, affect.affect_data, Affects.affect_4e, player);
        }
    }
    private void AddAffect(ushort time, int data, Affects affectType, Player player)
    {
        if (gbl.cureSpell == true)
        {
            return;
        }

        _ovr024.add_affect(true, data, time, affectType, player);
    }
}
