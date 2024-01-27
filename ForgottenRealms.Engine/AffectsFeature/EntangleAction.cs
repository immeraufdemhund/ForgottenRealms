using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class EntangleAction : IAffectAction
{
    public Affects ActionForAffect => Affects.entangle;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
