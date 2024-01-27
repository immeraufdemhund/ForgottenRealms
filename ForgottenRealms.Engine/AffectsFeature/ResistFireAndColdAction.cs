using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistFireAndColdAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_fire_and_cold;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
