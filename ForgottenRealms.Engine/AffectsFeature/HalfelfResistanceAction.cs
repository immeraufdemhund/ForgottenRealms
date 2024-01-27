using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class HalfelfResistanceAction : IAffectAction
{
    public Affects ActionForAffect => Affects.halfelf_resistance;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
