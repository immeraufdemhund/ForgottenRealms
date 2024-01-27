using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BlinkAction : IAffectAction
{
    public Affects ActionForAffect => Affects.blink;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
