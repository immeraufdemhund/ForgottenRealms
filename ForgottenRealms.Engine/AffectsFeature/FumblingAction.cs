using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FumblingAction : IAffectAction
{
    public Affects ActionForAffect => Affects.fumbling;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
