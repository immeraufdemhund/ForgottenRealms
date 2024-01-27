using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonNeg2Action : IAffectAction
{
    public Affects ActionForAffect => Affects.poison_neg_2;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
