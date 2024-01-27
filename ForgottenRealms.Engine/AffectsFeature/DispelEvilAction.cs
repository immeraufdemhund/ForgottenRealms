using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DispelEvilAction : IAffectAction
{
    public Affects ActionForAffect => Affects.dispel_evil;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
