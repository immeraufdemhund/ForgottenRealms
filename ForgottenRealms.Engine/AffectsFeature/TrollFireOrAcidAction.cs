using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class TrollFireOrAcidAction : IAffectAction
{
    public Affects ActionForAffect => Affects.troll_fire_or_acid;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
