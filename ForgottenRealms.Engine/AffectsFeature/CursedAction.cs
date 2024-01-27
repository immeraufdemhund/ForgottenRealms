using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CursedAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cursed;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
