using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ResistFireAction : IAffectAction
{
    public Affects ActionForAffect => Affects.resist_fire;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
