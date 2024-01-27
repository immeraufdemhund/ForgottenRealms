using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HalfDamageAction : IAffectAction
{
    public Affects ActionForAffect => Affects.half_damage;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
