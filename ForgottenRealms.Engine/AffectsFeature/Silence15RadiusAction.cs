using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Silence15RadiusAction : IAffectAction
{
    public Affects ActionForAffect => Affects.silence_15_radius;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
