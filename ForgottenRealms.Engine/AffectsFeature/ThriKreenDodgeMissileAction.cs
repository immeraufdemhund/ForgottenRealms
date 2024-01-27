using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ThriKreenDodgeMissileAction : IAffectAction
{
    public Affects ActionForAffect => Affects.thri_kreen_dodge_missile;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
