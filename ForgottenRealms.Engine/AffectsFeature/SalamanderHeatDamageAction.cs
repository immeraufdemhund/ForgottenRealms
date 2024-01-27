using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SalamanderHeatDamageAction : IAffectAction
{
    public Affects ActionForAffect => Affects.salamander_heat_damage;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
