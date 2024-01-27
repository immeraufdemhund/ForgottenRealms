using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class WeapDragonSlayerAction : IAffectAction
{
    public Affects ActionForAffect => Affects.weap_dragon_slayer;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
