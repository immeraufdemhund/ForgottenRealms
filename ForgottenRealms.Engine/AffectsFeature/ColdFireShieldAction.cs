using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ColdFireShieldAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cold_fire_shield;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
