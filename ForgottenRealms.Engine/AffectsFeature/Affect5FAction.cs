using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect5FAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_5F;
    private readonly ovr024 _ovr024;
    public Affect5FAction(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;
        affect.callAffectTable = false;

        if (player.in_combat == true)
        {
            _ovr024.KillPlayer("Falls dead", Status.dead, player);
        }
    }
}
