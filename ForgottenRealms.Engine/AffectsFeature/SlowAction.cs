using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SlowAction : IAffectAction
{
    public Affects ActionForAffect => Affects.slow;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
