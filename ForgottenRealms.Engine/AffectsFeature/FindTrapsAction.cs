using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FindTrapsAction : IAffectAction
{
    public Affects ActionForAffect => Affects.find_traps;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
