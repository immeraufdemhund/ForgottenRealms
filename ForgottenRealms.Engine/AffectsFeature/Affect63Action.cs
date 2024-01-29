using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect63Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_63;
    private readonly ovr024 _ovr024;
    public Affect63Action(ovr024 ovr024)
    {
        _ovr024 = ovr024;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        Affect arg_2 = (Affect)param;

        byte heal_amount = 0;

        if (player.health_status == Status.dying &&
            player.actions.bleeding < 6)
        {
            heal_amount = (byte)(6 - player.actions.bleeding);
        }

        if (player.health_status == Status.unconscious)
        {
            heal_amount = 6;
        }

        if (heal_amount > 0 &&
            _ovr024.combat_heal(heal_amount, player) == true)
        {
            var rollDice = (ushort)(_ovr024.roll_dice(4, 1) + 1);
            _ovr024.add_affect(true, 0xff, rollDice, Affects.affect_5F, player);
            arg_2.callAffectTable = false;
            _ovr024.remove_affect(arg_2, Affects.affect_63, player);
        }
    }
}
