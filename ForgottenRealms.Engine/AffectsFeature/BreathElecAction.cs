using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BreathElecAction : IAffectAction
{
    public Affects ActionForAffect => Affects.breath_elec;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
