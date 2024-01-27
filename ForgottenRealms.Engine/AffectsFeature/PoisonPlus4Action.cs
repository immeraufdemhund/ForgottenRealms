using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonPlus4Action : IAffectAction
{
    public Affects ActionForAffect => Affects.poison_plus_4;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
