using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtectionFromEvilAction : IAffectAction
{
    public Affects ActionForAffect => Affects.protection_from_evil;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
