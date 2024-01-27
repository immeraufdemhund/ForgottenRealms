using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class CursedAction : IAffectAction
{
    public Affects ActionForAffect => Affects.cursed;
    public void Execute(Effect effect, object param, Player player)
    {
        if (gbl.monster_morale < 5)
        {
            gbl.monster_morale = 0;
        }
        else
        {
            gbl.monster_morale -= 5;
        }

        gbl.attack_roll--;
    }
}
