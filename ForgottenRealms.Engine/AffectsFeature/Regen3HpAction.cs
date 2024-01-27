using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Regen3HpAction : IAffectAction
{
    public Affects ActionForAffect => Affects.regen_3_hp;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
