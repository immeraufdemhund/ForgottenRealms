using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class StrengthSpellAction : IAffectAction
{
    public Affects ActionForAffect => Affects.strength_spell;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
