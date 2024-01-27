using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AffectInStinkingCloudAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_in_stinking_cloud;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
