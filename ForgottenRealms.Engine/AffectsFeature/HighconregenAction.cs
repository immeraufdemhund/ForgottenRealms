using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HighconregenAction : IAffectAction
{
    public Affects ActionForAffect => Affects.highConRegen;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
