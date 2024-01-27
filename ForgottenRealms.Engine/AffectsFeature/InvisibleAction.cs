using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class InvisibleAction : IAffectAction
{
    public Affects ActionForAffect => Affects.invisible;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
