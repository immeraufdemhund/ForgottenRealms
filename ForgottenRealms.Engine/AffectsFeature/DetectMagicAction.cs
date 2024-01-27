using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DetectMagicAction : IAffectAction
{
    public Affects ActionForAffect => Affects.detect_magic;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
