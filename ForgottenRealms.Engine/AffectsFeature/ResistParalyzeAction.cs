using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistParalyzeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_paralyze;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
