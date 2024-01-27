using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CharmPersonAction : IAffectAction
{
    public Affects ActionForAffect => Affects.charm_person;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
