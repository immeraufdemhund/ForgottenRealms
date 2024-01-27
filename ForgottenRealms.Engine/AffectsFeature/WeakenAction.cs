using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class WeakenAction : IAffectAction
{
    public Affects ActionForAffect => Affects.weaken;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
