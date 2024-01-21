using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CombatCommand : IGameCommand
{
    public void Execute()
    {
        gbl.ecl_offset++;

        if (gbl.monstersLoaded == false &&
            gbl.combat_type == CombatType.normal)
        {
            if (gbl.area2_ptr.EnterShop == 1)
            {
                gbl.area2_ptr.EnterShop = 0;

                ovr007.CityShop();
            }
            else if (gbl.area2_ptr.EnterTemple == 1)
            {
                gbl.area2_ptr.EnterTemple = 0;

                ovr005.temple_shop();
            }
            else
            {
                ovr006.AfterCombatExpAndTreasure();
            }
        }
        else
        {
            ushort var_2 = ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (var_2 < gbl.area2_ptr.encounter_distance)
            {
                gbl.area2_ptr.encounter_distance = var_2;
            }

            ovr009.MainCombatLoop();

            ovr006.AfterCombatExpAndTreasure();

            if (gbl.area_ptr.inDungeon == 0)
            {
                ovr030.load_bigpic(0x79);
            }
        }

        if (gbl.area_ptr.inDungeon != 0)
        {
            gbl.game_state = GameState.DungeonMap;
        }
        else
        {
            gbl.game_state = GameState.WildernessMap;
        }

        gbl.area2_ptr.search_flags &= 1;

        gbl.encounter_flags[0] = false;
        gbl.encounter_flags[1] = false;
        gbl.spriteChanged = false;
        ovr025.LoadPic();
    }
}