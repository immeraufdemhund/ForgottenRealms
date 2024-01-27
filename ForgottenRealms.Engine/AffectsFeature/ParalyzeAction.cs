using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ParalyzeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.paralyze;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
