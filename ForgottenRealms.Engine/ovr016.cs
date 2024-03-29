using System.Collections.Generic;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.CharacterFeature.CreatePlayerFeature;
using ForgottenRealms.Engine.CharacterFeature.DropCharacterFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr016
{
    private Set AlterSet = new Set(0, 69);

    private readonly DropCharacterService _dropCharacterService;
    private readonly IconBuilder _iconBuilder;
    private readonly DisplayDriver _displayDriver;
    private readonly MainGameEngine _mainGameEngine;
    private readonly ovr017 _ovr017;
    private readonly ovr020 _ovr020;
    private readonly ovr021 _ovr021;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;
    private readonly ovr030 _ovr030;
    private readonly seg037 _seg037;
    private readonly seg051 _seg051;

    public ovr016(DropCharacterService dropCharacterService, IconBuilder iconBuilder, DisplayDriver displayDriver, MainGameEngine mainGameEngine, ovr017 ovr017, ovr020 ovr020, ovr021 ovr021, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr027 ovr027, ovr030 ovr030, seg037 seg037, seg051 seg051)
    {
        _dropCharacterService = dropCharacterService;
        _iconBuilder = iconBuilder;
        _displayDriver = displayDriver;
        _mainGameEngine = mainGameEngine;
        _ovr017 = ovr017;
        _ovr020 = ovr020;
        _ovr021 = ovr021;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
        _ovr030 = ovr030;
        _seg037 = seg037;
        _seg051 = seg051;
    }

    internal int sub_44032(Player player)
    {
        int max_spell_level = 0;
        int total_spell_level = 0;

        foreach (int id in player.spellList.LearningList())
        {
            int var_C = gbl.spellCastingTable[id].spellLevel;

            if (var_C > max_spell_level)
            {
                max_spell_level = var_C;
            }

            total_spell_level += var_C;
        }

        int max_scribe_level = 0;
        int total_scribe_level = 0;

        foreach (Item item in player.items)
        {
            if (item.IsScroll())
            {
                for (int loop_var = 1; loop_var <= 3; loop_var++)
                {
                    if ((int)item.getAffect(loop_var) > 0x7f)
                    {
                        int var_C = gbl.spellCastingTable[(int)item.getAffect(loop_var) & 0x7f].spellLevel;

                        if (var_C > max_scribe_level)
                        {
                            max_scribe_level = var_C;
                        }

                        total_scribe_level += var_C;
                    }
                }
            }
        }

        byte count = 0;
        if (total_spell_level > 0 || total_scribe_level > 0)
        {
            count = 4;
        }

        if (max_spell_level > 2 || max_scribe_level > 2)
        {
            count = 6;
        }

        player.spell_to_learn_count = count;
        int result = (count * 0x3C) + (total_scribe_level * 0x0f) + (total_spell_level * 0x0f);

        return result;
    }


    private void cancel_memorize(Player player)
    {
        player.spellList.CancelLearning();

        player.spell_to_learn_count = 0;
    }


    private void cancel_scribes(Player player)
    {
        foreach (Item item in player.items)
        {
            if (item.IsScroll() == true)
            {
                item.affect_1 &= (Affects)0x7F;
                item.affect_2 &= (Affects)0x7F;
                item.affect_3 &= (Affects)0x7F;
            }
        }
    }


    internal void cancel_spells()
    {
        foreach (Player player in gbl.TeamList)
        {
            cancel_memorize(player);
            cancel_scribes(player);
        }
    }


    internal int HowManySpellsPlayerCanLearn(SpellClass spellClass, int spellLevel) //sub_4428E
    {
        int alreadyLearning = 0;

        foreach (int spellId in gbl.SelectedPlayer.spellList.IdList())
        {
            if (gbl.spellCastingTable[spellId].spellLevel == spellLevel &&
                gbl.spellCastingTable[spellId].spellClass == spellClass)
            {
                alreadyLearning++;
            }
        }

        return gbl.SelectedPlayer.spellCastCount[(int)spellClass, spellLevel - 1] - alreadyLearning;
    }


    internal bool sub_443A0(byte learn_type)
    {
        string text = string.Empty;

        if (learn_type == 1)
        {
            if (gbl.area_ptr.can_cast_spells == true)
            {
                text = "cannot cast spells in this area";
            }
        }
        else if (gbl.SelectedPlayer.health_status == Status.animated ||
                 gbl.SelectedPlayer.in_combat == false)
        {
            text = "is in no condition to ";

            switch (learn_type)
            {
                case 1:
                    text += "cast any spells";
                    break;

                case 2:
                    text += "memorize spells";
                    break;

                case 3:
                    text += "scribe any scrolls";
                    break;
            }
        }

        if (text.Length != 0)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, text, gbl.SelectedPlayer);

            return false;
        }

        return true;
    }


    internal void cast_spell()
    {
        bool redraw = false;

        gbl.lastSelectetSpellTarget = null;

        gbl.menuSelectedWord = 1;

        if (sub_443A0(1) == true)
        {
            byte spell_id;
            int index = -1;

            do
            {
                bool var_3;

                spell_id = _ovr020.spell_menu2(out var_3, ref index, SpellSource.Cast, SpellLoc.memory);

                if (spell_id != 0)
                {
                    redraw = true;
                    _seg037.draw8x8_clear_area(TextRegion.NormalBottom);

                    _ovr023.sub_5D2E1(true, QuickFight.False, spell_id);
                }
                else if (var_3 == true)
                {
                    redraw = true;
                }
                else
                {
                    _ovr025.DisplayPlayerStatusString(true, 10, "has no spells memorized", gbl.SelectedPlayer);
                }
            } while (spell_id != 0);
        }

        if (redraw == true)
        {
            _ovr025.LoadPic();
        }
    }


    private bool BuildMemorizeSpellText() // sub_445D4
    {
        const int MaxSpellLevel = 5;
        const int MaxSpellClass = 3;
        string[,] var_60 = new string[MaxSpellClass, MaxSpellLevel];

        bool[] canLearnSpellClass = new bool[MaxSpellClass];

        bool found = false;

        for (var spellClass = SpellClass.Cleric; spellClass < SpellClass.Monster; spellClass++)
        {
            canLearnSpellClass[(int)spellClass] = false;

            for (int spellLevel = 0; spellLevel < MaxSpellLevel; spellLevel++)
            {
                var_60[(int)spellClass, spellLevel] = HowManySpellsPlayerCanLearn(spellClass, spellLevel + 1).ToString();

                if (gbl.SelectedPlayer.spellCastCount[(int)spellClass, spellLevel] == 0)
                {
                    var_60[(int)spellClass, spellLevel] = " ";
                }
                else
                {
                    canLearnSpellClass[(int)spellClass] = true;
                    found = true;
                }
            }
        }

        if (found == true)
        {
            _ovr025.DisplayPlayerStatusString(false, 10, "can memorize:", gbl.SelectedPlayer);
            int y_col = 3;
            for (int spellClass = 0; spellClass < MaxSpellClass; spellClass++)
            {
                if (canLearnSpellClass[spellClass])
                {
                    string text = string.Empty;

                    switch (spellClass)
                    {
                        case 0:
                            text = "    Cleric Spells:";
                            break;

                        case 1:
                            text = "     Druid Spells:";
                            break;

                        case 2:
                            text = "Magic-User Spells:";
                            break;
                    }

                    _displayDriver.displayString(text, 0, 10, y_col + 17, 1);
                    int x_col = 0x13;
                    for (int spellLevel = 0; spellLevel < MaxSpellLevel; spellLevel++)
                    {
                        _displayDriver.displayString(var_60[spellClass, spellLevel], 0, 10, y_col + 0x11, x_col + 1);
                        x_col += 3;
                    }
                    y_col++;
                }
            }
        }

        return found;
    }


    internal bool rest_menu()
    {
        int max_rest_time = 0;
        foreach (Player player in gbl.TeamList)
        {
            int rest_time = sub_44032(player);

            if (max_rest_time < rest_time)
            {
                max_rest_time = rest_time;
            }
        }

        gbl.timeToRest.field_6 = (ushort)(max_rest_time / 60);
        gbl.timeToRest.field_4 = (ushort)((max_rest_time - (gbl.timeToRest.field_6 * 60)) / 10);
        gbl.timeToRest.field_2 = (ushort)(max_rest_time % 10);

        bool action_interrupted = _ovr021.resting(true);

        gbl.timeToRest.Clear();

        _ovr025.display_map_position_time();

        return action_interrupted;
    }


    internal void memorize_spell()
    {
        bool var_2;

        if (sub_443A0(2) == true)
        {
            bool var_1 = false;
            int index = -1;
            gbl.menuSelectedWord = 1;

            byte spellId = _ovr020.spell_menu2(out var_2, ref index, 0, SpellLoc.memorize);
            bool redraw = true;

            if (var_2 == true)
            {
                if (_ovr027.yes_no(gbl.alertMenuColors, "Memorize These Spells? ") == 'N')
                {
                    cancel_memorize(gbl.SelectedPlayer);
                }
                else
                {
                    var_1 = true;
                }
            }
            else
            {
                redraw = false;
            }

            index = -1;

            while (var_1 == false)
            {
                var_1 = (BuildMemorizeSpellText() == false);

                if (var_1 == true)
                {
                    _ovr025.DisplayPlayerStatusString(true, 10, "cannot memorize any spells", gbl.SelectedPlayer);
                }
                else
                {
                    spellId = _ovr020.spell_menu2(out var_2, ref index, SpellSource.Memorize, SpellLoc.grimoire);
                    redraw = true;

                    if (spellId == 0)
                    {
                        var_1 = true;
                    }
                    else if (HowManySpellsPlayerCanLearn(gbl.spellCastingTable[spellId].spellClass, gbl.spellCastingTable[spellId].spellLevel) > 0)
                    {
                        gbl.SelectedPlayer.spellList.AddLearn(spellId);
                    }
                }
            }

            if (index != -1)
            {
                index = -1;

                spellId = _ovr020.spell_menu2(out var_2, ref index, 0, SpellLoc.memorize);

                if (var_2 == true &&
                    _ovr027.yes_no(gbl.alertMenuColors, "Memorize these spells? ") == 'N')
                {
                    cancel_memorize(gbl.SelectedPlayer);
                }
            }

            if (redraw == true)
            {
                _ovr025.LoadPic();
            }
        }
    }


    internal void scribe_spell()
    {
        bool redraw;
        bool var_2;
        byte var_1;

        if (sub_443A0(3) == true)
        {
            var_1 = 0;
            int var_8 = -1;

            _ovr020.spell_menu2(out var_2, ref var_8, 0, SpellLoc.scribe);
            redraw = true;

            if (var_2 == true)
            {
                if (_ovr027.yes_no(gbl.alertMenuColors, "Scribe These Spells? ") == 'N')
                {
                    cancel_scribes(gbl.SelectedPlayer);
                }
                else
                {
                    var_1 = 1;
                }
            }
            else
            {
                redraw = false;
            }

            var_8 = -1;

            while (var_1 == 0)
            {
                int var_4 = _ovr020.spell_menu2(out var_2, ref var_8, SpellSource.Scribe, SpellLoc.scrolls);

                if (var_4 == 0)
                {
                    var_1 = 1;

                    if (var_2 == false)
                    {
                        _ovr025.DisplayPlayerStatusString(true, 10, "has no copyable scrolls", gbl.SelectedPlayer);
                    }
                    else
                    {
                        redraw = true;
                    }
                }
                else
                {
                    redraw = true;
                    if (gbl.SelectedPlayer.KnowsSpell((Spells)var_4))
                    {
                        _ovr025.string_print01("You already know that spell");
                    }
                    else
                    {
                        bool var_D = gbl.SelectedPlayer.items.Find(item =>
                        {
                            return (item.IsScroll() == true &&
                                    (item.ScrollLearning(1, var_4) ||
                                     item.ScrollLearning(2, var_4) ||
                                     item.ScrollLearning(3, var_4)));
                        }) != null;


                        if (var_D == true)
                        {
                            _ovr025.string_print01("You are already scibing that spell");
                        }
                        else
                        {
                            int spell_level = gbl.spellCastingTable[var_4].spellLevel;
                            int spell_class = (int)gbl.spellCastingTable[var_4].spellClass;

                            if (gbl.SelectedPlayer.spellCastCount[spell_class, spell_level - 1] > 0)
                            {
                                foreach (Item var_C in gbl.SelectedPlayer.items)
                                {
                                    int var_6 = 1;
                                    do
                                    {
                                        if (var_C.getAffect(var_6) == (Affects)var_4)
                                        {
                                            var_C.setAffect(var_6, (Affects)((int)var_C.getAffect(var_6) | 0x80));
                                            var_D = true;
                                        }

                                        var_6++;
                                    } while (var_6 <= 3 && var_D == false);

                                    if (var_D) break;
                                }
                            }
                            else
                            {
                                _ovr025.string_print01("You can not scribe that spell.");
                            }
                        }
                    }
                }
            }

            if (var_8 != -1)
            {
                var_8 = -1;

                _ovr020.spell_menu2(out var_2, ref var_8, 0, SpellLoc.scribe);

                if (var_2 == true &&
                    _ovr027.yes_no(gbl.alertMenuColors, "Scribe these spells? ") == 'N')
                {
                    cancel_scribes(gbl.SelectedPlayer);
                }
            }

            if (redraw)
            {
                _ovr025.LoadPic();
            }
        }
    }

    private Dictionary<Affects, string> EffectNameMap = new Dictionary<Affects, string>();

    internal void BuildEffectNameMap()
    {
        Affects[] affects = { Affects.bless, Affects.cursed, Affects.detect_magic, Affects.protection_from_evil,
            Affects.protection_from_good, Affects.resist_cold, Affects.charm_person, Affects.enlarge,
            Affects.friends, Affects.read_magic, Affects.shield, Affects.find_traps, Affects.resist_fire,
            Affects.silence_15_radius, Affects.slow_poison, Affects.spiritual_hammer, Affects.detect_invisibility,
            Affects.invisibility, Affects.mirror_image, Affects.ray_of_enfeeblement, Affects.animate_dead,
            Affects.blinded, Affects.cause_disease_1, Affects.bestow_curse, Affects.blink, Affects.strength,
            Affects.haste, Affects.prot_from_normal_missiles, Affects.slow, Affects.prot_from_evil_10_radius,
            Affects.prot_from_good_10_radius, Affects.prayer, Affects.snake_charm, Affects.paralyze, Affects.sleep };

        foreach (Affects aff in affects)
        {
            bool found = false;

            for (int spId = 1; spId <= 0x38 && found == false; spId++)
            {
                if (gbl.spellCastingTable[spId].affect_id == aff)
                {
                    EffectNameMap.Add(aff, _ovr023.SpellNames[spId]);
                    found = true;
                }
            }

            if (found == false)
            {
                EffectNameMap.Add(aff, "Funky--" + aff.ToString());
            }
        }

        EffectNameMap.Add(Affects.dispel_evil, "Dispel Evil");
        EffectNameMap.Add(Affects.faerie_fire, "Faerie Fire");
        EffectNameMap.Add(Affects.fumbling, "Fumbling");
        EffectNameMap.Add(Affects.helpless, "Helpless");
        EffectNameMap.Add(Affects.confuse, "Confused");
        EffectNameMap.Add(Affects.cause_disease_2, "Cause Disease");
        EffectNameMap.Add(Affects.hot_fire_shield, "Hot Fire Shield");
        EffectNameMap.Add(Affects.cold_fire_shield, "Cold Fire Shield");
        EffectNameMap.Add(Affects.poisoned, "Poisoned");
        EffectNameMap.Add(Affects.regenerate, "Regenerating");
        EffectNameMap.Add(Affects.fire_resist, "Fire Resistance");
        EffectNameMap.Add(Affects.minor_globe_of_invulnerability, "Minor Globe of Invulnerability");
        EffectNameMap.Add(Affects.feeblemind, "enfeebled");
        EffectNameMap.Add(Affects.invisible_to_animals, "invisible to animals");
        EffectNameMap.Add(Affects.invisible, "Invisible");
        EffectNameMap.Add(Affects.camouflage, "Camouflaged");
        EffectNameMap.Add(Affects.prot_drag_breath, "protected from dragon breath");
        EffectNameMap.Add(Affects.berserk, "berserk");
        EffectNameMap.Add(Affects.displace, "Displaced");

    }


    private void DisplayMagicEffects()
    {
        List<MenuItem> var_C = new List<MenuItem>();

        var_C.Add(new MenuItem());

        foreach (Player player in gbl.TeamList)
        {
            bool any_effects = false;

            var_C.Add(new MenuItem(player.name, true));

            foreach (Affect affect in player.affects)
            {
                string affect_name;

                if (EffectNameMap.TryGetValue(affect.type, out affect_name))
                {
                    any_effects = true;
                    var_C.Add(new MenuItem(" " + affect_name));
                }
            }

            if (any_effects == false)
            {
                var_C.Add(new MenuItem(" <No Spell Effects>"));
            }

            var_C.Add(new MenuItem(" "));
        }

        _seg037.DrawFrame_Outer();

        bool dummyRedraw = true;
        int dummyIndex = 0;
        MenuItem dummyResult;
        _ovr027.sl_select_item(out dummyResult, ref dummyIndex, ref dummyRedraw, true, var_C,
            0x16, 0x26, 4, 1, new MenuColorSet(15, 10, 11), string.Empty, string.Empty);

        var_C.Clear();
        _ovr025.LoadPic();
    }


    internal bool magic_menu()
    {
        char inputKey = ' ';
        bool actionInterrupted = false;

        while (actionInterrupted == false && AlterSet.MemberOf(inputKey) == false)
        {
            bool controlKey;
            inputKey = _ovr027.displayInput(out controlKey, true, 1, gbl.defaultMenuColors, "Cast Memorize Scribe Display Rest Exit", string.Empty);

            if (controlKey == true)
            {
                _ovr020.scroll_team_list(inputKey);
                _ovr025.PartySummary(gbl.SelectedPlayer);
            }
            else
            {
                switch (inputKey)
                {
                    case 'C':
                        cast_spell();
                        break;

                    case 'M':
                        memorize_spell();
                        break;

                    case 'S':
                        scribe_spell();
                        break;

                    case 'D':
                        DisplayMagicEffects();
                        break;

                    case 'R':
                        actionInterrupted = rest_menu();
                        break;
                }
            }
        }

        return actionInterrupted;
    }


    private void MoveCurrentPlayerUp() // sub_4558D
    {
        // move gbl.player_ptr up the list by one, if at head, move to tail.

        int index = gbl.TeamList.IndexOf(gbl.SelectedPlayer);
        if (index >= 0)
        {
            gbl.TeamList.RemoveAt(index);

            if (index > 0)
            {
                gbl.TeamList.Insert(index - 1, gbl.SelectedPlayer);
            }
            else
            {
                gbl.TeamList.Add(gbl.SelectedPlayer);
            }
        }
    }


    private void MoveCurrentPlayerDown() // sub_456E5
    {
        int index = gbl.TeamList.IndexOf(gbl.SelectedPlayer);
        if (index >= 0)
        {
            gbl.TeamList.RemoveAt(index);

            if (index == gbl.TeamList.Count)
            {
                gbl.TeamList.Insert(0, gbl.SelectedPlayer);
            }
            else
            {
                gbl.TeamList.Insert(index + 1, gbl.SelectedPlayer);
            }
        }
    }

    private string[] reorderStrings = { "Select Exit", "Place Exit" }; //seg600_04A6
    private Set reorderSet = new Set(13,80,83);

    private void reorder_party()
    {
        int reorderState = 0;
        char inputKey = ' ';

        while (AlterSet.MemberOf(inputKey) == false)
        {
            bool controlKey;
            inputKey = _ovr027.displayInput(out controlKey, true, 1, gbl.defaultMenuColors, reorderStrings[reorderState], "Party Order: ");

            if (controlKey == true)
            {
                if (reorderState == 0)
                {
                    _ovr020.scroll_team_list(inputKey);
                    _ovr025.PartySummary(gbl.SelectedPlayer);
                }
                else
                {
                    if (inputKey == 0x47)
                    {
                        MoveCurrentPlayerUp();
                    }
                    else if (inputKey == 0x4F)
                    {
                        MoveCurrentPlayerDown();
                    }
                    _ovr025.PartySummary(gbl.SelectedPlayer);
                }
            }
            else if (reorderSet.MemberOf(inputKey) == true)
            {
                reorderState = (reorderState == 0) ? 1 : 0;

                if (reorderState != 0)
                {
                    _ovr025.DisplayPlayerStatusString(false, 10, "has been selected", gbl.SelectedPlayer);
                }
                else
                {
                    _ovr025.ClearPlayerTextArea();
                }
            }
        }
    }

    internal void game_speed()
    {
        char inputKey;

        do
        {
            _displayDriver.displayString(string.Format("Game Speed = {0} (0=fastest 9=slowest)", gbl.game_speed_var), 0, 10, 18, 1);

            string text = string.Empty;

            if (gbl.game_speed_var > 0)
            {
                text += " Faster";
            }

            if (gbl.game_speed_var < 9)
            {
                text += " Slower";
            }

            text += " Exit";

            bool controlKey;
            inputKey = _ovr027.displayInput(out controlKey, true, 1, gbl.defaultMenuColors, text, "Game Speed:");

            if (controlKey == true)
            {
                if (inputKey == 0x50)
                {
                    if (gbl.game_speed_var > 0)
                    {
                        gbl.game_speed_var--;
                    }
                }
                else if (inputKey == 0x48)
                {
                    if (gbl.game_speed_var < 9)
                    {
                        gbl.game_speed_var++;
                    }
                }
            }
            else
            {
                if (inputKey == 0x46)
                {
                    gbl.game_speed_var--;
                }
                else if (inputKey == 0x53)
                {
                    gbl.game_speed_var++;
                }
            }

        } while (AlterSet.MemberOf(inputKey) == false);

        _ovr025.ClearPlayerTextArea();
    }

    internal void alter_menu()
    {
        char inputKey = ' ';

        while (AlterSet.MemberOf(inputKey) == false)
        {
            bool controlKey;
            inputKey = _ovr027.displayInput(out controlKey, true, 1, gbl.defaultMenuColors, "Order Drop Speed Icon Exit", "Alter: ");

            if (controlKey == true)
            {
                _ovr020.scroll_team_list(inputKey);
                _ovr025.PartySummary(gbl.SelectedPlayer);
            }
            else
            {
                switch (inputKey)
                {
                    case 'O':
                        reorder_party();
                        break;

                    case 'D':
                        _dropCharacterService.DropPlayer();
                        break;

                    case 'S':
                        game_speed();
                        break;

                    case 'I':
                        _iconBuilder.Show();
                        _ovr025.LoadPic();
                        break;
                }
            }
        }
    }


    private int CalculateInitialHealing() // sub_45F22
    {
        int HealingAvailable = 0;

        foreach (Player player in gbl.TeamList)
        {
            if (player.health_status == Status.okey)
            {
                foreach (int id in player.spellList.LearntList())
                {
                    switch (id)
                    {
                        case 3:
                            HealingAvailable += _ovr024.roll_dice(8, 1);
                            break;

                        case 0x3A:
                            HealingAvailable += _ovr024.roll_dice(8, 2) + 1;
                            break;

                        case 0x47:
                            HealingAvailable += _ovr024.roll_dice(8, 3) + 3;
                            break;
                    }
                }
            }
        }

        return HealingAvailable;
    }


    private void CalculateHealing(ref int healingAvailable, int numCureLight, int numCureSerious, int numCureCritical) // sub_45FDD
    {
        for (int i = 0; i < numCureLight; i++)
        {
            healingAvailable += _ovr024.roll_dice(8, 1);
        }

        for (int i = 0; i < numCureSerious; i++)
        {
            healingAvailable += _ovr024.roll_dice(8, 2) + 1;
        }

        for (int i = 0; i < numCureCritical; i++)
        {
            healingAvailable += _ovr024.roll_dice(8, 3) + 3;
        }
    }


    private int TotalHitpointsLost() /* sub_4608F */
    {
        int lost_points = 0;
        foreach (Player player in gbl.TeamList)
        {
            lost_points += player.hit_point_max - player.hit_point_current;
        }

        return lost_points;
    }


    internal void CalculateTimeAndSpellNumbers(out int numCureCritical, out int numCureSerious, out int numCureLight) //sub_460ED
    {
        numCureLight = 0;
        numCureSerious = 0;
        numCureCritical = 0;

        int maxHealing = 0;
        int maxTime = 0;

        foreach (Player player in gbl.TeamList)
        {
            int var_10 = 0;
            int var_A = 0;
            int var_C = 0;
            int var_E = 0;

            if (player.health_status == Status.okey)
            {
                numCureLight += player.spellCastCount[0, 0];
                var_A = player.spellCastCount[0, 0] * 15;

                numCureSerious += player.spellCastCount[0, 3];
                var_C = player.spellCastCount[0, 3] * 60;

                numCureCritical += player.spellCastCount[0, 4];
                var_E = player.spellCastCount[0, 4] * 75;
            }

            if (var_A > 0)
            {
                var_10 = 240;
                maxHealing += 27;
            }

            if ((var_C + var_E) != 0)
            {
                var_10 = 360;

                if (var_E > 0)
                {
                    maxHealing += 78;
                }
                else
                {
                    maxHealing += 34;
                }
            }

            var_10 += var_A + var_C + var_E;

            if (maxTime < var_10)
            {
                maxTime = var_10;
            }
        }

        if (TotalHitpointsLost() < maxHealing)
        {
            int var_11 = maxHealing / TotalHitpointsLost();

            maxTime /= var_11;
        }

        gbl.timeToRest.field_6 = maxTime / 60;
        gbl.timeToRest.field_4 = (maxTime - (gbl.timeToRest.field_6 * 60)) / 10;
        gbl.timeToRest.field_2 = maxTime % 10;
    }


    private void DoTeamHealing(ref int healingAvailable) //sub_46280
    {
        foreach (Player player in gbl.TeamList)
        {
            if (player.hit_point_max > player.hit_point_current)
            {
                int damge_taken = player.hit_point_max - player.hit_point_current;

                if (damge_taken > healingAvailable)
                {
                    damge_taken = healingAvailable;
                }

                if (damge_taken < 1)
                {
                    damge_taken = 0;
                }

                if (damge_taken > 0 &&
                    _ovr024.heal_player(0, damge_taken, player) == true &&
                    damge_taken <= healingAvailable)
                {
                    healingAvailable -= damge_taken;
                }
            }
        }
    }


    private bool FixTeam() // fix_menu
    {
        bool action_interrupted = false;

        if (TotalHitpointsLost() != 0)
        {
            int healingAvailable = CalculateInitialHealing();

            if (TotalHitpointsLost() == 0)
            {
                _ovr025.PartySummary(gbl.SelectedPlayer);
                _ovr025.display_map_position_time();
            }
            else
            {
                RestTime timeBackup = new RestTime(gbl.timeToRest);

                int numCureCritical;
                int numCureSerious;
                int numCureLight;
                CalculateTimeAndSpellNumbers(out numCureCritical, out numCureSerious, out numCureLight);

                action_interrupted = _ovr021.resting(false);

                if (action_interrupted == false)
                {
                    CalculateHealing(ref healingAvailable, numCureLight, numCureSerious, numCureCritical);
                    DoTeamHealing(ref healingAvailable);

                    _ovr025.PartySummary(gbl.SelectedPlayer);
                    _ovr025.display_map_position_time();

                    gbl.timeToRest = new RestTime(timeBackup);
                }
            }
        }

        return action_interrupted;
    }

    private Set unk_463F4 = new Set(0, 69);

    /// <summary>
    /// Does Camp menu, returns if interrupted
    /// </summary>
    internal bool MakeCamp() // make_camp
    {
        var game_state_bkup = gbl.game_state;
        gbl.game_state = GameState.Camping;
        gbl.rest_10_seconds = 0;

        gbl.timeToRest.Clear();

        gbl.byte_1D5AB = gbl.lastDaxFile;
        gbl.byte_1D5B5 = gbl.lastDaxBlockId;

        _ovr025.LoadPic();
        _seg037.draw8x8_clear_area(TextRegion.NormalBottom);

        _displayDriver.displayString("The party makes camp...", 0, 10, 18, 1);
        cancel_spells();
        bool actionInterrupted = false;
        char input_key = ' ';

        while (actionInterrupted == false &&
               unk_463F4.MemberOf(input_key) == false)
        {
            bool special_key;
            input_key = _ovr027.displayInput(out special_key, true, 1, gbl.defaultMenuColors, "Save View Magic Rest Alter Fix Exit", "Camp:");

            if (special_key == true)
            {
                _ovr020.scroll_team_list(input_key);
                _ovr025.PartySummary(gbl.SelectedPlayer);
            }
            else
            {
                switch (input_key)
                {
                    case 'S':
                        _ovr017.SaveGame();
                        if (_ovr027.yes_no(gbl.alertMenuColors, "Quit TO DOS ") == 'Y')
                        {
                            _mainGameEngine.EngineStop();
                        }
                        break;

                    case 'V':
                        gbl.menuSelectedWord = 1;
                        _ovr020.viewPlayer();
                        break;

                    case 'M':
                        gbl.menuSelectedWord = 1;
                        actionInterrupted = magic_menu();
                        break;

                    case 'R':
                        gbl.menuSelectedWord = 1;
                        actionInterrupted = rest_menu();
                        break;

                    case 'F':
                        actionInterrupted = FixTeam();
                        break;

                    case 'A':
                        gbl.menuSelectedWord = 1;
                        alter_menu();
                        break;
                }
            }
        }

        if (_seg051.Copy(3, 1, gbl.byte_1D5AB) == "PIC")
        {
            _ovr030.load_pic_final(ref gbl.byte_1D556, 0, gbl.byte_1D5B5, gbl.byte_1D5AB);
        }

        cancel_spells();
        gbl.lastSelectetSpellTarget = null;
        gbl.game_state = game_state_bkup;
        _ovr025.display_map_position_time();
        _ovr025.ClearPlayerTextArea();
        _ovr027.ClearPromptArea();

        return actionInterrupted;
    }
}
