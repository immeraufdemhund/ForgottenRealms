using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AnimateDeadAction : IAffectAction
{
    public Affects ActionForAffect => Affects.animate_dead;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
