using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtectMagicAction : IAffectAction
{
    public Affects ActionForAffect => Affects.protect_magic;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
