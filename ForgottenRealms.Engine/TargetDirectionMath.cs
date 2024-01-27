using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class TargetDirectionMath
{
    private readonly ovr033 _ovr033;
    public TargetDirectionMath(ovr033 ovr033)
    {
        _ovr033 = ovr033;
    }

    internal byte getTargetDirection(Player playerB, Player playerA) /* sub_409BC */
    {
        var plyr_a = _ovr033.PlayerMapPos(playerA);
        var plyr_b = _ovr033.PlayerMapPos(playerB);

        int diff_x = System.Math.Abs(plyr_b.x - plyr_a.x);
        int diff_y = System.Math.Abs(plyr_b.y - plyr_a.y);

        byte direction = 0;
        bool solved = false;

        while (solved == false)
        {
            switch (direction)
            {
                case 0:
                    if (plyr_b.y > plyr_a.y ||
                        ((0x26A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 2:
                    if (plyr_b.x < plyr_a.x ||
                        ((0x6A * diff_x) / 0x100) < diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 4:
                    if (plyr_b.y < plyr_a.y ||
                        ((0x26A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 6:
                    if (plyr_b.x > plyr_a.x ||
                        ((0x6A * diff_x) / 0x100) < diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 1:
                    if (plyr_b.y > plyr_a.y ||
                        plyr_b.x < plyr_a.x ||
                        ((0x26A * diff_x) / 0x100) < diff_y ||
                        ((0x6A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 3:
                    if (plyr_b.y < plyr_a.y ||
                        plyr_b.x < plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 5:
                    if (plyr_b.y < plyr_a.y ||
                        plyr_b.x > plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 7:
                    if (plyr_b.y > plyr_a.y ||
                        plyr_b.x > plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;
            }

            if (solved == false)
            {
                direction++;
            }
        }

        return direction;
    }
}
