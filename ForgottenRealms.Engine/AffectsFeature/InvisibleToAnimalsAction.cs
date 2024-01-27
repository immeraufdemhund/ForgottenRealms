using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class InvisibleToAnimalsAction : IAffectAction
{
    public Affects ActionForAffect => Affects.invisible_to_animals;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
