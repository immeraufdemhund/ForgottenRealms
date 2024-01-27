using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BlindedAction : IAffectAction
{
    public Affects ActionForAffect => Affects.blinded;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
