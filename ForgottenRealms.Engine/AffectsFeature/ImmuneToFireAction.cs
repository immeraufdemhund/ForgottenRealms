using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ImmuneToFireAction : IAffectAction
{
    public Affects ActionForAffect => Affects.immune_to_fire;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
