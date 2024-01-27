using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class PoisonDamageAction : IAffectAction
{
    public Affects ActionForAffect => Affects.poison_damage;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
