using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtectElecAction : IAffectAction
{
    public Affects ActionForAffect => Affects.protect_elec;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
