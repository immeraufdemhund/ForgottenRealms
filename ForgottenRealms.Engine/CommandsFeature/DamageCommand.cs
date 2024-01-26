using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class DamageCommand : IGameCommand
{
    private readonly ovr008 _ovr008;
    private readonly ovr024 _ovr024;
    private readonly seg037 _seg037;
    private readonly DisplayDriver _displayDriver;
    private readonly KeyboardDriver _keyboardDriver;
    public DamageCommand(ovr008 ovr008, ovr024 ovr024, seg037 seg037, DisplayDriver displayDriver, KeyboardDriver keyboardDriver)
    {
        _ovr008 = ovr008;
        _ovr024 = ovr024;
        _seg037 = seg037;
        _displayDriver = displayDriver;
        _keyboardDriver = keyboardDriver;
    }

    public void Execute()
    {
        var currentPlayerBackup = gbl.SelectedPlayer;

        _ovr008.vm_LoadCmdSets(5);
        var var_1 = (byte)_ovr008.vm_GetCmdValue(1);
        int dice_count = _ovr008.vm_GetCmdValue(2);
        int dice_size = _ovr008.vm_GetCmdValue(3);
        int dam_plus = _ovr008.vm_GetCmdValue(4);
        var var_6 = (byte)_ovr008.vm_GetCmdValue(5);

        var damage = _ovr024.roll_dice(dice_size, dice_count) + dam_plus;

        byte rnd_player_id = 0;
        if ((var_1 & 0x40) == 0)
        {
            rnd_player_id = _ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
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
                        _ovr008.sub_32200(player03, damage);
                    }
                    else if (_ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, player03) == false)
                    {
                        _ovr008.sub_32200(player03, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        _ovr008.sub_32200(player03, damage);
                    }
                }
            }
            else
            {
                if ((var_6 & 0x80) != 0)
                {
                    if (bonusType == 0 ||
                        _ovr024.RollSavingThrow(saveBonus, (SaveVerseType)(bonusType - 1), gbl.SelectedPlayer) == false)
                    {
                        _ovr008.sub_32200(gbl.SelectedPlayer, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        _ovr008.sub_32200(gbl.SelectedPlayer, damage);
                    }
                }
                else
                {
                    var target = gbl.TeamList[rnd_player_id - 1];

                    if (_ovr024.RollSavingThrow(saveBonus, (SaveVerseType)bonusType, target) == false)
                    {
                        _ovr008.sub_32200(target, damage);
                    }
                    else if ((var_1 & 0x10) != 0)
                    {
                        _ovr008.sub_32200(target, damage);
                    }
                }
            }
        }
        else
        {
            for (var i = 0; i < var_1; i++)
            {
                rnd_player_id = _ovr024.roll_dice(gbl.area2_ptr.party_size, 1);
                var player03 = gbl.TeamList[rnd_player_id - 1];

                if (_ovr024.CanHitTarget(var_6, player03) == true)
                {
                    _ovr008.sub_32200(player03, damage);
                }

                damage = _ovr024.roll_dice(dice_size, dice_count) + dam_plus;
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
            _seg037.DrawFrame_Outer();
            gbl.textXCol = 2;
            gbl.textYCol = 2;

            _displayDriver.press_any_key("The entire party is killed!", true, 10, 0x16, 0x26, 1, 1);
            _keyboardDriver.SysDelay(3000);
        }

        gbl.SelectedPlayer = currentPlayerBackup;
        _displayDriver.DisplayAndPause("press <enter>/<return> to continue", 15);
    }
}
