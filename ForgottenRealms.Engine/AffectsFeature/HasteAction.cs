using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HasteAction : IAffectAction
{
    public Affects ActionForAffect => Affects.haste;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
