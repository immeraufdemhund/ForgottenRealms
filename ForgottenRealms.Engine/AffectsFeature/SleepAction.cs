using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SleepAction : IAffectAction
{
    public Affects ActionForAffect => Affects.sleep;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
