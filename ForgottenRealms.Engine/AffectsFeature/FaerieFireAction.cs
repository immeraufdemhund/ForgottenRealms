using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FaerieFireAction : IAffectAction
{
    public Affects ActionForAffect => Affects.faerie_fire;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
