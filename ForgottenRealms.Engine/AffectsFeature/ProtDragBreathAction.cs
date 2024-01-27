using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ProtDragBreathAction : IAffectAction
{
    public Affects ActionForAffect => Affects.prot_drag_breath;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
