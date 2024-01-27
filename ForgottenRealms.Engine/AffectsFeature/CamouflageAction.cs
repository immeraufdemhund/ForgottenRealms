using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CamouflageAction : IAffectAction
{
    public Affects ActionForAffect => Affects.camouflage;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
