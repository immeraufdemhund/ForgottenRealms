using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect30Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_30;
    public void Execute(Effect effect, object param, Player player)
    {
        if (gbl.SelectedPlayer.monsterType == MonsterType.type_1 &&
            (gbl.SelectedPlayer.field_DE & 0x7F) == 2)
        {
            gbl.attack_roll -= 4;
        }
    }
}
