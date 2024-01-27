using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AffectInCloudKillAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_in_cloud_kill;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
