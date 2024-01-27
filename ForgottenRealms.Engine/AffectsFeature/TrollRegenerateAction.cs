using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class TrollRegenerateAction : IAffectAction
{
    public Affects ActionForAffect => Affects.troll_regenerate;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
