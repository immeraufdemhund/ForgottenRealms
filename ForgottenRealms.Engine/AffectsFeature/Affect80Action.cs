using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect80Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_80;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
