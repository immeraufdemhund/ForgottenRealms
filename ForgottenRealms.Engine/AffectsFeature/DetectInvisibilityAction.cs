using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DetectInvisibilityAction : IAffectAction
{
    public Affects ActionForAffect => Affects.detect_invisibility;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
