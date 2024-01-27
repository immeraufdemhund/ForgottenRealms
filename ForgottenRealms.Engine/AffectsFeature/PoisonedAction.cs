using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonedAction : IAffectAction
{
    public Affects ActionForAffect => Affects.poisoned;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
