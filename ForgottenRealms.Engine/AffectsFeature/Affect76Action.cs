using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect76Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_76;
    public void Execute(Effect effect, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            gbl.damage /= 2;
        }
    }
}
