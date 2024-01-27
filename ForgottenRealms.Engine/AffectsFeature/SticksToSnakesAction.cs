using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class SticksToSnakesAction : IAffectAction
{
    public Affects ActionForAffect => Affects.sticks_to_snakes;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;

    public SticksToSnakesAction(ovr024 ovr024, ovr025 ovr025)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect affect = (Affect)param;

        byte var_1 = (byte)(player.attack2_AttacksLeft + player.attack1_AttacksLeft);

        if (affect.affect_data > var_1)
        {
            affect.affect_data -= var_1;
        }
        else
        {
            _ovr024.remove_affect(null, Affects.sticks_to_snakes, player);
        }

        _ovr025.MagicAttackDisplay("is fighting with snakes", true, player);
        _ovr025.ClearPlayerTextArea();

        _ovr025.clear_actions(player);
    }
}
