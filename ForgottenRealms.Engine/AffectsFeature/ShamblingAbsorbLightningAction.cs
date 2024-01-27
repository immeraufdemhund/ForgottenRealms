using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ShamblingAbsorbLightningAction : IAffectAction
{
    public Affects ActionForAffect => Affects.shambling_absorb_lightning;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
