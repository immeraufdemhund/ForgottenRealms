using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistColdAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_cold;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
