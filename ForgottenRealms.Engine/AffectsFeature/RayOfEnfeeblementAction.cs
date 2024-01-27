using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class RayOfEnfeeblementAction : IAffectAction
{
    public Affects ActionForAffect => Affects.ray_of_enfeeblement;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
