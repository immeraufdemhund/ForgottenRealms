using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class InvisibilityAction : IAffectAction
{
    public Affects ActionForAffect => Affects.invisibility;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
