using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CauseDisease1Action : IAffectAction
{
    public Affects ActionForAffect => Affects.cause_disease_1;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
