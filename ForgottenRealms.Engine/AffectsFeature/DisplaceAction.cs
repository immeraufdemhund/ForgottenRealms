using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DisplaceAction : IAffectAction
{
    public Affects ActionForAffect => Affects.displace;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
