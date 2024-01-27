using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class EnlargeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.enlarge;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
