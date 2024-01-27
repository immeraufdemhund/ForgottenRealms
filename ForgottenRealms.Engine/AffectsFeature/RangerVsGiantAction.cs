using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class RangerVsGiantAction : IAffectAction
{
    public Affects ActionForAffect => Affects.ranger_vs_giant;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
