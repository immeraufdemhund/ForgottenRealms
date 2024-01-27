using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistMagic15PercentAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_magic_15_percent;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
