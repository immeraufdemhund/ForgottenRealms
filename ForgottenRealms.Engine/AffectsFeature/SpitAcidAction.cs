using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SpitAcidAction : IAffectAction
{
    public Affects ActionForAffect => Affects.spit_acid;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
