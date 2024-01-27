using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect57Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_57;

    private readonly FindTargetMath _findTargetMath;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr032 _ovr032;
    private readonly ovr033 _ovr033;
    public Affect57Action(FindTargetMath findTargetMath, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr032 ovr032, ovr033 ovr033)
    {
        _findTargetMath = findTargetMath;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr032 = ovr032;
        _ovr033 = ovr033;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        int range = 0xFF; /* simeon */

        byte attacksTired = 0;
        int attackTiresLeft = 4;

        player.actions.target = null;
        sub_421C1(true, ref range, player);

        do
        {
            Player target = player.actions.target;

            range = _ovr025.getTargetRange(target, player);
            attackTiresLeft--;

            if (target != null)
            {
                if (range == 2 && (attacksTired & 1) == 0)
                {
                    attacksTired |= 1;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", player);
                    LoadMissleIconAndDraw(5, target, player);

                    if (_ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, target) == false)
                    {
                        _ovr024.KillPlayer("is disintergrated", Status.gone, target);
                    }

                    sub_421C1(false, ref range, player);
                }
                else if (range == 3 && (attacksTired & 2) == 0)
                {
                    attacksTired |= 2;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", player);
                    LoadMissleIconAndDraw(10, target, player);

                    if (_ovr024.RollSavingThrow(0, SaveVerseType.Petrification, target) == false)
                    {
                        _ovr024.KillPlayer("is Stoned", Status.stoned, target);
                    }

                    sub_421C1(false, ref range, player);
                }
                else if (range == 4 && (attacksTired & 4) == 0)
                {
                    attacksTired |= 4;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", player);
                    LoadMissleIconAndDraw(5, target, player);

                    if (_ovr024.RollSavingThrow(0, 0, target) == false)
                    {
                        _ovr024.KillPlayer("is killed", Status.dead, target);
                    }

                    sub_421C1(false, ref range, player);
                }
                else if (range == 5 && (attacksTired & 8) == 0)
                {
                    attacksTired |= 8;

                    _ovr025.DisplayPlayerStatusString(true, 10, "wounds you", player);
                    LoadMissleIconAndDraw(5, target, player);

                    _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(8, 2) + 1, target);
                    sub_421C1(false, ref range, player);
                }
                else if ((attacksTired & 0x10) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x54);
                    attacksTired |= 0x10;
                }
                else if ((attacksTired & 0x20) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x37);
                    attacksTired |= 0x20;
                }
                else if ((attacksTired & 0x40) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x15);
                    attacksTired |= 0x40;
                }
            }
        } while (attackTiresLeft > 0 && player.actions.target != null);
    }

    private void LoadMissleIconAndDraw(int icon_id, Player target, Player attacker) //sub_42159
    {
        _ovr025.load_missile_icons(icon_id + 13);

        _ovr025.draw_missile_attack(0x1E, 1, _ovr033.PlayerMapPos(target), _ovr033.PlayerMapPos(attacker));
    }


    private bool sub_421C1(bool clear_target, ref int range, Player player) // sub_421C1
    {
        bool var_5 = true;

        if (_findTargetMath.find_target(clear_target, 0, 0xff, player) == true)
        {
            var target = _ovr033.PlayerMapPos(player.actions.target);

            if (_ovr032.canReachTarget(ref range, target, _ovr033.PlayerMapPos(player)) == true)
            {
                var_5 = false;
            }
        }

        return var_5;
    }
}
