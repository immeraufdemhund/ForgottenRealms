using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ParalizingGazeAction : IAffectAction
{
    public Affects ActionForAffect => Affects.paralizing_gaze;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
