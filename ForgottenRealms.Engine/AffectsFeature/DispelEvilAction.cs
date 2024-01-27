using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DispelEvilAction : IAffectAction
{
    public Affects ActionForAffect => Affects.dispel_evil;
    public void Execute(Effect effect, object param, Player player)
    {
        if ((gbl.SelectedPlayer.field_14B & 1) != 0)
        {
            gbl.attack_roll -= 7;
        }
    }
}
