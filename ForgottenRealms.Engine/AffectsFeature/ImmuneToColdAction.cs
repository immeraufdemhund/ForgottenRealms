using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ImmuneToColdAction : IAffectAction
{
    public Affects ActionForAffect => Affects.immune_to_cold;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
