using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtFromGood10RadiusAction : IAffectAction
{
    public Affects ActionForAffect => Affects.prot_from_good_10_radius;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
