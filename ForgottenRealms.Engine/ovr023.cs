using System;
using System.Collections.Generic;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.CharacterFeature;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;

namespace ForgottenRealms.Engine;

public class ovr023
{
    internal string[] SpellNames =
    {
        /* AffectNames */
        string.Empty,
        "Bless",
        "Curse",
        "Cure Light Wounds",
        "Cause Light Wounds",
        "Detect Magic",
        "Protection from Evil",
        "Protection from Good",
        "Resist Cold",
        "Burning Hands",
        "Charm Person",
        "Detect Magic",
        "Enlarge",
        "Reduce",
        "Friends",
        "Magic Missile",
        "Protection From Evil",
        "Protection From Good",
        "Read Magic",
        "Shield",
        "Shocking Grasp",
        "Sleep",
        "Find Traps",
        "Hold Person",
        "Resist Fire",
        "Silence, 15' Radius",
        "Slow Poison",
        "Snake Charm",
        "Spiritual Hammer",
        "Detect Invisibility",
        "Invisibility",
        "Knock",
        "Mirror Image",
        "Ray of Enfeeblement",
        "Stinking Cloud",
        "Strength",
        "Animate Dead",
        "Cure Blindness",
        "Cause Blindness",
        "Cure Disease",
        "Cause Disease",
        "Dispel Magic",
        "Prayer",
        "Remove Curse",
        "Bestow Curse",
        "Blink",
        "Dispel Magic",
        "Fireball",
        "Haste",
        "Hold Person",
        "Invisibility, 10' Radius",
        "Lightning Bolt",
        "Protection From Evil, 10' Radius",
        "Protection From Good, 10' Radius",
        "Protection From Normal Missiles",
        "Slow",
        "Restoration",
        string.Empty,
        "Cure Serious Wounds",
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        "Cause Serious Wounds",
        "Neutralize Poison",
        "Poison",
        "Protection Evil, 10' Radius",
        "Sticks to Snakes",
        "Cure Critical Wounds",
        "Cause Critical Wounds",
        "Dispel Evil",
        "Flame Strike",
        "Raise Dead",
        "Slay Living",
        "Detect Magic",
        "Entangle",
        "Faerie Fire",
        "Invisibility to Animals",
        "Charm Monsters",
        "Confusion",
        "Dimension Door",
        "Fear",
        "Fire Shield",
        "Fumble",
        "Ice Storm",
        "Minor Globe Of Invulnerability",
        "Remove Curse",
        "Animate Dead",
        "Cloud Kill",
        "Cone of Cold",
        "Feeblemind",
        "Hold Monsters",
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        "Bestow Curse"
    };

    private string[] LevelStrings =
    {
        string.Empty,
        "1st Level",
        "2nd Level",
        "3rd Level",
        "4th Level",
        "5th Level",
        "6th Level",
        "7th Level",
        "8th Level",
        "9th Level"
    };

    private readonly ExperienceTable _experienceTable;
    private readonly AreaDamageTargetsBuilder _areaDamageTargetsBuilder;
    private readonly DisplayDriver _displayDriver;
    private readonly SoundDriver _soundDriver;
    private readonly ElectricalDamageMath _electricalDamageMath;
    private readonly Subroutine5FA44 _subroutine5FA44;
    private readonly ovr013 _ovr013;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr026 _ovr026;
    private readonly ovr027 _ovr027;
    private readonly ovr032 _ovr032;
    private readonly ovr033 _ovr033;
    private readonly seg037 _seg037;

    public ovr023(DisplayDriver displayDriver, SoundDriver soundDriver, ovr013 ovr013, ovr024 ovr024, ovr025 ovr025, ovr026 ovr026, ovr027 ovr027, ovr032 ovr032, ovr033 ovr033, seg037 seg037, ExperienceTable experienceTable, AreaDamageTargetsBuilder areaDamageTargetsBuilder, ElectricalDamageMath electricalDamageMath, Subroutine5FA44 subroutine5Fa44)
    {
        _displayDriver = displayDriver;
        _soundDriver = soundDriver;
        _ovr013 = ovr013;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr026 = ovr026;
        _ovr027 = ovr027;
        _ovr032 = ovr032;
        _ovr033 = ovr033;
        _seg037 = seg037;
        _experienceTable = experienceTable;
        _areaDamageTargetsBuilder = areaDamageTargetsBuilder;
        _electricalDamageMath = electricalDamageMath;
        _subroutine5FA44 = subroutine5Fa44;
    }

    internal bool can_learn_spell(int spell_id, Player player) /* sub_5C01E */
    {
        spell_id &= 0x7f;
        bool can_learn = false;

        switch (gbl.spellCastingTable[spell_id].spellClass)
        {
            case SpellClass.Cleric:
                if (player.stats2.Wis.full > 8 &&
                    (player.SkillLevel(SkillType.Cleric) > 0 ||
                     player.SkillLevel(SkillType.Paladin) > 8))
                {
                    can_learn = true;
                }

                break;

            case SpellClass.Druid:
                if ((player.stats2.Wis.full > 8 && player.SkillLevel(SkillType.Ranger) > 6))
                {
                    can_learn = true;
                }

                break;

            case SpellClass.MagicUser:
                if (player.stats2.Int.full > 8 &&
                    ((player.race != Race.human) ||
                     (player.activeItems.armor == null) ||
                     (gbl.game_state != GameState.Combat) ||
                     (player.SkillLevel(SkillType.Ranger) > 8) ||
                     (player.SkillLevel(SkillType.MagicUser) > 0)))
                {
                    can_learn = true;
                }

                break;

            case SpellClass.Monster:
                can_learn = false;
                break;
        }

        return can_learn;
    }

    private Set unk_5C1A2 = new Set(1, 2, 3, 4);
    private Set asc_5C1D1 = new Set(0, 67, 69, 76, 77, 83);
    private Set unk_5C1F1 = new Set(0, 69);

    internal byte spell_menu(ref int index, SpellSource spellSource)
    {
        string text;

        switch (spellSource)
        {
            case SpellSource.Cast:
                text = "Cast";
                break;

            case SpellSource.Memorize:
                text = "Memorize";
                break;

            case SpellSource.Scribe:
                text = "Scribe";
                break;

            case SpellSource.Learn:
                text = "Learn";
                break;

            default:
                text = string.Empty;
                break;
        }

        string prompt_text = text.Length > 0 ? "Choose Spell: " : "";

        int end_y = (spellSource == SpellSource.Memorize) ? 0x0F : 0x16;

        bool show_exit = spellSource != SpellSource.Learn;
        bool var_61 = false;

        if (index < 0)
        {
            var_61 = true;
            index = 0;
        }

        if (spellSource == SpellSource.Learn || spellSource == SpellSource.Cast)
        {
            var_61 = true;
        }

        MenuItem selected;
        char input_key;

        do
        {
            input_key = _ovr027.sl_select_item(out selected, ref index, ref var_61, show_exit, gbl.spell_string_list,
                end_y, 0x26, 5, 1, gbl.defaultMenuColors, text, prompt_text);
        } while (asc_5C1D1.MemberOf(input_key) == false);


        byte spell_id;
        if (unk_5C1F1.MemberOf(input_key) == true)
        {
            spell_id = 0;
        }
        else
        {
            int selected_index = gbl.spell_string_list.GetRange(0, gbl.spell_string_list.IndexOf(selected)).FindAll(mi => mi.Heading == false).Count;
            spell_id = gbl.memorize_spell_id[selected_index];

            if (spellSource == SpellSource.Scribe)
            {
                gbl.currentScroll = gbl.scribeScrolls[selected_index];
            }
        }

        gbl.spell_string_list.Clear();

        return spell_id;
    }


    internal void add_spell_to_list(byte spell_id) /* sub_5C3ED */
    {
        byte masked_id = (byte)(spell_id & 0x7F);

        var string_list = gbl.spell_string_list;

        int last_spell_level = 0;
        int index = 0;
        if (string_list.Count > 0)
        {
            index = string_list.FindAll(mi => mi.Heading == false).Count /*+1*/;
            last_spell_level = gbl.spellCastingTable[gbl.memorize_spell_id[index - 1]].spellLevel;
        }

        if (gbl.spellCastingTable[masked_id].spellLevel != last_spell_level)
        {
            string_list.Add(new MenuItem(LevelStrings[gbl.spellCastingTable[masked_id].spellLevel], true));
        }

        string_list.Add(new MenuItem(string.Format(" {0}{1}", (spell_id > 0x7F) ? '*' : ' ', SpellNames[masked_id])));

        gbl.memorize_spell_id[index] = masked_id;
    }


    internal void add_spell_to_learning_list(int spell_id) /* sub_5C5B9 */
    {
        int memorize_index;
        byte masked_id = (byte)(spell_id & 0x7F);

        var spell_list = gbl.spell_string_list;

        int sp_lvl = gbl.spellCastingTable[masked_id].spellLevel;

        if (gbl.spell_string_list.Count == 0)
        {
            System.Array.Clear(gbl.memorize_count, 0, gbl.max_spells);

            memorize_index = 0;
            gbl.memorize_count[memorize_index] = 1;
        }
        else
        {
            memorize_index = 0;

            foreach (var mi in spell_list)
            {
                if (mi.Heading == false)
                {
                    if (gbl.spellCastingTable[gbl.memorize_spell_id[memorize_index]].spellLevel > sp_lvl ||
                        gbl.memorize_spell_id[memorize_index] == masked_id)
                    {
                        break;
                    }
                }

                memorize_index++;
            }

            if (gbl.memorize_spell_id[memorize_index] != masked_id)
            {
                int insert_count = 1;
                for (int i = memorize_index; i < gbl.max_spells; i++)
                {
                    int tmp_count = gbl.memorize_count[i];
                    gbl.memorize_count[i] = insert_count;
                    insert_count = tmp_count;
                }
            }
            else
            {
                gbl.spell_string_list.RemoveAt(memorize_index);
                gbl.memorize_count[memorize_index] += 1;
            }
        }

        string menu_text = string.Format(" {0}{1}{2}",
            spell_id > 0x7F ? '*' : ' ',
            SpellNames[masked_id],
            gbl.memorize_count[memorize_index] > 1 ? string.Format(" ({0})", gbl.memorize_count[memorize_index]) : "");

        // insert before memorize_index
        gbl.spell_string_list.Insert(memorize_index, new MenuItem(menu_text));


        if (gbl.memorize_spell_id[memorize_index] != masked_id)
        {
            byte insert_id = gbl.memorize_spell_id[memorize_index];
            gbl.memorize_spell_id[memorize_index] = masked_id;

            for (int i = memorize_index + 1; i < gbl.max_spells; i++)
            {
                byte tmp_id = gbl.memorize_spell_id[i];
                gbl.memorize_spell_id[i] = insert_id;
                insert_id = tmp_id;
            }
        }
    }


