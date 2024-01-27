using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ShieldAction : IAffectAction
{
    public Affects ActionForAffect => Affects.shield;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
