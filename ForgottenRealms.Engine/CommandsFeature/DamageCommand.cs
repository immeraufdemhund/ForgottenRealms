using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DamageCommand : IGameCommand
{
    public void Execute()
    {
        var currentPlayerBackup = gbl.SelectedPlayer;

        ovr008.vm_LoadCmdSets(5);
        var var_1 = (byte)ovr008.vm_GetCmdValue(1);
        int dice_count = ovr008.vm_GetCmdValue(2);
        int dice_size = ovr008.vm_GetCmdValue(3);
        int dam_plus = ovr008.vm_GetCmdValue(4);
        var var_6 = (byte)ovr008.vm_GetCmdValue(5);

        var damage = ovr024.roll_dice(dice_size, dice_count) + dam_plus;

        byte rnd_player_id = 0;
        if ((var_1 & 0x40) == 0)
        {
            rnd_player_id = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
        }

        if ((var_1 & 0x80) != 0)
        {
            var saveBonus = var_1 & 0x1f;
            var bonusType = var_6 & 7;

            if ((var_1 & 0x40) != 0)
            {
                foreach (var player03 in gbl.TeamList)
                {
                    if ((var_1 & 0x20) != 0)
                    {
                        ovr008.sub_32200(player03, damage);
                    }
                    else if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, player03) == false)
                    {
                        ovr008.sub_32200(player03, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        ovr008.sub_32200(player03, damage);
                    }
                }
            }
            else
            {
                if ((var_6 & 0x80) != 0)
                {
                    if (bonusType == 0 ||
                        ovr024.RollSavingThrow(saveBonus, (SaveVerseType)(bonusType - 1), gbl.SelectedPlayer) == false)
                    {
                        ovr008.sub_32200(gbl.SelectedPlayer, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        ovr008.sub_32200(gbl.SelectedPlayer, damage);
                    }
                }
                else
                {
                    var target = gbl.TeamList[rnd_player_id - 1];

                    if (ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, target) == false)
                    {
                        ovr008.sub_32200(target, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        ovr008.sub_32200(target, damage);
                    }
                }
            }
        }
        else
        {
            for (var i = 0; i < var_1; i++)
            {
                rnd_player_id = ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
                var player03 = gbl.TeamList[rnd_player_id - 1];

                if (ovr024.CanHitTarget(var_6, player03) == true)
                {
                    ovr008.sub_32200(player03, damage);
                }

                damage = ovr024.roll_dice(dice_size, dice_count) + dam_plus;
            }
        }

        gbl.party_killed = true;

        foreach (var player in gbl.TeamList)
        {
            if (player.in_combat == true)
            {
                gbl.party_killed = false;
            }
        }

        if (gbl.party_killed == true)
        {
            seg037.DrawFrame_Outer();
            gbl.textXCol = 2;
            gbl.textYCol = 2;

            DisplayDriver.press_any_key("The entire party is killed!", true, 10, 0x16, 0x26, 1, 1);
            KeyboardDriver.SysDelay(3000);
        }

        gbl.SelectedPlayer = currentPlayerBackup;
        DisplayDriver.DisplayAndPause("press <enter>/<return> to continue", 15);
    }
}
