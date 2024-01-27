using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class TrollRegenAction : IAffectAction
{
    public Affects ActionForAffect => Affects.TrollRegen;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
