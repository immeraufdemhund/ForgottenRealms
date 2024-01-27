using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class MirrorImageAction : IAffectAction
{
    public Affects ActionForAffect => Affects.mirror_image;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
