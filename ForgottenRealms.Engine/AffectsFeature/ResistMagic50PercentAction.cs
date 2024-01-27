using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistMagic50PercentAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_magic_50_percent;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
