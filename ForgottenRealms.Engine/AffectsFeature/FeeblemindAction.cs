using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FeeblemindAction : IAffectAction
{
    public Affects ActionForAffect => Affects.feeblemind;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
