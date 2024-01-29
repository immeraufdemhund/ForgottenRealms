using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect72Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_72;
    public void Execute(Effect effect, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            gbl.damage /= 2;
        }
    }
}
