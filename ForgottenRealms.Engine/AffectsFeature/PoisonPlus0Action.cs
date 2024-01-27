using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonPlus0Action : IAffectAction
{
    public Affects ActionForAffect => Affects.poison_plus_0;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
