using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CauseDisease2Action : IAffectAction
{
    public Affects ActionForAffect => Affects.cause_disease_2;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
