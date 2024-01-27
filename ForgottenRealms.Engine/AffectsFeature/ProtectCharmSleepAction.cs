using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtectCharmSleepAction : IAffectAction
{
    public Affects ActionForAffect => Affects.protect_charm_sleep;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
