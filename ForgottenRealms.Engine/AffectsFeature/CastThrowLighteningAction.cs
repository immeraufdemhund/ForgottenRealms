using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CastThrowLighteningAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cast_throw_lightening;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
