using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HelplessAction : IAffectAction
{
    public Affects ActionForAffect => Affects.helpless;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
