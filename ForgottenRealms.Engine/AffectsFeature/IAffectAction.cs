using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public interface IAffectAction
{
    void Execute(Effect effect, object affect, Player player);
}
