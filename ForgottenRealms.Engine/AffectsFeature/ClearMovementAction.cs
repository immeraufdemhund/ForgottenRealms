using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ClearMovementAction : IAffectAction
{
    public Affects ActionForAffect => Affects.clear_movement;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
