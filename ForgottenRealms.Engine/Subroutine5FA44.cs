using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class Subroutine5FA44
{
    private readonly ElectricalDamageMath _electricalDamageMath;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public Subroutine5FA44(ElectricalDamageMath electricalDamageMath, ovr025 ovr025, ovr033 ovr033)
    {
        _electricalDamageMath = electricalDamageMath;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    internal void sub_5FA44(byte arg_0, SaveVerseType bonusType, int damage, byte arg_6)
    {
        int var_3A = 0; /* Simeon */
        bool var_36 = false;
        _ovr025.load_missile_icons(0x13);

        int var_39;
        int groundTile;

        _ovr033.AtMapXY(out groundTile, out var_39, gbl.targetPos);
        int var_3D = 0;
        int var_35 = 1;

        var playerPos = _ovr033.PlayerMapPos(gbl.SelectedPlayer);
        byte var_38 = arg_0;

        if (playerPos != gbl.targetPos)
        {
            int var_3C = arg_6 * 2;
            gbl.byte_1D2C7 = true;

            while (var_3C > 0)
            {
                var path_a = new SteppingPath();

                path_a.attacker = gbl.targetPos;
                path_a.target = gbl.targetPos + ((gbl.targetPos - playerPos) * var_35 * var_3C);

                path_a.CalculateDeltas();

                do
                {
                    var tmppos = path_a.current;

                    if (path_a.attacker != path_a.target)
                    {
                        bool stepping;

                        do
                        {
                            stepping = path_a.Step();

                            _ovr033.AtMapXY(out groundTile, out var_3A, path_a.current);

                            if (gbl.BackGroundTiles[groundTile].move_cost == 1)
                            {
                                var_36 = false;
                            }
                        } while (stepping == true &&
                                 (var_3A <= 0 || var_3A == var_39) &&
                                 groundTile != 0 &&
                                 gbl.BackGroundTiles[groundTile].move_cost <= 1 &&
                                 path_a.steps < var_3C);
                    }

                    if (groundTile == 0)
                    {
                        var_3C = 0;
                    }

                    _ovr025.draw_missile_attack(0x32, 4, path_a.current, tmppos);

                    var_36 = _electricalDamageMath.DoElecDamage(var_36, var_39, bonusType, damage, path_a.current);
                    var_39 = var_3A;

                    if (var_36 == true)
                    {
                        gbl.targetPos = path_a.current;

                        var path_b = new SteppingPath();

                        path_b.attacker = gbl.targetPos;
                        path_b.target = playerPos;

                        path_b.CalculateDeltas();

                        while (path_b.Step() == true)
                        {
                            /* empty */
                        }

                        if (var_38 != 0 && path_b.steps <= 8)
                        {
                            path_a.steps += 8;
                        }

                        var_35 = -var_35;
                        var_38 = 0;
                        var_39 = 0;
                    }

                    var_3D = (byte)(path_a.steps - var_3D);

                    if (var_3D < var_3C)
                    {
                        var_3C -= var_3D;
                    }
                    else
                    {
                        var_3C = 0;
                    }

                    var_3D = path_a.steps;
                } while (var_36 == false && var_3C != 0);
            }

            gbl.byte_1D2C7 = false;
        }
    }
}
