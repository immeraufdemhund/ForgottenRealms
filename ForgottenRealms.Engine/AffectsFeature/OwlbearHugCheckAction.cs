using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class OwlbearHugCheckAction : IAffectAction
{
    public Affects ActionForAffect => Affects.owlbear_hug_check;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
