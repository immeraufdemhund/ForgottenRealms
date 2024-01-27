using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SpDispelEvilAction : IAffectAction
{
    public Affects ActionForAffect => Affects.sp_dispel_evil;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
