using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class BlessAction : IAffectAction
{
    public Affects ActionForAffect => Affects.bless;
    public void Execute(Effect effect, object affect, Player player)
    {
        gbl.monster_morale += 5;
        gbl.attack_roll++;
    }
}
