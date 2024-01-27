using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class RegenerateAction : IAffectAction
{
    public Affects ActionForAffect => Affects.regenerate;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
