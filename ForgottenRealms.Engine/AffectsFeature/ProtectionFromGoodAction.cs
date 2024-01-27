using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtectionFromGoodAction : IAffectAction
{
    public Affects ActionForAffect => Affects.protection_from_good;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
