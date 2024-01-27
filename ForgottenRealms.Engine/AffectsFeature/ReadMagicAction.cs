using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ReadMagicAction : IAffectAction
{
    public Affects ActionForAffect => Affects.read_magic;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
