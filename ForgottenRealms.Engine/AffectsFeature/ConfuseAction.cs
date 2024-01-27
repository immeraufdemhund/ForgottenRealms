using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ConfuseAction : IAffectAction
{
    public Affects ActionForAffect => Affects.confuse;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
