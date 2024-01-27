using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ReduceAction : IAffectAction
{
    public Affects ActionForAffect => Affects.reduce;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
