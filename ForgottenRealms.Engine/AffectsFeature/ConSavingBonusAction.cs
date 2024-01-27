using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ConSavingBonusAction : IAffectAction
{
    public Affects ActionForAffect => Affects.con_saving_bonus;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
