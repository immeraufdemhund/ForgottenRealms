using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FireResistAction : IAffectAction
{
    public Affects ActionForAffect => Affects.fire_resist;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