    internal void scroll_5C912(bool learning) /* sub_5C912 */
    {
        if (gbl.SelectedPlayer.HasAffect(Affects.read_magic) == true ||
            ((gbl.SelectedPlayer.cleric_lvl > 0 || gbl.SelectedPlayer.cleric_old_lvl > gbl.SelectedPlayer.multiclassLevel) &&
             gbl.ItemDataTable[gbl.currentScroll.type].item_slot == ItemSlot.Quarrel))
        {
            gbl.currentScroll.hidden_names_flag = 0;
        }

        if (gbl.currentScroll.hidden_names_flag == 0)
        {
            for (byte var_1 = 1; var_1 <= 3; var_1++)
            {
                if ((learning == true && (int)gbl.currentScroll.getAffect(var_1) > 0x80) ||
                    (learning == false && (int)gbl.currentScroll.getAffect(var_1) > 0))
                {
                    add_spell_to_list((byte)gbl.currentScroll.getAffect(var_1));
                    gbl.scribeScrolls[gbl.scribeScrollsCount] = gbl.currentScroll;
                    gbl.scribeScrollsCount++;
                }
            }
        }
    }


    internal void BuildScrollSpellLists(bool showLearning) // sub_5C9F4
    {
        for (int i = 0; i < 48; i++)
        {
            gbl.scribeScrolls[i] = null;
        }

        gbl.scribeScrollsCount = 0;

        foreach (var item in gbl.SelectedPlayer.items)
        {
            gbl.currentScroll = item;

            if (item.IsScroll() == true)
            {
                scroll_5C912(showLearning);
            }
        }
    }


    internal bool BuildSpellList(SpellLoc spl_location) // sub_5CA74
    {
        bool buildSpellList = true;

        gbl.spell_string_list.Clear();

        for (int var_2 = 0; var_2 < gbl.max_spells; var_2++)
        {
            gbl.memorize_spell_id[var_2] = 0;
        }

        switch (spl_location)
        {
            case SpellLoc.memory:
                foreach (int id in gbl.SelectedPlayer.spellList.LearntList())
                {
                    if (can_learn_spell(id, gbl.SelectedPlayer))
                    {
                        add_spell_to_learning_list(id);
                    }
                }

                break;

            case SpellLoc.memorize:
                foreach (int id in gbl.SelectedPlayer.spellList.LearningList())
                {
                    if (can_learn_spell(id, gbl.SelectedPlayer))
                    {
                        add_spell_to_learning_list(id);
                    }
                }

                break;

            case SpellLoc.grimoire:
                foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                {
                    if (gbl.SelectedPlayer.KnowsSpell(spell) &&
                        can_learn_spell((int)spell, gbl.SelectedPlayer))
                    {
                        add_spell_to_learning_list((int)spell);
                    }
                }

                break;

            case SpellLoc.scroll:
                scroll_5C912(false);
                buildSpellList = false;
                break;

            case SpellLoc.scrolls:
                BuildScrollSpellLists(false);
                buildSpellList = false;
                break;

            case SpellLoc.scribe:
                BuildScrollSpellLists(true);
                buildSpellList = false;
                break;

            case SpellLoc.choose:
                foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                {
                    int sp_lvl = gbl.spellCastingTable[(int)spell].spellLevel;
                    SpellClass sp_class = gbl.spellCastingTable[(int)spell].spellClass;

                    if (sp_lvl > 5 || sp_class >= SpellClass.Monster)
                    {
                        //skip this spell
                    }
                    else if (gbl.SelectedPlayer.spellCastCount[(int)sp_class, sp_lvl - 1] > 0 &&
                             can_learn_spell((int)spell, gbl.SelectedPlayer) == true &&
                             gbl.SelectedPlayer.KnowsSpell(spell) == false)
                    {
                        add_spell_to_learning_list((int)spell);
                    }
                }

                break;
        }

        if (gbl.spell_string_list.Count > 0)
        {
            if (buildSpellList == true)
            {
                int idx = 0;
                int spellLvl = 0;

                int insert = 0;
                var inserts = new Queue<KeyValuePair<int, int>>();

                foreach (var mi in gbl.spell_string_list)
                {
                    var lastLvl = spellLvl;

                    if (gbl.memorize_spell_id[idx] != 0)
                    {
                        spellLvl = gbl.spellCastingTable[gbl.memorize_spell_id[idx]].spellLevel;
                    }

                    if (spellLvl > lastLvl)
                    {
                        inserts.Enqueue(new KeyValuePair<int, int>(insert, spellLvl));
                        insert++;
                    }

                    insert++;
                    idx++;
                }

                foreach (var vp in inserts)
                {
                    gbl.spell_string_list.Insert(vp.Key, new MenuItem(LevelStrings[vp.Value], true));
                }
            }

            return true;
        }

        return false;
    }


    internal int SpellRange(int spellId) // sub_5CDE5
    {
        int castingLvl = (gbl.spell_from_item == true) ? 6 : _ovr025.spellMaxTargetCount(spellId);
        int range = gbl.spellCastingTable[spellId].fixedRange + (gbl.spellCastingTable[spellId].perLvlRange * castingLvl);

        if (range == 0 &&
            gbl.spellCastingTable[spellId].field_6 != 0)
        {
            range = 1;
        }

        if (range == -1 || range == 0xff)
        {
            range = 1;
        }

        return range;
    }


    internal ushort GetSpellAffectTimeout(Spells spellId) // sub_5CE92
    {
        int var_4;

        if (spellId == Spells.cause_disease)
        {
            var_4 = _ovr024.roll_dice(6, 1) * 10;
        }
        else if (spellId == Spells.spell_39 || spellId == Spells.spell_3d)
        {
            var_4 = _ovr024.roll_dice(4, 5);
        }
        else if (spellId == Spells.spell_3b)
        {
            var_4 = (_ovr024.roll_dice(4, 1) * 10) + 40;
        }
        else if (spellId == Spells.spell_3f)
        {
            if (gbl.game_state == GameState.Combat)
            {
                var_4 = _ovr024.roll_dice(10, 2) * 10;
            }
            else
            {
                var_4 = (_ovr024.roll_dice(10, 1) + 10) * 10;
            }
        }
        else if (spellId == Spells.neutralize_poison)
        {
            var_4 = 1440;
        }
        else
        {
            var_4 = gbl.spellCastingTable[(int)spellId].fixedDuration + (gbl.spellCastingTable[(int)spellId].perLvlDuration * _ovr025.spellMaxTargetCount((int)spellId));
        }

        return (ushort)var_4;
    }


    internal void DoSpellCastingWork(string text, DamageType damageFlags, int damage, bool call_affect_table, int TargetCount, int spell_id) // sub_5CF7F
    {
        gbl.damage_flags = (damage == 0) ? 0 : damageFlags;

        if (gbl.spellTargets.Count > 0)
        {
            int target_count = TargetCount > 0 ? TargetCount : _ovr025.spellMaxTargetCount(spell_id);

            foreach (var target in gbl.spellTargets)
            {
                bool saved;

                if (gbl.spellCastingTable[spell_id].damageOnSave == 0)
                {
                    saved = false;
                }
                else
                {
                    saved = _ovr024.RollSavingThrow(0, gbl.spellCastingTable[spell_id].saveVerse, target);
                }

                if (gbl.spellCastingTable[spell_id].fixedRange == -1)
                {
                    _ovr025.reclac_player_values(target);

                    _ovr024.CheckAffectsEffect(target, CheckType.Type_11);

                    if (_ovr024.PC_CanHitTarget(target.ac, target, gbl.SelectedPlayer) == false)
                    {
                        damage = 0;
                        saved = true;
                    }
                }

                if (damage > 0)
                {
                    _ovr024.damage_person(saved, gbl.spellCastingTable[spell_id].damageOnSave, damage, target);
                }

                if (gbl.spellCastingTable[spell_id].affect_id > 0)
                {
                    _ovr024.ApplyAttackSpellAffect(text, saved, gbl.spellCastingTable[spell_id].damageOnSave,
                        call_affect_table, target_count, GetSpellAffectTimeout((Spells)spell_id), gbl.spellCastingTable[spell_id].affect_id,
                        target);
                }
            }

            gbl.damage_flags = 0;
        }
    }


    internal bool NonCombatSpellCast(QuickFight quick_fight, int spellId) // cast_spell_on
    {
        if (gbl.lastSelectetSpellTarget == null)
        {
            gbl.lastSelectetSpellTarget = gbl.SelectedPlayer;
        }

        gbl.spellTargets.Clear();
        gbl.spellTargets.Add(gbl.SelectedPlayer);

        bool castSpell = true;

        switch (gbl.spellCastingTable[spellId].targetType)
        {
            case SpellTargets.Self:
                break;

            case SpellTargets.PartyMember:
                _ovr025.LoadPic();

                _ovr025.selectAPlayer(ref gbl.lastSelectetSpellTarget, true, "Cast Spell on whom");

                gbl.spellTargets.Clear();
                if (gbl.lastSelectetSpellTarget == null)
                {
                    castSpell = false;
                }
                else
                {
                    gbl.spellTargets.Add(gbl.lastSelectetSpellTarget);
                }

                break;

            case SpellTargets.WholeParty:
                // prepend all players
                gbl.spellTargets.AddRange(gbl.TeamList);
                break;

            default:
                castSpell = false;
                break;
        }

        return castSpell;
    }


    internal void sub_5D2E1(bool showCastingText, QuickFight quick_fight, int spell_id) // sub_5D2E1
    {
        bool dummy = false;
        sub_5D2E1(ref dummy, showCastingText, quick_fight, spell_id);
    }


