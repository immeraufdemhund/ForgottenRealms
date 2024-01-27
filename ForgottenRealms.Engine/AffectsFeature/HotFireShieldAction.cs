using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HotFireShieldAction : IAffectAction
{
    public Affects ActionForAffect => Affects.hot_fire_shield;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
