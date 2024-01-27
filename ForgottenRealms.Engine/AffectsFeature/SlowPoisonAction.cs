using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SlowPoisonAction : IAffectAction
{
    public Affects ActionForAffect => Affects.slow_poison;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
