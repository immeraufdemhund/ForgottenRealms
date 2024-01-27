using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DracolichParalysisAction : IAffectAction
{
    public Affects ActionForAffect => Affects.dracolich_paralysis;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
