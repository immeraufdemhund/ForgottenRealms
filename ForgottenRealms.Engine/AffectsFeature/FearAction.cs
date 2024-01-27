using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FearAction : IAffectAction
{
    public Affects ActionForAffect => Affects.fear;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
