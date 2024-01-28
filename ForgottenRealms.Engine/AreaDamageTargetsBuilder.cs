using System.Collections.Generic;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class AreaDamageTargetsBuilder
{
    private Point[] unk_16D22 = { new Point(-1, 0), new Point(0, -1), new Point(0, -1), new Point(1, 0), new Point(1, 0), new Point(0, 1), new Point(0, 1), new Point(-1, 0) };
    private Point[] unk_16D32 = { new Point(1, 0), new Point(1, 0), new Point(0, 1), new Point(0, 1), new Point(-1, 0), new Point(-1, 0), new Point(0, -1), new Point(0, -1) };

    private readonly ovr032 _ovr032;
    private readonly ovr033 _ovr033;

    public AreaDamageTargetsBuilder(ovr032 ovr032, ovr033 ovr033)
    {
        _ovr032 = ovr032;
        _ovr033 = ovr033;
    }

    public void BuildAreaDamageTargets(int max_range, int playerSize, Point targetPos, Point casterPos)
    {
        List<int> players_on_path = new List<int>();

        bool finished;
        SteppingPath path = new SteppingPath();

        localSteppingPathInit(targetPos, casterPos, path);

        byte[] directions = new byte[0x32];
        int index = 0;
        while (!path.Step())
        {
            directions[index] = path.direction;
            index++;
        }

        int count = index - 1;

        index = 0;
        max_range *= 2;
        int tmp_range = path.steps;
        finished = false;

        var tmpPos = new Point(targetPos);

        while (tmp_range < max_range && finished == false)
        {
            if (tmpPos.x < 0x31 && tmpPos.x > 0 && tmpPos.y < 0x18 && tmpPos.y > 0)
            {
                switch (directions[index])
                {
                    case 0:
                    case 2:
                    case 4:
                    case 6:
                        tmp_range += 2;
                        break;

                    case 1:
                    case 3:
                    case 5:
                    case 7:
                        tmp_range += 3;
                        break;
                }

                tmpPos += gbl.MapDirectionDelta[directions[index]];

                if (index == count)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
            else
            {
                finished = true;
            }
        }

        targetPos.MapBoundaryTrunc();

        _ovr032.canReachTarget(ref targetPos, casterPos);

        localSteppingPathInit(targetPos, casterPos, path);
        int var_76 = find_players_on_path(path, players_on_path);

        if (playerSize > 1)
        {
            Point map_b = targetPos + unk_16D32[var_76];
            map_b.MapBoundaryTrunc();

            localSteppingPathInit(map_b, casterPos, path);
            find_players_on_path(path, players_on_path);

            if (playerSize > 2)
            {
                Point map_a = targetPos + unk_16D22[var_76];

                map_a.MapBoundaryTrunc();

                localSteppingPathInit(map_a, casterPos, path);
                find_players_on_path(path, players_on_path);
            }
        }

        gbl.spellTargets.Clear();

        foreach (var idx in players_on_path)
        {
            var player = gbl.player_array[idx];
            if (player != gbl.SelectedPlayer)
            {
                gbl.spellTargets.Add(player);
            }
        }
    }

    private void localSteppingPathInit(Point target, Point caster, SteppingPath path) /* sub_5D676 */
    {
        path.attacker = caster;
        path.target = target;

        path.CalculateDeltas();
    }

    private int find_players_on_path(SteppingPath path, List<int> player_list) /* sub_5D702 */
    {
        int dir = 0;
        while (path.Step())
        {
            int playerIndex = _ovr033.PlayerIndexAtMapXY(path.current.y, path.current.x);

            if (playerIndex > 0)
            {
                if (player_list.Contains(playerIndex) == false)
                {
                    player_list.Add(playerIndex);
                }
            }

            dir = path.direction;
        }

        return dir;
    }
}
