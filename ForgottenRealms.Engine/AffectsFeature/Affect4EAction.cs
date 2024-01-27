using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect4EAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_4e;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
