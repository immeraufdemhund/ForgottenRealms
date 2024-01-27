using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PaladindailycurerefreshAction : IAffectAction
{
    public Affects ActionForAffect => Affects.paladinDailyCureRefresh;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
