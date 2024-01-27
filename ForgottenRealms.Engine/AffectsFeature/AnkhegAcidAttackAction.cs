using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class AnkhegAcidAttackAction : IAffectAction
{
    public Affects ActionForAffect => Affects.ankheg_acid_attack;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
