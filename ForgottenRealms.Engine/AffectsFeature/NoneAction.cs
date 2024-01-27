using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class NoneAction : IAffectAction
{
    public Affects ActionForAffect => Affects.none;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
