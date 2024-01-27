using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PaladindailyhealcastAction : IAffectAction
{
    public Affects ActionForAffect => Affects.paladinDailyHealCast;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
