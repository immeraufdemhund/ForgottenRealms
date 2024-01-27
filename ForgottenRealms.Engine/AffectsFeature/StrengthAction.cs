using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class StrengthAction : IAffectAction
{
    public Affects ActionForAffect => Affects.strength;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
