using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect5EAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_5e;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
