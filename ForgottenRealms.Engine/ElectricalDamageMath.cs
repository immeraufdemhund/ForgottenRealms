using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ElectricalDamageMath
{
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;
    public ElectricalDamageMath(ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    public bool DoElecDamage(bool arg_0, int player_index, SaveVerseType bonusType, int damage, Point pos)
    {
        int groundTile;
        int playerIndex;

        _ovr033.AtMapXY(out groundTile, out playerIndex, pos);

        if (groundTile > 0 &&
            gbl.BackGroundTiles[groundTile].move_cost == 0xff &&
            gbl.area_ptr.inDungeon == 1 &&
            arg_0 == false)
        {
            arg_0 = true;
        }
        else
        {
            arg_0 = false;
        }

        DoActualElecDamage(player_index, bonusType, damage, playerIndex);

        return arg_0;
    }

    public void DoElecDamage(int player_index, SaveVerseType bonusType, int damage, Point pos)
    {
        int playerIndex = _ovr033.PlayerIndexAtMapXY(pos.y, pos.x);

        DoActualElecDamage(player_index, bonusType, damage, playerIndex);
    }

    private void DoActualElecDamage(int player_index, SaveVerseType bonusType, int damage, int playerIndex)
    {
        if (playerIndex > 0 &&
            playerIndex != player_index)
        {
            Player player = gbl.player_array[playerIndex];
            gbl.damage_flags = DamageType.Magic | DamageType.Electricity;

            _ovr024.damage_person(_ovr024.RollSavingThrow(0, bonusType, player), DamageOnSave.Half, damage, player);
            _ovr025.load_missile_icons(0x13);
            gbl.damage_flags = 0;
        }
    }
}
