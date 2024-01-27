using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect5DAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_5d;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
