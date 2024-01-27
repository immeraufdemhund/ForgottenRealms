using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonPlus2Action : IAffectAction
{
    public Affects ActionForAffect => Affects.poison_plus_2;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
