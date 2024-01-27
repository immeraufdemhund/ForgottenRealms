using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class FriendsAction : IAffectAction
{
    public Affects ActionForAffect => Affects.friends;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
