using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtFromNormalMissilesAction : IAffectAction
{
    public Affects ActionForAffect => Affects.prot_from_normal_missiles;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
