using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect8AAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_8a;

    private readonly ovr024 _ovr024;
    public Affect8AAction(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        _ovr024.add_affect(false, 0xff, 0xff, Affects.invisibility, player);
    }
}
