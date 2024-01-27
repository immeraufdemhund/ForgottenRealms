using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SticksToSnakesAction : IAffectAction
{
    public Affects ActionForAffect => Affects.sticks_to_snakes;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
