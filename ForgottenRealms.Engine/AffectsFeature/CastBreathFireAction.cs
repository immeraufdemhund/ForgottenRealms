using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CastBreathFireAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cast_breath_fire;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
