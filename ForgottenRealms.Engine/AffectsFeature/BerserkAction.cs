using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BerserkAction : IAffectAction
{
    public Affects ActionForAffect => Affects.berserk;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
