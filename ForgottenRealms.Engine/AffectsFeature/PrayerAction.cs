using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PrayerAction : IAffectAction
{
    public Affects ActionForAffect => Affects.prayer;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
