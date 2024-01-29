using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect71Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_71;
    public void Execute(Effect effect, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            for (int i = 1; i <= gbl.dice_count; i++)
            {
                gbl.damage--;

                if (gbl.damage < gbl.dice_count)
                {
                    gbl.damage = gbl.dice_count;
                }
            }
        }
    }
}
