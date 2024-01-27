using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SnakeCharmAction : IAffectAction
{
    public Affects ActionForAffect => Affects.snake_charm;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