    internal void sub_5D2E1(ref bool arg_0, bool showCastingText, QuickFight quick_fight, int spell_id) // sub_5D2E1
    {
        Player caster = gbl.SelectedPlayer;
        bool stillCast = true;

        if (gbl.game_state != GameState.Combat &&
            gbl.spellCastingTable[spell_id].targetType == SpellTargets.Combat)
        {
            if (gbl.spell_from_item == false)
            {
                _displayDriver.displayString(SpellNames[spell_id], 0, 10, 0x13, 1);
                _displayDriver.displayString("can't be cast here...", 0, 10, 0x14, 1);

                if (_ovr027.yes_no(gbl.defaultMenuColors, "Lose it? ") == 'Y')
                {
                    caster.spellList.ClearSpell(spell_id);
                }
            }
            else
            {
                _displayDriver.displayString("That Item", 0, 10, 0x13, 1);
                _displayDriver.displayString("is a combat-only item...", 0, 10, 0x14, 1);

                if (_ovr027.yes_no(gbl.defaultMenuColors, "Use it? ") == 'Y')
                {
                    arg_0 = true;
                }
            }

            showCastingText = false;
            stillCast = false;
        }

        if (caster.HasAffect(Affects.affect_4a) == true)
        {
            byte dice_roll = _ovr024.roll_dice(2, 1);

            if (dice_roll == 1)
            {
                DisplayCaseSpellText(spell_id, "miscasts", caster);
                showCastingText = false;
                stillCast = false;
            }
        }

        if (showCastingText == true && gbl.spell_from_item == false)
        {
            DisplayCaseSpellText(spell_id, "casts", caster);
        }

        while (stillCast == true)
        {
            arg_0 = gbl.SpellCastFunction(quick_fight, spell_id);

            if (arg_0 == true)
            {
                stillCast = false;

                if (gbl.game_state == GameState.Combat)
                {
                    _ovr025.load_missile_icons(0x12);
                    var casterPos = _ovr033.PlayerMapPos(caster);

                    byte direction = _ovr032.FindCombatantDirection(gbl.targetPos, casterPos);

                    gbl.focusCombatAreaOnPlayer = true;
                    _ovr033.draw_74B3F(false, Icon.Attack, direction, caster);

                    if (spell_id == 0x2F)
                    {
                        _soundDriver.PlaySound(Sound.sound_b);
                    }
                    else if (spell_id == 0x33)
                    {
                        _soundDriver.PlaySound(Sound.sound_8);
                    }
                    else
                    {
                        _soundDriver.PlaySound(Sound.sound_2);
                    }

                    _ovr025.draw_missile_attack(0x1E, 4, gbl.targetPos, casterPos);

                    if (_ovr033.PlayerOnScreen(false, caster) == true)
                    {
                        _ovr033.draw_74B3F(true, Icon.Attack, caster.actions.direction, caster);
                        _ovr033.draw_74B3F(false, Icon.Normal, caster.actions.direction, caster);
                    }
                }

                _ovr024.remove_invisibility(caster);

                if (gbl.spell_from_item == false)
                {
                    caster.spellList.ClearSpell(spell_id);
                }

                gbl.spell_id = spell_id;

                var func = gbl.spellTable[(Spells)spell_id];
                func();

                gbl.spell_id = 0;
                gbl.byte_1D2C7 = false;
            }
            else
            {
                if (gbl.game_state != GameState.Combat)
                {
                    stillCast = false;
                }
                else if (quick_fight == QuickFight.True ||
                         _ovr027.yes_no(gbl.alertMenuColors, "Abort Spell? ") == 'Y')
                {
                    _ovr025.string_print01("Spell Aborted");
                    if (gbl.spell_from_item == false)
                    {
                        caster.spellList.ClearSpell(spell_id);
                    }

                    stillCast = false;
                }
            }
        }

        _ovr025.ClearPlayerTextArea();

        if (gbl.game_state == GameState.Combat)
        {
            _seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
        }
    }


    internal void MultiTargetedSpell(string text, int save_bonus) // sub_5DB24
    {
        bool firstTimeRound = true;
        foreach (var target in gbl.spellTargets)
        {
            if (firstTimeRound == false)
            {
                _soundDriver.PlaySound(Sound.sound_2);
                _ovr025.load_missile_icons(0x12);

                _ovr025.draw_missile_attack(0x1E, 4, _ovr033.PlayerMapPos(target), _ovr033.PlayerMapPos(gbl.SelectedPlayer));
            }

            bool saved;
            DamageOnSave can_save_flag;

            if ((gbl.spell_id == 0x4F || gbl.spell_id == 0x51) &&
                firstTimeRound == true)
            {
                saved = true;
                can_save_flag = DamageOnSave.Zero;
            }
            else
            {
                saved = _ovr024.RollSavingThrow(save_bonus, gbl.spellCastingTable[gbl.spell_id].saveVerse, target);
                can_save_flag = gbl.spellCastingTable[gbl.spell_id].damageOnSave;
            }

            if ((target.monsterType > MonsterType.type_1 || target.field_DE > 1) &&
                gbl.spell_id != 0x53)
            {
                saved = true;
            }

            _ovr024.ApplyAttackSpellAffect(text, saved, can_save_flag, false, _ovr025.spellMaxTargetCount(gbl.spell_id), GetSpellAffectTimeout((Spells)gbl.spell_id),
                gbl.spellCastingTable[gbl.spell_id].affect_id, target);
        }
    }


    private void CastTeamSpell(string text, CombatTeam team) // sub_5DCA0
    {
        gbl.byte_1D2C7 = true;

        gbl.spellTargets.RemoveAll(target => target.combat_team != team ||
                                             (gbl.spell_id == (int)Spells.bless && gbl.game_state == GameState.Combat && _ovr025.BuildNearTargets(1, target).Count > 0));

        DoSpellCastingWork(text, 0, 0, false, 0, gbl.spell_id);
    }


    internal void cleric_bless() /* is_Blessed */
    {
        CastTeamSpell("is Blessed", gbl.SelectedPlayer.combat_team);
    }


    internal void cleric_curse() /* is_Cursed */
    {
        CastTeamSpell("is Cursed", gbl.SelectedPlayer.OppositeTeam());
    }


    internal void SpellCureLight() /* sub_5DDBC */
    {
        if (gbl.spellTargets.Count > 0 &&
            _ovr024.heal_player(0, _ovr024.roll_dice(8, 1), gbl.spellTargets[0]) == true)
        {
            _ovr025.DescribeHealing(gbl.spellTargets[0]);
        }
    }


    internal void SpellCauseLight() /* sub_5DDF8 */
    {
        DoSpellCastingWork("", DamageType.Magic, _ovr024.roll_dice_save(8, 1), false, 0, gbl.spell_id);
    }


