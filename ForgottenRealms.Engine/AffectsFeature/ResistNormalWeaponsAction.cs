using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistNormalWeaponsAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_normal_weapons;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
