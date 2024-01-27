using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DoItemsAffectAction : IAffectAction
{
    public Affects ActionForAffect => Affects.do_items_affect;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
