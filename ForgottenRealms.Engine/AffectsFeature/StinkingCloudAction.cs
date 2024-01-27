using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class StinkingCloudAction : IAffectAction
{
    public Affects ActionForAffect => Affects.stinking_cloud;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
