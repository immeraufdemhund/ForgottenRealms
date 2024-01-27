using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ItemInvisibilityAction : IAffectAction
{
    public Affects ActionForAffect => Affects.item_invisibility;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
