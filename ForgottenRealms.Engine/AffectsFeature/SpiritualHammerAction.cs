using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SpiritualHammerAction : IAffectAction
{
    public Affects ActionForAffect => Affects.spiritual_hammer;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
