using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.CommandsFeature;

public class CombatCommand : IGameCommand
{
    private readonly TempleShopService _templeShopService;
    private readonly ovr006 _ovr006;
    private readonly ovr007 _ovr007;
    private readonly ovr008 _ovr008;
    private readonly ovr009 _ovr009;
    private readonly ovr025 _ovr025;
    private readonly ovr030 _ovr030;

    public CombatCommand(TempleShopService templeShopService, ovr006 ovr006, ovr007 ovr007, ovr008 ovr008, ovr009 ovr009, ovr025 ovr025, ovr030 ovr030)
    {
        _templeShopService = templeShopService;
        _ovr006 = ovr006;
        _ovr007 = ovr007;
        _ovr008 = ovr008;
        _ovr009 = ovr009;
        _ovr025 = ovr025;
        _ovr030 = ovr030;
    }

    public void Execute()
    {
        gbl.ecl_offset++;

        if (gbl.monstersLoaded == false &&
            gbl.combat_type == CombatType.normal)
        {
            if (gbl.area2_ptr.EnterShop == 1)
            {
                gbl.area2_ptr.EnterShop = 0;

                _ovr007.CityShop();
            }
            else if (gbl.area2_ptr.EnterTemple == 1)
            {
                gbl.area2_ptr.EnterTemple = 0;

                _templeShopService.temple_shop();
            }
            else
            {
                _ovr006.AfterCombatExpAndTreasure();
            }
        }
        else
        {
            ushort var_2 = _ovr008.sub_304B4(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            if (var_2 < gbl.area2_ptr.encounter_distance)
            {
                gbl.area2_ptr.encounter_distance = var_2;
            }

            _ovr009.MainCombatLoop();

            _ovr006.AfterCombatExpAndTreasure();

            if (gbl.area_ptr.inDungeon == 0)
            {
                _ovr030.load_bigpic(0x79);
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
        _ovr025.LoadPic();
    }
}
