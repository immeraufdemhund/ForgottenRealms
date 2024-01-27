using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BestowCurseAction : IAffectAction
{
    public Affects ActionForAffect => Affects.bestow_curse;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
