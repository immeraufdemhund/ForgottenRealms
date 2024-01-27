using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BreathAcidAction : IAffectAction
{
    public Affects ActionForAffect => Affects.breath_acid;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
