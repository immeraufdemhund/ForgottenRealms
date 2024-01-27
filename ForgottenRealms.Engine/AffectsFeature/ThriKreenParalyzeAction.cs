using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ThriKreenParalyzeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.thri_kreen_paralyze;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