    internal void is_affected()
    {
        DoSpellCastingWork("is affected", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellProtectionFromX() // is_protected
    {
        DoSpellCastingWork("is protected", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellResistCold() // is_cold_resistant
    {
        DoSpellCastingWork("is cold-resistant", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellBuringHands() // sub_5DEE1
    {
        DoSpellCastingWork("", DamageType.Magic | DamageType.Fire, _ovr025.spellMaxTargetCount(gbl.spell_id), false, 0, gbl.spell_id);
    }


    internal void SpellCharm() // is_charmed
    {
        Player target = gbl.spellTargets[0];

        if (target.monsterType > MonsterType.type_1 ||
            target.field_DE > 1)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
        }
        else
        {
            DoSpellCastingWork("is charmed", 0, 0, true, (byte)(((int)gbl.SelectedPlayer.combat_team << 7) + _ovr025.spellMaxTargetCount(gbl.spell_id)), gbl.spell_id);

            Affect affect = target.GetAffect(Affects.charm_person);

            if (affect != null)
            {
                _ovr013.CallAffectTable(Effect.Add, affect, target, Affects.shield);
            }
        }
    }


    internal void SpellEnlarge() // is_stronger
    {
        Player target = gbl.spellTargets[0];
        int new_str = 18;
        int new_str100 = 0;

        switch (_ovr025.spellMaxTargetCount(gbl.spell_id))
        {
            case 1:
                new_str100 = 0;
                break;

            case 2:
                new_str100 = 1;
                break;

            case 3:
                new_str100 = 51;
                break;

            case 4:
                new_str100 = 76;
                break;

            case 5:
                new_str100 = 91;
                break;

            case 6:
                new_str100 = 100;
                break;

            case 7:
                new_str = 19;
                break;

            case 8:
                new_str = 20;
                break;

            case 9:
                new_str = 21;
                break;

            case 10:
            case 11:
                new_str = 22;
                break;
        }

        int encoded_strength;
        if (_ovr024.TryEncodeStrength(out encoded_strength, new_str100, new_str, target) == true)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is stronger", target);

            _ovr024.add_affect(true, encoded_strength, GetSpellAffectTimeout((Spells)gbl.spell_id), Affects.enlarge, target);

            _ovr024.CalcStatBonuses(Stat.STR, target);
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
        }
    }


    internal void SpellReduce() // has_been_reduced
    {
        Player target = gbl.spellTargets[0];

        if (target != null &&
            gbl.spellTargets.Count > 0 &&
            _ovr024.RollSavingThrow(0, SaveVerseType.Spell, target) == false &&
            target.HasAffect(Affects.enlarge) == true)
        {
            _ovr024.remove_affect(null, Affects.enlarge, target);
            _ovr024.CalcStatBonuses(Stat.STR, target);
            _ovr025.DisplayPlayerStatusString(true, 10, "has been reduced", target);
        }
    }


    internal void SpellFriends() // is_friendly
    {
        DoSpellCastingWork("is friendly", 0, 0, true, _ovr024.roll_dice(4, 2), gbl.spell_id);
        _ovr024.CalcStatBonuses(Stat.CHA, gbl.SelectedPlayer);
    }


    internal void SpellMagicMissile() // sub_5E221
    {
        int var_1 = _ovr025.spellMaxTargetCount(gbl.spell_id) + 1;

        DoSpellCastingWork("", DamageType.Magic, (var_1 / 2) + _ovr024.roll_dice_save(4, var_1 / 2), false, 0, gbl.spell_id);
    }


    internal void SpellShield() // is_shielded
    {
        DoSpellCastingWork("is shielded", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellShockingGrasp() // sub_5E2B2
    {
        DoSpellCastingWork("", DamageType.Acid | DamageType.Cold, _ovr024.roll_dice_save(8, 1) + _ovr025.spellMaxTargetCount(gbl.spell_id),
            false, 0, gbl.spell_id);
    }


    internal void SpellSleep() // falls_asleep
    {
        gbl.byte_1D2C7 = true;
        int totalSpellPower = _ovr024.roll_dice(4, 4);

        gbl.spellTargets.RemoveAll(target =>
        {
            int spellCost = CalcSleepCost(target);

            if (target.health_status != Status.animated &&
                target.HasAffect(Affects.sleep) == false &&
                totalSpellPower >= spellCost)
            {
                totalSpellPower -= spellCost;
                return false;
            }
            else
            {
                return true;
            }
        });

        DoSpellCastingWork("falls asleep", 0, 0, false, 0, gbl.spell_id);
    }

    private int CalcSleepCost(Player target)
    {
        int cost;
        switch (target.HitDice)
        {
            case 0:
            case 1:
                cost = 1;
                break;

            case 2:
                cost = 2;
                break;

            case 3:
                cost = 4;
                break;

            case 4:
                cost = 6;
                break;

            case 5:
                cost = (target.race == Race.monster) ? 10 : 20;
                break;

            default:
                cost = 20;
                break;
        }

        return cost;
    }


    internal void SpellHoldX() // is_held
    {
        int save_bonus;

        if (gbl.spellTargets.Count == 1)
        {
            if (gbl.spell_id == 0x17)
            {
                save_bonus = -2;
            }
            else
            {
                save_bonus = -3;
            }
        }
        else if (gbl.spellTargets.Count == 2)
        {
            save_bonus = -1;
        }
        else if (gbl.spellTargets.Count == 3 || gbl.spellTargets.Count == 4)
        {
            save_bonus = 0;
        }
        else
        {
            throw new System.NotSupportedException();
        }

        MultiTargetedSpell("is held", save_bonus);
    }


    internal void SpellFireResistant() // is_fire_resistant
    {
        DoSpellCastingWork("is fire resistant", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellSilence15Radius() // is_silenced
    {
        DoSpellCastingWork("is silenced", 0, 0, false, 0, gbl.spell_id);
    }


    internal void is_affected2()
    {
        Player player = gbl.spellTargets[0];

        if (player.health_status == Status.animated)
        {
            gbl.spellTargets.Clear();
        }
        else if (player.HasAffect(Affects.poisoned) == true)
        {
            if (player.hit_point_current == 0)
            {
                player.hit_point_current = 1;
            }

            DoSpellCastingWork("is affected", 0, 0, true, 0xff, gbl.spell_id);
            _ovr013.CallAffectTable(Effect.Remove, null, player, Affects.affect_4e);
            _ovr024.add_affect(true, 0xff, 10, Affects.poison_damage, player);
        }
    }


    internal void SpellSnakeCharm() // is_charmed2
    {
        int totalSpellPower = gbl.SelectedPlayer.hit_point_current;

        gbl.spellTargets = gbl.TeamList.FindAll(target =>
        {
            if (target.monsterType == MonsterType.snake &&
                totalSpellPower >= target.hit_point_current)
            {
                totalSpellPower -= target.hit_point_current;
                return true;
            }
            else
            {
                return false;
            }
        });

        DoSpellCastingWork("is charmed", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellSpiritualHammer() // sub_5E681
    {
        DoSpellCastingWork(string.Empty, 0, 0, true, 0, gbl.spell_id);

        _ovr013.CallAffectTable(Effect.Add, null, gbl.spellTargets[0], Affects.spiritual_hammer);
    }


    internal void is_invisible()
    {
        DoSpellCastingWork("is invisible", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellKnock()
    {
        DoSpellCastingWork("Knock-Knock", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellMirrorImage() // is_duplicated
    {
        int var_1 = _ovr024.roll_dice(4, 1) << 4;

        var_1 += _ovr025.spellMaxTargetCount(gbl.spell_id);

        DoSpellCastingWork("is duplicated", 0, 0, false, var_1, gbl.spell_id);
    }


    internal void SpellRayOfEnfeeblement() // is_weakened
    {
        DoSpellCastingWork("is weakened", 0, 0, false, 0, gbl.spell_id);
    }

    private const int StinkingCloudMaxTargets = 4;

    internal void SpellStinkingCloud() //TODO similar to spell_poisonous_cloud
    {
        byte var_12;
        int groundTile;
        int[] var_C = new int[4];

        gbl.byte_1D2C7 = true;

        byte var_10 = (byte)_ovr025.spellMaxTargetCount(gbl.spell_id);
        int count = gbl.StinkingCloud.FindAll(cell => cell.player == gbl.SelectedPlayer).Count;

        GasCloud var_8 = new GasCloud(gbl.SelectedPlayer, count, gbl.targetPos);
        gbl.StinkingCloud.Add(var_8);

        _ovr024.add_affect(true, (byte)(var_10 + (count << 4)), var_10, Affects.affect_in_stinking_cloud, gbl.SelectedPlayer);

        for (int var_11 = 0; var_11 < StinkingCloudMaxTargets; var_11++)
        {
            var_12 = gbl.SmallCloudDirections[var_11];

            _ovr033.AtMapXY(out groundTile, out var_C[var_11], gbl.targetPos + gbl.MapDirectionDelta[var_12]);

            if (groundTile > 0 && gbl.BackGroundTiles[groundTile].move_cost < 0xFF)
            {
                var_8.present[var_11] = true;
            }
            else
            {
                var_8.present[var_11] = false;
            }


            if (groundTile == 0x1E)
            {
                foreach (var var_4 in gbl.StinkingCloud)
                {
                    if (var_4 != var_8)
                    {
                        for (int var_D = 0; var_D < 4; var_D++)
                        {
                            if (var_4.present[var_D] == true &&
                                gbl.targetPos + gbl.MapDirectionDelta[var_12] == var_4.targetPos + gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_D]] &&
                                var_4.groundTile[var_D] != 0x1E)
                            {
                                groundTile = var_4.groundTile[var_D];
                            }
                        }
                    }
                }
            }
            else if (groundTile == gbl.Tile_DownPlayer)
            {
                var c = gbl.downedPlayers.FindLast(cell => cell.map == gbl.targetPos + gbl.MapDirectionDelta[var_12]);
                if (c != null)
                {
                    groundTile = c.originalBackgroundTile;
                }
            }

            var_8.groundTile[var_11] = groundTile;
            if (var_8.present[var_11] == true)
            {
                var pos = gbl.MapDirectionDelta[var_12] + gbl.targetPos;

                gbl.mapToBackGroundTile[pos] = gbl.Tile_StinkingCloud;
            }
        }

        _ovr025.DisplayPlayerStatusString(false, 10, "Creates a noxious cloud", gbl.SelectedPlayer);

        _ovr033.redrawCombatArea(8, 0xff, gbl.targetPos);
        _displayDriver.GameDelay();
        _ovr025.ClearPlayerTextArea();
        for (int var_11 = 0; var_11 < 4; var_11++)
        {
            for (int var_D = 0; var_D < 4; var_D++)
            {
                if (var_C[var_D] == var_C[var_11] &&
                    var_11 != var_D)
                {
                    var_C[var_11] = 0;
                }
            }
        }

        for (int var_11 = 0; var_11 < 4; var_11++)
        {
            if (var_C[var_11] > 0)
            {
                _ovr024.in_poison_cloud(1, gbl.player_array[var_C[var_11]]);
            }
        }
    }


    internal void SpellStrength() // sub_5EC5B
    {
        int strIncrease = 0;
        Player target = gbl.spellTargets[0];

        if (target.magic_user_lvl > 0 ||
            target.magic_user_old_lvl > target.multiclassLevel)
        {
            strIncrease = _ovr024.roll_dice(4, 1);
        }

        if (target.cleric_lvl > 0 ||
            target.cleric_old_lvl > target.multiclassLevel ||
            target.thief_lvl > 0 ||
            target.thief_old_lvl > target.multiclassLevel)
        {
            strIncrease = _ovr024.roll_dice(6, 1);
        }

        if (target.fighter_lvl > 0 ||
            target.fighter_old_lvl > target.multiclassLevel)
        {
            strIncrease = _ovr024.roll_dice(8, 1);
        }

        int str = target.stats2.Str.full + strIncrease;
        int str_100 = 0;

        if (str > 18)
        {
            if (target.fighter_lvl > 0 ||
                target.fighter_old_lvl > target.multiclassLevel ||
                target.paladin_lvl > 0 ||
                target.paladin_old_lvl > target.multiclassLevel ||
                target.ranger_lvl > 0 ||
                target.ranger_old_lvl > target.multiclassLevel)
            {
                str_100 = target.stats2.Str00.cur + ((str - 18) * 10);

                if (str_100 > 100)
                {
                    str_100 = 100;
                }

                str = 18;
            }
            else
            {
                str = 18;
            }
        }

        int encoded_str;

        if (_ovr024.TryEncodeStrength(out encoded_str, str_100, str, target) == true)
        {
            encoded_str = strIncrease + 100;

            _ovr024.add_affect(true, encoded_str, GetSpellAffectTimeout((Spells)gbl.spell_id), Affects.strength, target);
            _ovr024.CalcStatBonuses(Stat.STR, target);
        }
    }


    internal void SpellAnimateDead() // is_animated
    {
        gbl.byte_1D2C7 = true;

        int var_3 = _ovr025.spellMaxTargetCount(gbl.spell_id);

        gbl.spellTargets.Clear();

        foreach (Player player in gbl.TeamList)
        {
            if (player.health_status == Status.dead &&
                player.monsterType == 0)
            {
                if (_ovr033.sub_7515A(true, _ovr033.PlayerMapPos(player), player) == true)
                {
                    byte var_2 = (byte)(((int)player.combat_team << 4) + _ovr025.spellMaxTargetCount(gbl.spell_id));

                    player.combat_team = gbl.SelectedPlayer.combat_team;
                    player.quick_fight = QuickFight.True;
                    player.field_E9 = 1;
                    player.attackLevel = 0;
                    player.base_movement = 6;

                    player.spellList.Clear();

                    if (player.control_morale >= Control.NPC_Base)
                    {
                        player.control_morale = Control.NPC_Berzerk;
                    }
                    else
                    {
                        player.control_morale = Control.PC_Berzerk;
                    }

                    player.monsterType = MonsterType.animated_dead;

                    if (gbl.game_state == GameState.Combat)
                    {
                        player.actions.target = null;
                    }

                    var_3--;

                    if (_ovr024.combat_heal(player.hit_point_max, player) == true)
                    {
                        _ovr024.ApplyAttackSpellAffect("is animated", false, 0, true, var_2, 0, Affects.animate_dead, player);
                        player.health_status = Status.animated;
                    }
                }
            }

            if (var_3 <= 0) break;
        }
    }


    internal void SpellCureBlindness() // can_see
    {
        if (_ovr024.cure_affect(Affects.blinded, gbl.spellTargets[0]) == true)
        {
            _ovr025.MagicAttackDisplay("can see", true, gbl.spellTargets[0]);
        }
    }


    internal void SpellCauseBlindness() // is_blind
    {
        DoSpellCastingWork("is blind", 0, 0, false, 0, gbl.spell_id);
    }


    internal bool sub_5F037()
    {
        bool var_1 = false;

        gbl.cureSpell = true;

        if (_ovr024.cure_affect(Affects.cause_disease_1, gbl.spellTargets[0]) == true)
        {
            var_1 = true;
        }

        if (_ovr024.cure_affect(Affects.weaken, gbl.spellTargets[0]) == true)
        {
            var_1 = true;

            _ovr024.remove_affect(null, Affects.cause_disease_2, gbl.spellTargets[0]);
            _ovr024.remove_affect(null, Affects.helpless, gbl.spellTargets[0]);
        }

        if (_ovr024.cure_affect(Affects.hot_fire_shield, gbl.spellTargets[0]) == true)
        {
            var_1 = true;
            _ovr024.remove_affect(null, Affects.affect_39, gbl.spellTargets[0]);
        }

        gbl.cureSpell = false;

        return var_1;
    }


    internal void SpellCureDisease() // sub_5F0DC
    {
        sub_5F037();
    }


    internal void SpellCauseDisease() // is_diseased
    {
        DoSpellCastingWork("is diseased", 0, 0, true, 0, gbl.spell_id);
    }


    internal bool sub_5F126(Player arg_2, int target_count) // sub_5F126
    {
        int muLvl = arg_2.SkillLevel(SkillType.MagicUser);
        int roll;

        if (target_count > muLvl)
        {
            roll = ((target_count - muLvl) * 5) + 50;
        }
        else if (target_count < muLvl)
        {
            roll = 50 - ((muLvl - target_count) * 2);
        }
        else
        {
            roll = 50;
        }

        return _ovr024.roll_dice(100, 1) <= roll;
    }


    internal void SpellDispelMagic() // is_affected3
    {
        gbl.byte_1D2C7 = true;
        int maxTargetCount = _ovr025.spellMaxTargetCount(gbl.spell_id);

        if (gbl.spellTargets.Count > 0)
        {
            bool is_affected = false;
            Player target = gbl.spellTargets[0];

            var removeList = new List<Affect>();

            foreach (Affect var_C in target.affects)
            {
                if (var_C.affect_data < 0xff)
                {
                    int byte_1AFDE = var_C.affect_data & 0x0f;
                    int rollNeeded;

                    if (maxTargetCount > byte_1AFDE)
                    {
                        rollNeeded = 50 + ((maxTargetCount - byte_1AFDE) * 5);
                    }
                    else if (maxTargetCount < byte_1AFDE)
                    {
                        rollNeeded = 50 - ((byte_1AFDE - maxTargetCount) * 2);
                    }
                    else
                    {
                        rollNeeded = 50;
                    }

                    if (_ovr024.roll_dice(100, 1) <= rollNeeded)
                    {
                        removeList.Add(var_C);
                        is_affected = true;
                    }
                }
            }

            foreach (Affect affect in removeList)
            {
                _ovr024.remove_affect(affect, affect.type, target);
            }

            removeList.Clear();

            if (is_affected == true)
            {
                _ovr025.MagicAttackDisplay("is affected", true, target);
            }
        }

        int yPos = 0;
        int xPos = 0;

        for (int dir = 0; dir <= 8; dir++)
        {
            switch (dir)
            {
                case 0:
                    xPos = gbl.targetPos.x;
                    yPos = gbl.targetPos.y;
                    break;

                case 1:
                    yPos = gbl.targetPos.y - 1;
                    break;

                case 2:
                    xPos = gbl.targetPos.x - 1;
                    break;

                case 3:
                    yPos = gbl.targetPos.y;
                    break;

                case 4:
                    yPos = gbl.targetPos.y + 1;
                    break;

                case 5:
                    xPos = gbl.targetPos.x;
                    break;

                case 6:
                    xPos = gbl.targetPos.x - 1;
                    break;

                case 7:
                    yPos = gbl.targetPos.y;
                    break;

                case 8:
                    yPos = gbl.targetPos.y - 1;
                    break;
            }

            int dummy_byte;
            int ground_tile;
            _ovr033.AtMapXY(out ground_tile, out dummy_byte, yPos, xPos);

            if (ground_tile == 0x1C || ground_tile == 0x1E)
            {
                int targetCount = (ground_tile == 0x1C) ? 9 : 4;
                var looplist = (ground_tile == 0x1C) ? gbl.CloudKillCloud : gbl.StinkingCloud;

                var mappos = new Point(xPos, yPos);
                looplist.ForEach(var_18 =>
                {
                    for (int var_1 = 0; var_1 < targetCount; var_1++)
                    {
                        if (mappos == var_18.targetPos + gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_1]] &&
                            var_18.field_1D == false)
                        {
                            if (sub_5F126(var_18.player, maxTargetCount) == true)
                            {
                                Affect affect = null;
                                bool found = false;

                                foreach (Affect tmpAffect in var_18.player.affects)
                                {
                                    if (((affect.type == Affects.affect_in_cloud_kill && ground_tile == 0x1c) ||
                                         (affect.type == Affects.affect_in_stinking_cloud && ground_tile == 0x1E)) &&
                                        (affect.affect_data >> 4) == var_18.field_1C)
                                    {
                                        affect = tmpAffect;
                                        found = true;
                                        break;
                                    }
                                }

                                if (found == true)
                                {
                                    if (ground_tile == 0x1C)
                                    {
                                        _ovr024.remove_affect(affect, Affects.affect_in_cloud_kill, var_18.player);
                                    }
                                    else
                                    {
                                        _ovr024.remove_affect(affect, Affects.affect_in_stinking_cloud, var_18.player);
                                    }
                                }
                            }
                            else
                            {
                                var_18.field_1D = true;
                            }
                        }
                    }
                });
            }
        }
    }


    internal void SpellPrayer() // is_praying
    {
        byte tmpByte = (byte)(((int)gbl.SelectedPlayer.combat_team * 16) + _ovr025.spellMaxTargetCount(gbl.spell_id));

        DoSpellCastingWork("is praying", 0, 0, false, tmpByte, gbl.spell_id);
    }


    internal void SpellRemoveCurse() // uncurse
    {
        if (_ovr024.cure_affect(Affects.bestow_curse, gbl.spellTargets[0]) == true)
        {
            _ovr025.MagicAttackDisplay("is un-cursed", true, gbl.spellTargets[0]);
        }
        else
        {
            Item item = gbl.spellTargets[0].items.Find(i => i.cursed);

            if (item != null)
            {
                item.readied = false;

                if ((int)item.affect_3 > 0x7F)
                {
                    gbl.applyItemAffect = true;
                    _ovr013.CallAffectTable(Effect.Remove, item, gbl.spellTargets[0], item.affect_3);

                    var target = gbl.spellTargets[0];

                    _ovr024.CalcStatBonuses(Stat.STR, target);
                    _ovr024.CalcStatBonuses(Stat.INT, target);
                    _ovr024.CalcStatBonuses(Stat.WIS, target);
                    _ovr024.CalcStatBonuses(Stat.DEX, target);
                    _ovr024.CalcStatBonuses(Stat.CON, target);
                    _ovr024.CalcStatBonuses(Stat.CHA, target);
                }

                _ovr025.MagicAttackDisplay("has an item un-cursed", true, gbl.spellTargets[0]);
            }
        }
    }


    internal void curse()
    {
        DoSpellCastingWork("has been cursed!", 0, 0, false, 0, gbl.spell_id);
    }


    internal void spell_blinking()
    {
        DoSpellCastingWork("is blinking", 0, 0, false, 0, gbl.spell_id);
    }


    internal void sub_5F782()
    {
        int dice_count;

        gbl.byte_1D2C7 = true;

        if (gbl.spell_id == 0x40)
        {
            dice_count = (_ovr024.roll_dice(3, 1) * 2) + 1;
        }
        else
        {
            dice_count = _ovr025.spellMaxTargetCount(gbl.spell_id);
        }

        if (gbl.area_ptr.inDungeon == 0)
        {
            var scl = _ovr032.Rebuild_SortedCombatantList(1, 2, gbl.targetPos, sc => true);

            gbl.spellTargets.Clear();
            foreach (var sc in scl)
            {
                gbl.spellTargets.Add(sc.player);
            }
        }

        _ovr033.redrawCombatArea(8, 0, gbl.targetPos);

        DoSpellCastingWork("", DamageType.Magic | DamageType.Fire, _ovr024.roll_dice_save(6, dice_count), false, 0, gbl.spell_id);
    }


    internal void RemoveComplimentSpellFirst(string text, CombatTeam combatTeam, Affects affect) //sub_5F87B
    {
        gbl.byte_1D2C7 = true;

        int maxTargets = _ovr025.spellMaxTargetCount(gbl.spell_id);

        gbl.spellTargets.RemoveAll(target =>
        {
            if (target.combat_team == combatTeam && maxTargets > 0)
            {
                maxTargets -= 1;

                if (_ovr024.cure_affect(affect, target) == true)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        });

        DoSpellCastingWork(text, 0, 0, false, 0, gbl.spell_id);
    }


    internal void cast_haste()
    {
        RemoveComplimentSpellFirst("is Hasted", gbl.SelectedPlayer.combat_team, Affects.slow);
    }

    internal void SpellLightningBolt() // sub_5FCD9
    {
        int damage = _ovr024.roll_dice(6, _ovr025.spellMaxTargetCount(gbl.spell_id));

        _electricalDamageMath.DoElecDamage(0, SaveVerseType.Spell, damage, gbl.targetPos);
        _subroutine5FA44.sub_5FA44(1, SaveVerseType.Spell, damage, 7);
    }


    internal void SpellSlow() // sub_5FD2E
    {
        RemoveComplimentSpellFirst("is Slowed", gbl.SelectedPlayer.OppositeTeam(), Affects.haste);
    }


    internal void SpellRestoration() // cast_restore
    {
        int var_C = 30; /* simeon */

        Player player = gbl.spellTargets[0];

        if (player.lost_lvls > 0)
        {
            byte restored_hp = (byte)(player.lost_hp / player.lost_lvls);

            player.hit_point_max += restored_hp;
            player.hit_point_current += restored_hp;
            player.hit_point_rolled += restored_hp;
            player.lost_hp -= restored_hp;
            player.lost_lvls -= 1;

            int max_lvl = 13;
            int max_exp = 10000000;

            for (int skill = 0; skill <= 7; skill++)
            {
                int lvl = player.ClassLevel[skill];

                if (lvl > 0 &&
                    lvl <= max_lvl)
                {
                    var currentMinimumExperience = _experienceTable.GetMinimumExperience((ClassId)skill, lvl);
                    if (_experienceTable.IsTrainingAllowed((ClassId)skill, lvl) &&
                        currentMinimumExperience < max_exp &&
                        Limits.RaceStatLevelRestricted((ClassId)skill, player) == false)
                    {
                        max_lvl = lvl;
                        var_C = skill;
                        max_exp = currentMinimumExperience;
                    }
                }
            }

            player.ClassLevel[var_C]++;

            if (player.exp < max_exp)
            {
                player.exp = max_exp;
            }

            _ovr026.ReclacClassBonuses(player);
            _ovr025.DisplayPlayerStatusString(true, 10, "is restored", player);
        }
    }


    internal void cast_speed()
    {
        if (_ovr024.cure_affect(Affects.slow, gbl.spellTargets[0]) == false)
        {
            DoSpellCastingWork("is Speedy", 0, 0, false, 0, gbl.spell_id);
        }
    }


    internal void SpellCureSeriousWounds() // sub_5FF6D
    {
        if (gbl.spellTargets.Count > 0 &&
            _ovr024.heal_player(0, _ovr024.roll_dice(8, 2) + 1, gbl.spellTargets[0]) == true)
        {
            _ovr025.DescribeHealing(gbl.spellTargets[0]);
        }
    }


    internal void cast_strength()
    {
        int encodedStrength = 0;
        var target = gbl.spellTargets[0];

        if (_ovr024.TryEncodeStrength(out encodedStrength, 0, 0x15, target) == true)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is stronger", target);
        }

        _ovr024.add_affect(true, encodedStrength, (ushort)((_ovr024.roll_dice(4, 1) * 10) + 0x28), Affects.strength_spell, target);
        _ovr024.CalcStatBonuses(Stat.STR, target);
    }


    internal void sub_6003C()
    {
        _electricalDamageMath.DoElecDamage(0, SaveVerseType.Spell, _ovr024.roll_dice(6, 1) + 20, gbl.targetPos);
        _subroutine5FA44.sub_5FA44(0, SaveVerseType.Spell, 20, 3);
    }

    internal void cast_paralyzed()
    {
        DoSpellCastingWork("is paralyzed", 0, 0, false, 0, gbl.spell_id);
    }


    internal void cast_heal()
    {
        if (_ovr024.heal_player(0, _ovr024.roll_dice(4, 2) + 2, gbl.spellTargets[0]) == true)
        {
            _ovr025.MagicAttackDisplay("is Healed", true, gbl.spellTargets[0]);
        }
    }


    internal void cast_invisible()
    {
        DoSpellCastingWork("is invisible", 0, 0, false, 0, gbl.spell_id);
    }


    internal void dam2d4plus2()
    {
        DoSpellCastingWork("", DamageType.Magic, _ovr024.roll_dice_save(4, 2) + 2, false, 0, gbl.spell_id);
    }


    internal void SpellCauseSeriousWounds() // sub_60185
    {
        DoSpellCastingWork("", DamageType.Magic, _ovr024.roll_dice_save(8, 2) + 1, false, 0, gbl.spell_id);
    }


    internal void SpellNeutralizePoison() // cure_poison
    {
        Player target = gbl.spellTargets[0];

        if (target.health_status == Status.animated)
        {
            gbl.spellTargets.Remove(target);
        }
        else if (target.HasAffect(Affects.poisoned) == true)
        {
            if (target.hit_point_current == 0)
            {
                target.hit_point_current = 1;
            }

            gbl.cureSpell = true;

            _ovr024.remove_affect(null, Affects.poisoned, target);
            _ovr024.remove_affect(null, Affects.slow_poison, target);
            _ovr024.remove_affect(null, Affects.poison_damage, target);

            gbl.cureSpell = false;

            _ovr025.DisplayPlayerStatusString(true, 10, "is unpoisoned", target);

            target.in_combat = true;
            target.health_status = Status.okey;
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
        }
    }


    internal void SpellPoison() // sub_602D0
    {
        DoSpellCastingWork("", DamageType.Magic, 0, false, 0, gbl.spell_id);

        Player target = gbl.SelectedPlayer.actions.target;

        gbl.current_affect = Affects.poison_plus_0;
        _ovr024.CheckAffectsEffect(target, CheckType.MagicResistance);

        if (gbl.current_affect == Affects.poison_plus_0)
        {
            _ovr013.CallAffectTable(Effect.Add, null, gbl.SelectedPlayer, Affects.poison_plus_0);
        }
    }


    internal void SpellSticksToSnakes() // cast_flattern
    {
        if (gbl.spellTargets[0].HitDice < 6)
        {
            DoSpellCastingWork("", DamageType.Magic, 0, false, _ovr025.spellMaxTargetCount(gbl.spell_id), gbl.spell_id);

            Affect affect;
            if (_ovr025.FindAffect(out affect, Affects.sticks_to_snakes, gbl.spellTargets[0]) == true)
            {
                _ovr013.CallAffectTable(Effect.Add, affect, gbl.spellTargets[0], Affects.sticks_to_snakes);
            }
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "smashes them flat", gbl.spellTargets[0]);
        }
    }


    internal void SpellCureCriticalWounds() // sub_603F0
    {
        if (gbl.spellTargets.Count > 0 &&
            _ovr024.heal_player(0, _ovr024.roll_dice(8, 3) + 3, gbl.spellTargets[0]) == true)
        {
            _ovr025.DescribeHealing(gbl.spellTargets[0]);
        }
    }


    internal void SpellCauseCriticalWounds() // sub_60431
    {
        DoSpellCastingWork("", DamageType.Magic, _ovr024.roll_dice_save(8, 3) + 3, false, 0, gbl.spell_id);
    }


    internal void SpellDispelEvil() // is_affected4
    {
        _ovr024.ApplyAttackSpellAffect(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(Spells.dispel_evil), Affects.dispel_evil, gbl.SelectedPlayer);
        DoSpellCastingWork("is affected", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellFlameStrike() // sub_604DA
    {
        DoSpellCastingWork("", DamageType.Magic | DamageType.Fire, _ovr024.roll_dice_save(8, 6), false, 0, gbl.spell_id);
    }


    internal void SpellRaiseDead() // cast_raise
    {
        Player player = gbl.spellTargets[0];

        if ((player.health_status == Status.dead || player.health_status == Status.animated) &&
            player.stats2.Con.cur > 0 &&
            player.race != Race.elf)
        {
            gbl.cureSpell = true;

            _ovr024.remove_affect(null, Affects.animate_dead, player);
            _ovr024.remove_affect(null, Affects.poisoned, player);
            gbl.cureSpell = false;

            player.health_status = Status.okey;
            player.in_combat = true;
            player.stats2.Con.cur--;

            _ovr024.CalcStatBonuses(Stat.CON, player);
            player.hit_point_current = 1;

            _ovr025.DisplayPlayerStatusString(true, 10, "is raised", player);
        }
    }


    internal void SpellSlayLiving() //cast_slay
    {
        Player target = gbl.spellTargets[0];
        gbl.damage_flags = DamageType.Unknown40;
        gbl.damage = 67;
        _ovr024.CheckAffectsEffect(target, CheckType.MagicResistance);

        if (gbl.damage != 0)
        {
            if (_ovr024.RollSavingThrow(0, SaveVerseType.Spell, target) == false)
            {
                _ovr024.KillPlayer("is slain", Status.dead, target);
            }
            else
            {
                gbl.damage_flags = DamageType.Magic;

                _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(8, 2) + 1, target);
            }
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
        }
    }


    internal void SpellEntangle() // cast_entangle
    {
        if (gbl.area_ptr.inDungeon == 0)
        {
            foreach (var target in gbl.spellTargets)
            {
                bool saved = _ovr024.RollSavingThrow(0, SaveVerseType.Spell, target);

                _ovr024.ApplyAttackSpellAffect("is entangled", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout((Spells)0x88), Affects.entangle, target);
            }
        }
    }


    internal void SpellFaerieFire() /* cast_highlisht */
    {
        MultiTargetedSpell("is highlighted", 0);
    }


    internal void SpellInvisToAnimals() // cast_invisible2
    {
        DoSpellCastingWork("is invisible", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellCharmMonsters() // cast_charmed
    {
        MultiTargetedSpell("is charmed", 0);

        foreach (var target in gbl.spellTargets)
        {
            Affect affect;

            if (_ovr025.FindAffect(out affect, Affects.charm_person, target) == true)
            {
                _ovr013.CallAffectTable(Effect.Add, affect, target, Affects.charm_person);
            }
        }
    }


    internal void SpellConfusion() // cast_confuse
    {
        int target_count = _ovr024.roll_dice(8, 2);

        if (gbl.spellTargets.Count > target_count)
        {
            gbl.spellTargets.RemoveRange(target_count, gbl.spellTargets.Count - target_count);
        }

        foreach (var target in gbl.spellTargets)
        {
            bool saved = _ovr024.RollSavingThrow(0, SaveVerseType.Spell, target);

            _ovr024.ApplyAttackSpellAffect("is confused", saved, DamageOnSave.Zero, false, 0, GetSpellAffectTimeout(Spells.confusion), Affects.cause_disease_2, target);
        }
    }


    internal void SpellDimensionDoor() // cast_teleport
    {
        Affect affect;
        Player player = gbl.SelectedPlayer;

        if (_ovr025.FindAffect(out affect, Affects.clear_movement, player) == true)
        {
            var scl = _ovr032.Rebuild_SortedCombatantList(1, 1, _ovr033.PlayerMapPos(player), sc => true);

            foreach (var sc in scl)
            {
                Player playerB = sc.player;

                if (_ovr025.FindAffect(out affect, Affects.owlbear_hug_round_attack, playerB) == true ||
                    _ovr025.FindAffect(out affect, Affects.affect_8b, playerB) == true)
                {
                    if (gbl.player_array[affect.affect_data] == player)
                    {
                        _ovr024.remove_affect(null, Affects.owlbear_hug_round_attack, playerB);
                        _ovr024.remove_affect(null, Affects.affect_8b, playerB);
                    }
                }
            }
        }

        _ovr033.RedrawPlayerBackground(_ovr033.GetPlayerIndex(player));

        _ovr033.sub_7515A(false, gbl.targetPos, player);

        _ovr033.redrawCombatArea(8, 0, _ovr033.PlayerMapPos(player));

        _ovr025.DisplayPlayerStatusString(true, 10, "teleports", player);
    }


    internal void SpellFear() /* cast_terror */
    {
        Player caster = gbl.SelectedPlayer;

        Point casterPos = _ovr033.PlayerMapPos(caster);
        _areaDamageTargetsBuilder.BuildAreaDamageTargets(6, 3, gbl.targetPos, casterPos);

        foreach (var target in gbl.spellTargets)
        {
            bool saves = _ovr024.RollSavingThrow(0, SaveVerseType.Spell, target);

            if (saves == false)
            {
                _ovr024.ApplyAttackSpellAffect("runs in terror", saves, DamageOnSave.Zero, true, 0, GetSpellAffectTimeout(Spells.fear), Affects.fear, target);
                target.actions.fleeing = true;
                target.quick_fight = QuickFight.True;

                if (target.control_morale < Control.NPC_Base)
                {
                    target.control_morale = Control.PC_Berzerk;
                }

                target.actions.target = null;
            }
            else
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", target);
            }
        }
    }


    internal void SpellFireProtection() // cast_protection
    {
        char input_key;

        bool var_3 = false;

        do
        {
            if (gbl.SelectedPlayer.quick_fight == QuickFight.True)
            {
                if (_ovr024.roll_dice(10, 1) > 5)
                {
                    input_key = 'H';
                }
                else
                {
                    input_key = 'C';
                }
            }
            else
            {
                input_key = _ovr027.displayInput(false, 0, gbl.defaultMenuColors, "Hot Cold", "flame type: ");
            }

            if (input_key == 'H')
            {
                _ovr024.ApplyAttackSpellAffect("is protected", false, 0, false, 0, GetSpellAffectTimeout(Spells.fire_shield), Affects.hot_fire_shield, gbl.SelectedPlayer);
                _ovr024.ApplyAttackSpellAffect(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(Spells.fire_shield), Affects.protect_elec, gbl.SelectedPlayer);
                var_3 = true;
            }
            else if (input_key == 'C')
            {
                _ovr024.ApplyAttackSpellAffect(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(Spells.fire_shield), Affects.cold_fire_shield, gbl.SelectedPlayer);
                _ovr024.ApplyAttackSpellAffect(string.Empty, false, 0, false, 0, GetSpellAffectTimeout(Spells.fire_shield), Affects.protect_elec, gbl.SelectedPlayer);
                var_3 = true;
            }
            else
            {
                input_key = _ovr027.displayInput(false, 0, gbl.defaultMenuColors, "Yes No", "Abort spell? ");

                if (input_key == 'Y')
                {
                    var_3 = true;
                }
            }
        } while (var_3 == false);
    }


    internal void SpellFumble() // spell_slow
    {
        Player target = gbl.spellTargets[0];
        gbl.damage_flags = DamageType.Unknown40;

        if (_ovr024.RollSavingThrow(0, SaveVerseType.Spell, target) == false)
        {
            _ovr024.ApplyAttackSpellAffect("is clumsy", false, 0, false, 0, GetSpellAffectTimeout(Spells.fumble), Affects.fumbling, target);

            if (target.HasAffect(Affects.fumbling) == true)
            {
                _ovr013.CallAffectTable(Effect.Add, null, target, Affects.fumbling);
            }
        }
        else
        {
            _ovr024.ApplyAttackSpellAffect("is slowed", false, 0, false, 0, GetSpellAffectTimeout(Spells.fumble), Affects.slow, target);

            if (target.HasAffect(Affects.slow) == true)
            {
                _ovr013.CallAffectTable(Effect.Add, null, target, Affects.slow);
            }
        }

        DoSpellCastingWork("is clumsy", 0, 0, true, 0, gbl.spell_id);
    }


    internal void SpellIceStorm() // sub_60F0B
    {
        DoSpellCastingWork("", DamageType.Acid, _ovr024.roll_dice_save(10, 3), false, 0, gbl.spell_id);
    }


    internal void SpellMinorGlobeOfInvulnerability() // sub_60F4E
    {
        DoSpellCastingWork("is protected", 0, 0, false, 0, gbl.spell_id);
    }


    internal void SpellCloudKill() // spell_poisonous_cloud // similar to create_noxious_cloud
    {
        byte dir = 0;
        int var_16;
        int ground_tile = 0;
        const int max_targets = 9;
        int[] targets = new int[max_targets];

        gbl.byte_1D2C7 = true;

        byte var_15 = (byte)_ovr025.spellMaxTargetCount(gbl.spell_id);
        int count = gbl.CloudKillCloud.FindAll(cell => cell.player == gbl.SelectedPlayer).Count;

        GasCloud var_8 = new GasCloud(gbl.SelectedPlayer, count, gbl.targetPos);
        gbl.CloudKillCloud.Add(var_8);

        _ovr024.add_affect(true, (byte)(var_15 + (count << 4)), var_15, Affects.affect_in_cloud_kill, gbl.SelectedPlayer);

        for (var_16 = 0; var_16 < max_targets; var_16++)
        {
            dir = gbl.CloudDirections[var_16];

            _ovr033.AtMapXY(out ground_tile, out targets[var_16], gbl.targetPos + gbl.MapDirectionDelta[dir]);

            if (ground_tile > 0 &&
                gbl.BackGroundTiles[ground_tile].move_cost < 0xff)
            {
                var_8.present[var_16] = true;
            }
            else
            {
                var_8.present[var_16] = false;
            }

            if (ground_tile == gbl.Tile_StinkingCloud)
            {
                bool found = false;
                foreach (var var_4 in gbl.StinkingCloud)
                {
                    for (int var_12 = 0; var_12 < 4; var_12++)
                    {
                        if (var_4.present[var_12] == true &&
                            (gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_12]] + var_4.targetPos) == (gbl.MapDirectionDelta[dir] + gbl.targetPos) &&
                            var_4.groundTile[var_12] != 0x1E &&
                            var_4.groundTile[var_12] != 0x1C)
                        {
                            ground_tile = var_4.groundTile[var_12];
                            found = true;
                        }
                    }

                    if (found) break;
                }
            }
            else if (ground_tile == gbl.Tile_CloudKill)
            {
                bool found = false;
                foreach (GasCloud var_4 in gbl.CloudKillCloud)
                {
                    if (var_4 != var_8)
                    {
                        for (int var_12 = 0; var_12 < 9; var_12++)
                        {
                            if (var_4.present[var_12] == true &&
                                (gbl.MapDirectionDelta[gbl.CloudDirections[var_12]] + var_4.targetPos) == (gbl.MapDirectionDelta[dir] + gbl.targetPos) &&
                                var_4.groundTile[var_12] != 0x1E &&
                                var_4.groundTile[var_12] != 0x1C)
                            {
                                ground_tile = var_4.groundTile[var_12];
                                found = true;
                            }
                        }
                    }

                    if (found) break;
                }
            }
            else if (ground_tile == gbl.Tile_DownPlayer)
            {
                var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

                var c = gbl.downedPlayers.FindLast(cell => cell.map == pos);
                if (c != null)
                {
                    ground_tile = c.originalBackgroundTile;
                }
            }

            var_8.groundTile[var_16] = ground_tile;

            if (var_8.present[var_16] == true)
            {
                var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

                gbl.mapToBackGroundTile[pos] = gbl.Tile_CloudKill;
            }
        }

        var_8.groundTile[var_16] = ground_tile;

        if (var_8.present[var_16] == true)
        {
            var pos = gbl.MapDirectionDelta[dir] + gbl.targetPos;

            gbl.mapToBackGroundTile[pos] = gbl.Tile_CloudKill;
        }

        _ovr025.DisplayPlayerStatusString(false, 10, "Creates a poisonous cloud", gbl.SelectedPlayer);

        _ovr033.redrawCombatArea(8, 0xFF, gbl.targetPos);
        _displayDriver.GameDelay();
        _ovr025.ClearPlayerTextArea();

        for (int idx = 0; idx < max_targets; idx++)
        {
            if (targets[idx] > 0)
            {
                _ovr024.in_poison_cloud(1, gbl.player_array[targets[idx]]);
            }
        }
    }


    internal void SpellConeOfCold() // sub_61550
    {
        Player player = gbl.SelectedPlayer;
        int target_count = _ovr025.spellMaxTargetCount(gbl.spell_id);
        int max_range = (target_count + 1) / 2;

        if (max_range < 1)
        {
            max_range = 1;
        }

        Point casterPos = _ovr033.PlayerMapPos(player);
        _areaDamageTargetsBuilder.BuildAreaDamageTargets(max_range, 2, gbl.targetPos, casterPos);

        DoSpellCastingWork("", DamageType.Acid, target_count + _ovr024.roll_dice_save(4, target_count), false, 0, gbl.spell_id);
    }


    internal void SpellFeeblemind() // sub_615F2
    {
        Player target = gbl.spellTargets[0];
        int saveTypeSpell = (int)SaveVerseType.Spell;

        // Different classes have adjustments to their saving throw for this spell.
        var oldBonus = target.saveVerse[saveTypeSpell];

        if (target._class == ClassId.cleric)
        {
            target.saveVerse[saveTypeSpell] -= 1;
        }
        else if (target._class == ClassId.magic_user)
        {
            target.saveVerse[saveTypeSpell] += 4;
        }
        else
        {
            target.saveVerse[saveTypeSpell] += 2;
        }

        gbl.damage_flags = 0;

        DoSpellCastingWork(string.Empty, 0, 0, false, 0, gbl.spell_id);

        if (target.HasAffect(Affects.feeblemind) == true)
        {
            _ovr013.CallAffectTable(Effect.Add, null, target, Affects.feeblemind);
        }

        target.saveVerse[saveTypeSpell] = oldBonus;
    }


    internal void sub_616CC()
    {
        DoSpellCastingWork("", 0, 0, false, 0, gbl.spell_id);
    }


    internal void sub_61727()
    {
        Player attacker = gbl.SelectedPlayer;

        Point casterPos = _ovr033.PlayerMapPos(attacker);
        _areaDamageTargetsBuilder.BuildAreaDamageTargets(3, 1, gbl.targetPos, casterPos);

        foreach (var target in gbl.spellTargets)
        {
            bool change_damage = target.monsterType != MonsterType.plant;

            _ovr024.damage_person(change_damage, gbl.spellCastingTable[(int)Spells.spell_62].damageOnSave, _ovr024.roll_dice_save(6, 6), target);
        }
    }

    internal void cast_heal2()
    {
        if (_ovr024.heal_player(0, _ovr024.roll_dice(4, 2) + 2, gbl.spellTargets[0]) == true)
        {
            _ovr025.MagicAttackDisplay("is Healed", true, gbl.spellTargets[0]);
        }
    }

    internal void remove_spell_from_scroll(int affect, Item item, Player player) /* sub_623FF */
    {
        int affect_index = 0;

        for (int index = 1; index <= 3; index++)
        {
            if (((int)item.getAffect(index) & 0x7F) == affect)
            {
                affect_index = index;
            }
        }

        if (affect_index != 0)
        {
            item.setAffect(affect_index, 0);
            item.namenum2 -= 1;
            if (item.namenum2 < 0xd2)
            {
                _ovr025.lose_item(item, player);
            }
        }
    }


    internal void DisplayCaseSpellText(int spellId, string arg_2, Player player) /* cast_a_spell */
    {
        if (gbl.game_state == GameState.Combat)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "Casts a Spell", player);
            _seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);

            _displayDriver.displayString("Spell:" + SpellNames[spellId], 0, 10, 0x17, 0);
        }
        else
        {
            _seg037.draw8x8_clear_area(0x16, 0x26, 0x12, 1);

            _ovr025.displayPlayerName(false, 0x13, 1, player);

            _displayDriver.displayString(arg_2, 0, 10, 0x13, player.name.Length + 2);
            _displayDriver.displayString(SpellNames[spellId], 0, 10, 0x14, 1);
            _displayDriver.GameDelay();
            _ovr025.ClearPlayerTextArea();
        }
    }


    internal void setup_spells()
    {
        gbl.cureSpell = false;
        gbl.spell_from_item = false;
        gbl.lastSelectetSpellTarget = null;
        gbl.byte_1D2C8 = true;

        gbl.SpellCastFunction = new spellDelegate(NonCombatSpellCast);

        gbl.spellTable = new Dictionary<Spells, spellDelegate2>();

        gbl.spellTable.Add(Spells.bless, cleric_bless);
        gbl.spellTable.Add(Spells.curse, cleric_curse);
        gbl.spellTable.Add(Spells.cure_light_wounds, SpellCureLight);
        gbl.spellTable.Add(Spells.cause_light_wounds, SpellCauseLight);
        gbl.spellTable.Add(Spells.detect_magic_CL, is_affected);
        gbl.spellTable.Add(Spells.protect_from_evil_CL, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.protect_from_good_CL, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.resist_cold, SpellResistCold);
        gbl.spellTable.Add(Spells.burning_hands, SpellBuringHands);
        gbl.spellTable.Add(Spells.charm_person, SpellCharm);
        gbl.spellTable.Add(Spells.detect_magic_MU, is_affected);
        gbl.spellTable.Add(Spells.enlarge, SpellEnlarge);
        gbl.spellTable.Add(Spells.reduce, SpellReduce);
        gbl.spellTable.Add(Spells.friends, SpellFriends);
        gbl.spellTable.Add(Spells.magic_missile, SpellMagicMissile);
        gbl.spellTable.Add(Spells.protect_from_evil_MU, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.protect_from_good_MU, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.read_magic, is_affected);
        gbl.spellTable.Add(Spells.shield, SpellShield);
        gbl.spellTable.Add(Spells.shocking_grasp, SpellShockingGrasp);
        gbl.spellTable.Add(Spells.sleep, SpellSleep);
        gbl.spellTable.Add(Spells.find_traps, is_affected);
        gbl.spellTable.Add(Spells.hold_person_CL, SpellHoldX);
        gbl.spellTable.Add(Spells.resist_fire, SpellFireResistant);
        gbl.spellTable.Add(Spells.silence_15_radius, SpellSilence15Radius);
        gbl.spellTable.Add(Spells.slow_poison, is_affected2);
        gbl.spellTable.Add(Spells.snake_charm, SpellSnakeCharm);
        gbl.spellTable.Add(Spells.spiritual_hammer, SpellSpiritualHammer);
        gbl.spellTable.Add(Spells.detect_invisibility, is_affected);
        gbl.spellTable.Add(Spells.invisibility, is_invisible);
        gbl.spellTable.Add(Spells.knock, SpellKnock);
        gbl.spellTable.Add(Spells.mirror_image, SpellMirrorImage);
        gbl.spellTable.Add(Spells.ray_of_enfeeblement, SpellRayOfEnfeeblement);
        gbl.spellTable.Add(Spells.stinking_cloud, SpellStinkingCloud);
        gbl.spellTable.Add(Spells.strength, SpellStrength);
        gbl.spellTable.Add(Spells.animate_dead, SpellAnimateDead);
        gbl.spellTable.Add(Spells.cure_blindness, SpellCureBlindness);
        gbl.spellTable.Add(Spells.cause_blindness, SpellCauseBlindness);
        gbl.spellTable.Add(Spells.cure_disease, SpellCureDisease);
        gbl.spellTable.Add(Spells.cause_disease, SpellCauseDisease);
        gbl.spellTable.Add(Spells.dispel_magic_CL, SpellDispelMagic);
        gbl.spellTable.Add(Spells.prayer, SpellPrayer);
        gbl.spellTable.Add(Spells.remove_curse, SpellRemoveCurse);
        gbl.spellTable.Add(Spells.bestow_curse_CL, curse);
        gbl.spellTable.Add(Spells.blink, spell_blinking);
        gbl.spellTable.Add(Spells.dispel_magic_MU, SpellDispelMagic);
        gbl.spellTable.Add(Spells.fireball, sub_5F782);
        gbl.spellTable.Add(Spells.haste, cast_haste);
        gbl.spellTable.Add(Spells.hold_person_MU, SpellHoldX);
        gbl.spellTable.Add(Spells.invisibility_10_radius, is_invisible);
        gbl.spellTable.Add(Spells.lightning_bolt, SpellLightningBolt);
        gbl.spellTable.Add(Spells.protect_from_evil_10_rad, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.protect_from_good_10_rad, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.protect_from_normal_missiles, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.slow, SpellSlow);
        gbl.spellTable.Add(Spells.restoration, SpellRestoration);
        gbl.spellTable.Add(Spells.spell_39, cast_speed);
        gbl.spellTable.Add(Spells.cure_serious_wounds, SpellCureSeriousWounds);
        gbl.spellTable.Add(Spells.spell_3b, cast_strength);
        gbl.spellTable.Add(Spells.spell_3c, sub_6003C);
        gbl.spellTable.Add(Spells.spell_3d, cast_paralyzed);
        gbl.spellTable.Add(Spells.spell_3e, cast_heal);
        gbl.spellTable.Add(Spells.spell_3f, cast_invisible);
        gbl.spellTable.Add(Spells.spell_40, sub_5F782);
        gbl.spellTable.Add(Spells.spell_41, dam2d4plus2);
        gbl.spellTable.Add(Spells.cause_serious_wounds, SpellCauseSeriousWounds);
        gbl.spellTable.Add(Spells.neutralize_poison, SpellNeutralizePoison);
        gbl.spellTable.Add(Spells.poison, SpellPoison);
        gbl.spellTable.Add(Spells.protect_evil_10_rad, SpellProtectionFromX);
        gbl.spellTable.Add(Spells.sticks_to_snakes, SpellSticksToSnakes);
        gbl.spellTable.Add(Spells.cure_critical_wounds, SpellCureCriticalWounds);
        gbl.spellTable.Add(Spells.cause_critical_wounds, SpellCauseCriticalWounds);
        gbl.spellTable.Add(Spells.dispel_evil, SpellDispelEvil);
        gbl.spellTable.Add(Spells.flame_strike, SpellFlameStrike);
        gbl.spellTable.Add(Spells.raise_dead, SpellRaiseDead);
        gbl.spellTable.Add(Spells.slay_living, SpellSlayLiving);
        gbl.spellTable.Add(Spells.detect_magic_DR, is_affected);
        gbl.spellTable.Add(Spells.entangle, SpellEntangle);
        gbl.spellTable.Add(Spells.faerie_fire, SpellFaerieFire);
        gbl.spellTable.Add(Spells.invisibility_to_animals, SpellInvisToAnimals);
        gbl.spellTable.Add(Spells.charm_monsters, SpellCharmMonsters);
        gbl.spellTable.Add(Spells.confusion, SpellConfusion);
        gbl.spellTable.Add(Spells.dimension_door, SpellDimensionDoor);
        gbl.spellTable.Add(Spells.fear, SpellFear);
        gbl.spellTable.Add(Spells.fire_shield, SpellFireProtection);
        gbl.spellTable.Add(Spells.fumble, SpellFumble);
        gbl.spellTable.Add(Spells.ice_storm, SpellIceStorm);
        gbl.spellTable.Add(Spells.minor_globe_of_invuln, SpellMinorGlobeOfInvulnerability);
        gbl.spellTable.Add(Spells.spell_59, SpellRemoveCurse);
        gbl.spellTable.Add(Spells.spell_5a, SpellAnimateDead);
        gbl.spellTable.Add(Spells.cloud_kill, SpellCloudKill);
        gbl.spellTable.Add(Spells.cone_of_cold, SpellConeOfCold);
        gbl.spellTable.Add(Spells.feeblemind, SpellFeeblemind);
        gbl.spellTable.Add(Spells.hold_monsters, SpellHoldX);
        gbl.spellTable.Add(Spells.spell_5f, sub_616CC);
        gbl.spellTable.Add(Spells.spell_60, sub_616CC);
        gbl.spellTable.Add(Spells.spell_61, sub_616CC);
        gbl.spellTable.Add(Spells.spell_62, sub_61727);
        gbl.spellTable.Add(Spells.spell_63, cast_heal2);
        gbl.spellTable.Add(Spells.bestow_curse_MU, curse);
    }
}
