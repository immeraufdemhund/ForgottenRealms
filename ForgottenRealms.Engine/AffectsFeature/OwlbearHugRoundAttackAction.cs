using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class OwlbearHugRoundAttackAction : IAffectAction
{
    public Affects ActionForAffect => Affects.owlbear_hug_round_attack;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
