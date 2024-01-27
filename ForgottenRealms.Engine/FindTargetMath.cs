using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class FindTargetMath
{
    private readonly CanSeeTargetMath _canSeeTargetMath;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;

    public FindTargetMath(CanSeeTargetMath canSeeTargetMath, ovr024 ovr024, ovr025 ovr025)
    {
        _canSeeTargetMath = canSeeTargetMath;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public bool find_target(bool clear_target, byte arg_2, int max_range, Player player)
    {
        bool target_found = false;

        Player target = player.actions.target;

        if (clear_target == true ||
            (target != null &&
             (target.combat_team == player.combat_team ||
              target.in_combat == false ||
              _canSeeTargetMath.CanSeeTargetA(target, player) == false)))
        {
            player.actions.target = null;
        }

        if (player.actions.target != null)
        {
            target_found = true;
        }

        bool secondPass = false;
        bool var_5 = false;
        while (target_found == false && var_5 == false)
        {
            var_5 = secondPass;

            if (secondPass == true && clear_target == false)
            {
                gbl.mapToBackGroundTile.ignoreWalls = true;
            }

            int tryCount = 20;
            var nearTargets = _ovr025.BuildNearTargets(max_range, player);

            while (tryCount > 0 && target_found == false && nearTargets.Count > 0)
            {
                tryCount--;
                int roll = _ovr024.roll_dice(nearTargets.Count, 1);

                var epi = nearTargets[roll - 1];
                target = epi.player;

                if ((arg_2 != 0 && gbl.mapToBackGroundTile.ignoreWalls == true) ||
                    _canSeeTargetMath.CanSeeTargetA(target, player) == true)
                {
                    target_found = true;
                    player.actions.target = target;
                }
                else
                {
                    nearTargets.Remove(epi);
                }
            }

            if (secondPass == false)
            {
                secondPass = true;
            }
        }

        gbl.mapToBackGroundTile.ignoreWalls = false;

        return target_found;
    }
}
