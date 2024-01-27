using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr013
{
    private readonly ovr014 _ovr014;
    private readonly ovr020 _ovr020;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr032 _ovr032;
    private readonly ovr033 _ovr033;
    private readonly DisplayDriver _displayDriver;

    public ovr013(ovr014 ovr014, ovr020 ovr020, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr032 ovr032, ovr033 ovr033, DisplayDriver displayDriver)
    {
        _ovr014 = ovr014;
        _ovr020 = ovr020;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr032 = ovr032;
        _ovr033 = ovr033;
        _displayDriver = displayDriver;
    }

    internal void SetupAffectTables() // setup_spells2
    {
        affect_table = new System.Collections.Generic.Dictionary<Affects, affectDelegate>();

        affect_table.Add(Affects.bless, Bless);
        affect_table.Add(Affects.cursed, Curse);
        affect_table.Add(Affects.sticks_to_snakes, SticksToSnakes);
        affect_table.Add(Affects.dispel_evil, DispelEvil);
        affect_table.Add(Affects.detect_magic, empty);
        affect_table.Add(Affects.affect_06, BonusVsMonstersX);
        affect_table.Add(Affects.faerie_fire, FaerieFire);
        affect_table.Add(Affects.protection_from_evil, affect_protect_evil);
        affect_table.Add(Affects.protection_from_good, affect_protect_good);
        affect_table.Add(Affects.resist_cold, affect_resist_cold);
        affect_table.Add(Affects.charm_person, affect_charm_person);
        affect_table.Add(Affects.enlarge, empty);
        affect_table.Add(Affects.reduce, Suffocates);
        affect_table.Add(Affects.friends, empty);
        affect_table.Add(Affects.poison_damage, AffectPoisonDamage);
        affect_table.Add(Affects.read_magic, empty);
        affect_table.Add(Affects.shield, AffectShield);
        affect_table.Add(Affects.gnome_vs_man_sized_giant, AffectGnomeVsManSizedGiant);
        affect_table.Add(Affects.find_traps, empty);
        affect_table.Add(Affects.resist_fire, AffectResistFire);
        affect_table.Add(Affects.silence_15_radius, is_silenced1);
        affect_table.Add(Affects.slow_poison, AffectSlowPoison);
        affect_table.Add(Affects.spiritual_hammer, affect_spiritual_hammer);
        affect_table.Add(Affects.detect_invisibility, empty);
        affect_table.Add(Affects.invisibility, sub_3A6C6);
        affect_table.Add(Affects.dwarf_vs_orc, AffectDwarfVsOrc);
        affect_table.Add(Affects.fumbling, sub_3A071);
        affect_table.Add(Affects.mirror_image, MirrorImage);
        affect_table.Add(Affects.ray_of_enfeeblement, three_quarters_damage);
        affect_table.Add(Affects.stinking_cloud, StinkingCloud);
        affect_table.Add(Affects.helpless, sub_3A071);
        affect_table.Add(Affects.animate_dead, sub_3A89E);
        affect_table.Add(Affects.blinded, AffectBlinded);
        affect_table.Add(Affects.cause_disease_1, AffectCauseDisease);
        affect_table.Add(Affects.confuse, AffectConfuse);
        affect_table.Add(Affects.bestow_curse, affect_curse);
        affect_table.Add(Affects.blink, AffectBlink);
        affect_table.Add(Affects.strength, empty);
        affect_table.Add(Affects.haste, AffectHaste);
        affect_table.Add(Affects.affect_in_stinking_cloud, StinkingCloudAffect);
        affect_table.Add(Affects.prot_from_normal_missiles, AffectProtNormalMissles);
        affect_table.Add(Affects.slow, AffectSlow);
        affect_table.Add(Affects.weaken, weaken);
        affect_table.Add(Affects.cause_disease_2, sub_3B0C2);
        affect_table.Add(Affects.prot_from_evil_10_radius, affect_protect_evil);
        affect_table.Add(Affects.prot_from_good_10_radius, affect_protect_good);
        affect_table.Add(Affects.dwarf_and_gnome_vs_giants, AffectDwarfGnomeVsGiants);
        affect_table.Add(Affects.affect_30, sub_3B1A2);
        affect_table.Add(Affects.prayer, AffectPrayer);
        affect_table.Add(Affects.hot_fire_shield, HotFireShield);
        affect_table.Add(Affects.snake_charm, sub_3A071);
        affect_table.Add(Affects.paralyze, sub_3A071);
        affect_table.Add(Affects.sleep, sub_3A071);
        affect_table.Add(Affects.cold_fire_shield, ColdFireShield);
        affect_table.Add(Affects.poisoned, empty);
        affect_table.Add(Affects.item_invisibility, sub_3B27B);
        affect_table.Add(Affects.affect_39, _ovr014.engulfs);
        affect_table.Add(Affects.clear_movement, AffectClearMovement);
        affect_table.Add(Affects.regenerate, AffectRegenration);
        affect_table.Add(Affects.resist_normal_weapons, AffectResistWeapons);
        affect_table.Add(Affects.fire_resist, AffectFireResist);
        affect_table.Add(Affects.highConRegen, AffectHighConRegen);
        affect_table.Add(Affects.minor_globe_of_invulnerability, AffectMinorGlobeOfInvulnerability);
        affect_table.Add(Affects.poison_plus_0, AffectPoisonPlus0);
        affect_table.Add(Affects.poison_plus_4, AffectPoisonPlus4);
        affect_table.Add(Affects.poison_plus_2, AffectPoisonPlus2);
        affect_table.Add(Affects.thri_kreen_paralyze, ThriKreenParalyze);
        affect_table.Add(Affects.feeblemind, AffectFeebleMind);
        affect_table.Add(Affects.invisible_to_animals, AffectInvisToAnimals);
        affect_table.Add(Affects.poison_neg_2, AffectPoisonNeg2);
        affect_table.Add(Affects.invisible, AffectInvisible);
        affect_table.Add(Affects.camouflage, AffectCamouflage);
        affect_table.Add(Affects.prot_drag_breath, ProtDragonsBreath);
        affect_table.Add(Affects.affect_4a, empty);
        affect_table.Add(Affects.weap_dragon_slayer, AffectDragonSlayer);
        affect_table.Add(Affects.weap_frost_brand, AffectFrostBrand);
        affect_table.Add(Affects.berserk, AffectBerzerk);
        affect_table.Add(Affects.affect_4e, sub_3B8D9);
        affect_table.Add(Affects.fireAttack_2d10, MagicFireAttack_2d10);
        affect_table.Add(Affects.ankheg_acid_attack, AnkhegAcidAttack);
        affect_table.Add(Affects.half_damage, half_damage);
        affect_table.Add(Affects.resist_fire_and_cold, AffectResistFireAndCold);
        affect_table.Add(Affects.paralizing_gaze, _ovr023.AffectParalizingGaze);
        affect_table.Add(Affects.shambling_absorb_lightning, AffectShamblerAbsorbLightning);
        affect_table.Add(Affects.affect_55, sub_3BA14);
        affect_table.Add(Affects.spit_acid, _ovr023.AffectSpitAcid);
        affect_table.Add(Affects.affect_57, _ovr014.attack_or_kill);
        affect_table.Add(Affects.breath_elec, _ovr023.DragonBreathElec);
        affect_table.Add(Affects.displace, AffectDisplace);
        affect_table.Add(Affects.breath_acid, _ovr023.DragonBreathAcid);
        affect_table.Add(Affects.affect_in_cloud_kill, CloudKillAffect);
        affect_table.Add(Affects.affect_5c, empty);
        affect_table.Add(Affects.affect_5d, half_fire_damage);
        affect_table.Add(Affects.affect_5e, sub_3BDB2);
        affect_table.Add(Affects.affect_5F, sub_3BE06);
        affect_table.Add(Affects.owlbear_hug_check, _ovr014.AffectOwlbearHugAttackCheck);
        affect_table.Add(Affects.con_saving_bonus, con_saving_bonus);
        affect_table.Add(Affects.regen_3_hp, AffectRegen3Hp);
        affect_table.Add(Affects.affect_63, sub_3BEE8);
        affect_table.Add(Affects.troll_fire_or_acid, AffectTrollFireOrAcid);
        affect_table.Add(Affects.troll_regen, AffectTrollRegenerate);
        affect_table.Add(Affects.TrollRegen, AffectTrollRegen);
        affect_table.Add(Affects.salamander_heat_damage, AffectSalamanderHeatDamage);
        affect_table.Add(Affects.thri_kreen_dodge_missile, sub_3C0DA);
        affect_table.Add(Affects.resist_magic_50_percent, ResistMagic50Percent);
        affect_table.Add(Affects.resist_magic_15_percent, ResistMagic15Percent);
        affect_table.Add(Affects.elf_resist_sleep, AffectElfRisistSleep);
        affect_table.Add(Affects.protect_charm_sleep, AffectProtCharmSleep);
        affect_table.Add(Affects.resist_paralyze, ResistParalyze);
        affect_table.Add(Affects.immune_to_cold, AffectImmuneToCold);
        affect_table.Add(Affects.affect_6f, sub_3C1C9);
        affect_table.Add(Affects.immune_to_fire, AffectImmuneToFire);
        affect_table.Add(Affects.affect_71, sub_3C201);
        affect_table.Add(Affects.affect_72, AffectProtectionFromElectricity);
        affect_table.Add(Affects.affect_73, sub_3C260);
        affect_table.Add(Affects.affect_74, half_damage_if_weap_magic);
        affect_table.Add(Affects.affect_75, sub_3C2F9);
        affect_table.Add(Affects.affect_76, AffectProtCold);
        affect_table.Add(Affects.affect_77, AffectProtNonMagicWeapons);
        affect_table.Add(Affects.affect_78, sub_3C3A2);
        affect_table.Add(Affects.affect_79, sub_3C3F6);
        affect_table.Add(Affects.dracolich_paralysis, AffectDracolichParalysis);
        affect_table.Add(Affects.affect_7b, sub_3C59D);
        affect_table.Add(Affects.halfelf_resistance, AffectHalfElfResistance);
        affect_table.Add(Affects.affect_7d, sub_3C5F4);
        affect_table.Add(Affects.affect_7e, _ovr023.cast_gaze_paralyze);
        affect_table.Add(Affects.affect_7f, empty);
        affect_table.Add(Affects.affect_80, _ovr023.DragonBreathFire);
        affect_table.Add(Affects.protect_magic, AffectProtMagic);
        affect_table.Add(Affects.affect_82, sub_3C643);
        affect_table.Add(Affects.cast_breath_fire, _ovr023.cast_breath_fire);
        affect_table.Add(Affects.cast_throw_lightening, _ovr023.cast_throw_lightening);
        affect_table.Add(Affects.affect_85, AffectDracolichA);
        affect_table.Add(Affects.ranger_vs_giant, AffectRangerVsGiant);
        affect_table.Add(Affects.protect_elec, AffectProtElec);
        affect_table.Add(Affects.entangle, AffectEntangle);
        affect_table.Add(Affects.affect_89, sub_3C7E0);
        affect_table.Add(Affects.affect_8a, add_affect_19);
        affect_table.Add(Affects.affect_8b, _ovr014.sub_425C6);
        affect_table.Add(Affects.paladinDailyHealCast, empty);
        affect_table.Add(Affects.paladinDailyCureRefresh, PaladinCastCureRefresh);
        affect_table.Add(Affects.fear, AffectFear);
        affect_table.Add(Affects.affect_8f, sub_3C975);
        affect_table.Add(Affects.owlbear_hug_round_attack, _ovr014.AffectOwlbearHugRoundAttack);
        affect_table.Add(Affects.sp_dispel_evil, sp_dispel_evil);
        affect_table.Add(Affects.strength_spell, empty);
        affect_table.Add(Affects.do_items_affect, do_items_affect);
    }

    internal void CallAffectTable(Effect add_remove, object parameter, Player player, Affects affect) /* sub_630C7 */
    {
        if (gbl.applyItemAffect == true)
        {
            affect = Affects.do_items_affect;
        }

        affectDelegate func;
        if (affect_table.TryGetValue(affect, out func))
        {
            func(add_remove, parameter, player);
        }
    }

    /// <summary>
    /// If same as current affect damage set to zero, or if affect is zero
    /// </summary>
    private void ProtectedIf(Affects affect) /* sub_3A019 */
    {
        if (gbl.current_affect == affect)
        {
            Protected();
        }
    }

    private void Protected()
    {
        gbl.damage = 0;
        gbl.current_affect = 0;
    }


    private bool addAffect(ushort time, int data, Affects affect_type, Player player)
    {
        if (gbl.cureSpell == true)
        {
            return false;
        }
        else
        {
            _ovr024.add_affect(true, data, time, affect_type, player);
            return true;
        }
    }


    private void sub_3A071(Effect arg_0, object param, Player player)
    {
        _ovr025.clear_actions(player);
    }


    private void Bless(Effect add_remove, object param, Player player)
    {
        gbl.monster_morale += 5;
        gbl.attack_roll++;
    }


    private void Curse(Effect arg_0, object param, Player player)
    {
        if (gbl.monster_morale < 5)
        {
            gbl.monster_morale = 0;
        }
        else
        {
            gbl.monster_morale -= 5;
        }

        gbl.attack_roll--;
    }


    private void SticksToSnakes(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        byte var_1 = (byte)(player.attack2_AttacksLeft + player.attack1_AttacksLeft);

        if (affect.affect_data > var_1)
        {
            affect.affect_data -= var_1;
        }
        else
        {
            _ovr024.remove_affect(null, Affects.sticks_to_snakes, player);
        }

        _ovr025.MagicAttackDisplay("is fighting with snakes", true, player);
        _ovr025.ClearPlayerTextArea();

        _ovr025.clear_actions(player);
    }


    private void DispelEvil(Effect arg_0, object param, Player player)
    {
        if ((gbl.SelectedPlayer.field_14B & 1) != 0)
        {
            gbl.attack_roll -= 7;
        }
    }


    private void BonusVsMonstersX(Effect arg_0, object param, Player player) // sub_3A17A
    {
        int bonus = 0;

        if (player.actions != null &&
            player.actions.target != null)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.monsterType == MonsterType.troll)
            {
                bonus = 1;
            }
            else if (gbl.spell_target.monsterType == MonsterType.type_9 || gbl.spell_target.monsterType == MonsterType.type_12)
            {
                bonus = 2;
            }
            else if (gbl.spell_target.monsterType == MonsterType.animated_dead)
            {
                bonus = 3;
            }
            else
            {
                bonus = 0;
            }
        }

        gbl.attack_roll += bonus;
        gbl.damage += bonus;
        gbl.damage_flags = DamageType.Magic | DamageType.Fire;
    }


    private void FaerieFire(Effect arg_0, object param, Player player)
    {
        if (player.ac < 0x3A)
        {
            player.ac += 2;
        }
        else
        {
            player.ac = 0x3C;
        }

        if (player.ac_behind < 0x3A)
        {
            player.ac_behind += 2;
        }
        else
        {
            player.ac_behind = 0x3C;
        }
    }


    private void affect_protect_evil(Effect arg_0, object param, Player player) /* sub_3A224 */
    {
        if (gbl.SelectedPlayer.alignment == 2 ||
            gbl.SelectedPlayer.alignment == 5 ||
            gbl.SelectedPlayer.alignment == 8)
        {
            gbl.savingThrowRoll += 2;
            gbl.attack_roll -= 2;
        }
    }


    private void affect_protect_good(Effect arg_0, object param, Player player) /* sub_3A259 */
    {
        if (gbl.SelectedPlayer.alignment == 0 ||
            gbl.SelectedPlayer.alignment == 3 ||
            gbl.SelectedPlayer.alignment == 6)
        {
            gbl.savingThrowRoll += 2;
            gbl.attack_roll -= 2;
        }
    }


    private void affect_resist_cold(Effect arg_0, object param, Player player) /* sub_3A28E */
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            gbl.damage /= 2;
            gbl.savingThrowRoll += 3;
        }
    }


    private void affect_charm_person(Effect arg_0, object param, Player player) /* sub_3A2AD */
    {
        Affect affect = (Affect)param;

        if (arg_0 == Effect.Remove)
        {
            player.combat_team = (CombatTeam)((affect.affect_data & 0x40) >> 6);

            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Base;
            }
        }
        else
        {
            if ((affect.affect_data & 0x20) == 0)
            {
                affect.affect_data += (byte)(0x20 + (((int)player.combat_team) << 6));

                player.combat_team = (CombatTeam)(affect.affect_data >> 7);
                player.quick_fight = QuickFight.True;

                if (player.control_morale < Control.NPC_Base)
                {
                    player.control_morale = Control.PC_Berzerk;
                }

                player.actions.target = null;
                _ovr025.CountCombatTeamMembers();
            }

            gbl.monster_morale = 100;
        }
    }


    private void Suffocates(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (affect.affect_data == 0)
        {
            _ovr024.KillPlayer("Suffocates", Status.dead, player);
        }
        else
        {
            affect.affect_data--;
        }
    }


    private void AffectPoisonDamage(Effect arg_0, object param, Player player) // sub_3A3BC
    {
        Affect affect = (Affect)param;

        if (addAffect(10, affect.affect_data, Affects.poison_damage, player) == true &&
            player.hit_point_current > 1)
        {
            gbl.damage_flags = 0;

            _ovr024.damage_person(false, 0, 1, player);

            if (gbl.game_state != GameState.Combat)
            {
                _ovr025.PartySummary(gbl.SelectedPlayer);
            }
        }
    }


    private void AffectShield(Effect arg_0, object param, Player player) /* sub_3A41F */
    {
        if (player.ac < 0x39) // AC 3
        {
            player.ac = 0x39; // AC 3
        }

        gbl.savingThrowRoll += 1;

        if (gbl.spell_id == 15) /* Magic Missle */
        {
            gbl.damage = 0;
        }
    }


    private void AffectGnomeVsManSizedGiant(Effect arg_0, object param, Player player) // sub_3A44A
    {
        if (player.actions != null &&
            player.actions.target != null &&
            (player.actions.target.field_14B & 2) != 0)
        {
            gbl.spell_target = player.actions.target;
            gbl.attack_roll++;
        }
    }


    private void AffectResistFire(Effect add_remove, object param, Player player) /* sub_3A480 */
    {
        if (add_remove == Effect.Add &&
            (gbl.damage_flags & DamageType.Fire) != 0)
        {
            gbl.damage /= 2;
            gbl.savingThrowRoll += 3;
        }
    }


    private void is_silenced1(Effect arg_0, object param, Player player)
    {
        if (player.actions.can_use == true)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is silenced", player);
        }

        player.actions.can_use = false;
        player.actions.can_cast = false;
    }


    private void AffectSlowPoison(Effect arg_0, object param, Player player) // sub_3A517
    {
        if (player.HasAffect(Affects.poisoned) == true)
        {
            _ovr024.KillPlayer("dies from poison", Status.dead, player);
        }

        gbl.cureSpell = true;

        _ovr024.remove_affect(null, Affects.poison_damage, player);

        gbl.cureSpell = false;
    }


    private void affect_spiritual_hammer(Effect add_remove, object param, Player player) /* sub_3A583 */
    {
        Item item = player.items.Find(i => i.type == ItemType.Hammer && i.namenum3 == 0xf3);
        bool item_found = item != null;

        if (add_remove == Effect.Remove && item != null)
        {
            _ovr025.lose_item(item, player);
        }

        if (add_remove == Effect.Add &&
            item_found == false &&
            player.items.Count < Player.MaxItems)
        {
            item = new Item(Affects.affect_78, Affects.spiritual_hammer, 0, 0, 0, 0, false, 0, false, 0, 1, 243, 20, 0, ItemType.Hammer, true);

            player.items.Add(item);
            _ovr020.ready_Item(item);

            _ovr025.DisplayPlayerStatusString(true, 10, "Gains an item", player);
        }

        _ovr025.reclac_player_values(player);
    }


    private void sub_3A6C6(Effect arg_0, object param, Player player)
    {
        if (player.name.Length == 0 &&
            gbl.SelectedPlayer.HasAffect(Affects.detect_invisibility) == false)
        {
            gbl.targetInvisible = true;
            gbl.attack_roll -= 4;
        }
    }


    private void AffectDwarfVsOrc(Effect arg_0, object param, Player player) // sub_3A7E8
    {
        gbl.spell_target = player.actions.target;

        if ((gbl.spell_target.field_14B & 4) != 0)
        {
            gbl.attack_roll++;
        }
    }


    private void MirrorImage(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (_ovr024.roll_dice((affect.affect_data >> 4) + 1, 1) > 1 &&
            gbl.spell_id > 0 &&
            gbl.byte_1D2C7 == false)
        {
            Protected();

            _ovr025.DisplayPlayerStatusString(true, 10, "lost an image", player);

            affect.affect_data -= 1;

            if (affect.affect_data == 0)
            {
                _ovr024.remove_affect(null, Affects.mirror_image, player);
            }
        }
    }


    private void three_quarters_damage(Effect arg_0, object param, Player player)
    {
        gbl.damage -= gbl.damage / 4;
    }


    private void StinkingCloud(Effect arg_0, object param, Player player)
    {
        if (player.actions.can_use == true)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "is coughing", player);
        }

        player.actions.can_use = false;
        player.actions.can_cast = false;

        _ovr025.reclac_player_values(player);

        if (player.ac_behind > 0x34)
        {
            player.ac_behind -= 2;
        }
        else
        {
            player.ac_behind = 0x32;
        }

        player.ac = player.ac_behind;

        if (player == gbl.SelectedPlayer)
        {
            _ovr025.CombatDisplayPlayerSummary(player);
        }
    }


    private void sub_3A89E(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        affect.callAffectTable = false;

        if (gbl.cureSpell == false)
        {
            _ovr024.KillPlayer("collapses", Status.dead, player);
        }

        player.combat_team = (CombatTeam)(affect.affect_data >> 4);
        player.quick_fight = QuickFight.True;
        player.field_E9 = 0;

        player.attackLevel = (byte)player.SkillLevel(SkillType.Fighter);
        player.base_movement = 0x0C;

        if (player.control_morale == Control.PC_Berzerk)
        {
            player.control_morale = Control.PC_Base;
        }

        player.monsterType = 0;
    }


    private void AffectBlinded(Effect arg_0, object param, Player player) // sub_3A951
    {
        gbl.attack_roll -= 4;

        player.ac -= 4;
        player.ac_behind -= 4;

        gbl.savingThrowRoll -= 4;
    }


    private void AffectCauseDisease(Effect add_remove, object param, Player player) // sub_3A974
    {
        CallAffectTable(add_remove, param, player, Affects.weaken);
        CallAffectTable(add_remove, param, player, Affects.cause_disease_2);
    }


    private void AffectConfuse(Effect arg_0, object arg_2, Player player) // sub_3A9D9
    {
        byte var_1 = _ovr024.roll_dice(100, 1);

        if (var_1 >= 1 && var_1 <= 10)
        {
            _ovr024.remove_affect(null, Affects.confuse, player);
            player.actions.fleeing = true;
            player.quick_fight = QuickFight.True;

            if (player.control_morale < Control.NPC_Base)
            {
                player.control_morale = Control.PC_Berzerk;
            }

            player.actions.target = null;

            _ovr024.ApplyAttackSpellAffect("runs away", false, DamageOnSave.Zero, true, 0, 10, Affects.fear, player);
        }
        else if (var_1 >= 11 && var_1 <= 60)
        {
            _ovr025.MagicAttackDisplay("is confused", true, player);
            _ovr025.ClearPlayerTextArea();
            sub_3A071(0, arg_2, player);
        }
        else if (var_1 >= 61 && var_1 <= 80)
        {
            _ovr024.ApplyAttackSpellAffect("goes berserk", false, DamageOnSave.Zero, true, (byte)player.combat_team, 1, Affects.affect_89, player);
            CallAffectTable(Effect.Add, null, player, Affects.affect_89);
        }
        else if (var_1 >= 81 && var_1 <= 100)
        {
            _ovr025.MagicAttackDisplay("is enraged", true, player);
            _ovr025.ClearPlayerTextArea();
        }

        if (_ovr024.RollSavingThrow(-2, SaveVerseType.Spell, player) == true)
        {
            _ovr024.remove_affect(null, Affects.confuse, player);
        }
    }


    private void affect_curse(Effect arg_0, object param, Player player) /* sub_3AB6F */
    {
        gbl.attack_roll -= 4;
        gbl.savingThrowRoll -= 4;
    }


    private void AffectBlink(Effect arg_0, object param, Player player) // has_action_timedout
    {
        if (player.actions.delay == 0)
        {
            gbl.targetInvisible = true;
            gbl.attack_roll = -1;
        }
    }


    private void AffectHaste(Effect arg_0, object param, Player player) // spl_age
    {
        Affect affect = (Affect)param;

        if ((affect.affect_data & 0x10) == 0)
        {
            affect.affect_data += 0x10;

            _ovr025.DisplayPlayerStatusString(true, 10, "ages", player);
            player.age++;
        }

        gbl.halfActionsLeft *= 2;
    }


    private void StinkingCloudAffect(Effect arg_0, object param, Player player) // sub_3AC1D
    {
        Affect affect = (Affect)param;

        var var_8 = gbl.StinkingCloud.Find(cell => cell.player == player && cell.field_1C == (affect.affect_data >> 4));

        if (var_8 != null)
        {
            _ovr025.string_print01("The air clears a little...");

            for (int var_B = 0; var_B < 4; var_B++)
            {
                if (var_8.present[var_B] == true)
                {
                    var tmp = var_8.targetPos + gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_B]];

                    bool var_9 = gbl.downedPlayers.Exists(cell => cell.target != null && cell.map == tmp);

                    if (var_9 == true)
                    {
                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_DownPlayer;
                    }
                    else
                    {
                        gbl.mapToBackGroundTile[tmp] = var_8.groundTile[var_B];
                    }
                }
            }

            gbl.StinkingCloud.Remove(var_8);

            foreach (var var_4 in gbl.StinkingCloud)
            {
                for (int var_B = 0; var_B < 4; var_B++)
                {
                    if (var_4.present[var_B] == true)
                    {
                        var tmp = gbl.MapDirectionDelta[gbl.SmallCloudDirections[var_B]] + var_4.targetPos;

                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_StinkingCloud;
                    }
                }
            }
        }
    }


    private void AvoidMissleAttack(int percentage, Player player) // sub_3AF06
    {
        if (gbl.SelectedPlayer.activeItems.primaryWeapon != null &&
            _ovr025.getTargetRange(player, gbl.SelectedPlayer) == 0 &&
            _ovr024.roll_dice(100, 1) <= percentage)
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "Avoids it", player);
            gbl.damage = 0;
            gbl.attack_roll = -1;
            gbl.bytes_1D2C9[1] -= 1;
        }
    }


    private Item get_primary_weapon(Player player) /* sub_3AF77 */
    {
        Item item = null;

        if (player.activeItems.primaryWeapon != null)
        {
            bool item_found = _ovr025.GetCurrentAttackItem(out item, player);

            if (item_found == false || item == null)
            {
                item = player.activeItems.primaryWeapon;
            }
        }

        return item;
    }


    private void AffectProtNormalMissles(Effect arg_0, object param, Player player) // sub_3AFE0
    {
        Item item = get_primary_weapon(gbl.SelectedPlayer);

        if (item != null && item.plus == 0)
        {
            AvoidMissleAttack(100, player);
        }
    }


    private void AffectSlow(Effect arg_0, object param, Player player) //sub_3B01B
    {
        gbl.halfActionsLeft /= 2;
    }


    private void weaken(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (addAffect(0x3c, affect.affect_data, Affects.weaken, player) == true)
        {
            if (player.stats2.Str.full > 3)
            {
                _ovr025.DisplayPlayerStatusString(true, 10, "is weakened", player);
                player.stats2.Str.full--;
            }
            else if (player.HasAffect(Affects.helpless) == true)
            {
                _ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
            }
        }
    }


    private void sub_3B0C2(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (addAffect(10, affect.affect_data, Affects.cause_disease_2, player) == true)
        {
            if (player.hit_point_current > 1)
            {
                gbl.damage_flags = 0;

                _ovr024.damage_person(false, 0, 1, player);

                if (gbl.game_state != GameState.Combat)
                {
                    _ovr025.PartySummary(gbl.SelectedPlayer);
                }
            }
            else if (player.HasAffect(Affects.helpless) == false)
            {
                _ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
            }
        }
    }


    private void AffectDwarfGnomeVsGiants(Effect arg_0, object param, Player player)
    {
        gbl.spell_target = player.actions.target;

        if (gbl.SelectedPlayer.monsterType == MonsterType.giant ||
            gbl.SelectedPlayer.monsterType == MonsterType.troll)
        {
            if ((gbl.SelectedPlayer.field_DE & 0x7F) == 2)
            {
                gbl.attack_roll -= 4;
            }
        }
    }


    private void sub_3B1A2(Effect arg_0, object param, Player player)
    {
        if (gbl.SelectedPlayer.monsterType == MonsterType.type_1 &&
            (gbl.SelectedPlayer.field_DE & 0x7F) == 2)
        {
            gbl.attack_roll -= 4;
        }
    }


    private void AffectPrayer(Effect arg_0, object param, Player player) // sub_3B1C9
    {
        Affect affect = (Affect)param;

        CombatTeam team = (CombatTeam)((affect.affect_data & 0x10) >> 4);

        if (player.combat_team == team)
        {
            gbl.savingThrowRoll += 1;
            gbl.attack_roll++;
        }
        else
        {
            gbl.attack_roll -= 1;
            gbl.savingThrowRoll -= 1;
        }
    }


    private void HotFireShield(Effect arg_0, object param, Player player) // sub_3B212
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            gbl.savingThrowRoll += 2;
        }
        else if ((gbl.damage_flags & DamageType.Fire) != 0 && gbl.savingThrowMade == false)
        {
            gbl.damage *= 2;
        }
    }


    private void ColdFireShield(Effect arg_0, object param, Player player) // sub_3B243
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            gbl.savingThrowRoll += 2;
        }
        else if ((gbl.damage_flags & DamageType.Cold) != 0 && gbl.savingThrowMade == false)
        {
            gbl.damage *= 2;
        }
    }


    private void sub_3B27B(Effect arg_0, object param, Player player) // sub_3B27B
    {
        _ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
    }


    private void AffectClearMovement(Effect arg_0, object param, Player player) //sub_3B29A
    {
        player.actions.move = 0;

        if (gbl.resetMovesLeft == true)
        {
            gbl.halfActionsLeft = 0;
        }
    }


    private void AffectRegenration(Effect arg_0, object param, Player player)
    {
        _ovr024.add_affect(false, 0xff, 0, Affects.regen_3_hp, player);
    }


    private void AffectResistWeapons(Effect arg_0, object param, Player player) // sub_3B2D8
    {
        Item weapon = get_primary_weapon(gbl.SelectedPlayer);

        if (weapon == null ||
            weapon.plus == 0)
        {
            gbl.damage = 0;
        }
        else if (weapon.plus < 3)
        {
            gbl.damage /= 2;
        }
    }


    private void AffectFireResist(Effect arg_0, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            for (int i = 1; i <= gbl.dice_count; i++)
            {
                gbl.damage -= 2;

                if (gbl.damage < gbl.dice_count)
                {
                    gbl.damage = gbl.dice_count;
                }
            }

            gbl.savingThrowRoll += 4;

            if ((gbl.damage_flags & DamageType.Magic) == 0)
            {
                Protected();
            }
        }
    }


    private void AffectHighConRegen(Effect arg_0, object param, Player player) /* sub_3B386 */
    {
        Affect affect = (Affect)param;

        if (addAffect(0x3C, affect.affect_data, Affects.highConRegen, player) == true &&
            _ovr024.heal_player(1, 1, player) == true)
        {
            _ovr025.DescribeHealing(player);
        }
    }


    private void AffectMinorGlobeOfInvulnerability(Effect arg_0, object param, Player player) /* sub_3B3CA */
    {
        if (gbl.spell_id > 0 &&
            gbl.spellCastingTable[gbl.spell_id].spellLevel < 4)
        {
            Protected();
        }
    }


    private void PoisonAttack(int save_bonus, Player player)
    {
        gbl.spell_target = player.actions.target;

        if (_ovr024.RollSavingThrow(save_bonus, SaveVerseType.Poison, gbl.spell_target) == false)
        {
            _ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", gbl.spell_target);
            _displayDriver.GameDelay();
            _ovr024.add_affect(false, 0xff, 0, Affects.poisoned, gbl.spell_target);

            _ovr024.KillPlayer("is killed", Status.dead, gbl.spell_target);
        }
    }


    private void AffectPoisonPlus0(Effect arg_0, object param, Player player) // sub_3B520
    {
        PoisonAttack(0, player);
    }


    private void AffectPoisonPlus4(Effect arg_0, object param, Player player) // sub_3B534
    {
        PoisonAttack(4, player);
    }


    private void AffectPoisonPlus2(Effect arg_0, object param, Player player) // sub_3B548
    {
        PoisonAttack(2, player);
    }


    private void ThriKreenParalyze(Effect arg_0, object param, Player player) // sub_3B55C
    {
        ushort time = _ovr024.roll_dice(8, 2);

        gbl.spell_target = player.actions.target;

        if (_ovr024.RollSavingThrow(0, SaveVerseType.Poison, gbl.spell_target) == false)
        {
            _ovr025.MagicAttackDisplay("is Paralyzed", true, gbl.spell_target);
            _ovr024.add_affect(false, 12, time, Affects.paralyze, gbl.spell_target);
        }
    }


    private void AffectFeebleMind(Effect arg_0, object param, Player player) // spell_stupid
    {
        player.stats2.Int.full = 7;
        player.stats2.Wis.full = 7;

        _ovr025.DisplayPlayerStatusString(true, 10, "is stupid", player);

        if (gbl.game_state == GameState.Combat)
        {
            _ovr024.TryLooseSpell(player);
        }
    }


    private void AffectInvisToAnimals(Effect arg_0, object param, Player player) // sub_3B636
    {
        if (gbl.SelectedPlayer.monsterType == MonsterType.animal)
        {
            if (gbl.SelectedPlayer.HasAffect(Affects.detect_invisibility) == false)
            {
                gbl.targetInvisible = true;
            }

            gbl.attack_roll -= 4;
        }
    }


    private void AffectPoisonNeg2(Effect arg_0, object param, Player player) // sub_3B671
    {
        PoisonAttack(-2, player);
    }


    private void AffectInvisible(Effect arg_0, object param, Player player) // sub_3B685
    {
        gbl.targetInvisible = true;
        gbl.attack_roll -= 4;
    }


    private void AffectCamouflage(Effect arg_0, object param, Player player) // sub_3B696
    {
        if (_ovr024.roll_dice(100, 1) <= 95)
        {
            _ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
        }
    }


    private void ProtDragonsBreath(Effect arg_0, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.DragonBreath) > 0)
        {
            Protected();
            _ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", player);
        }
    }


    private void AffectDragonSlayer(Effect arg_0, object param, Player player) // sub_3B71A
    {
        if (player.actions != null &&
            player.actions.target != null)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.monsterType == MonsterType.dragon)
            {
                gbl.damage = (_ovr024.roll_dice(12, 1) * 3) + 4 + _ovr025.strengthDamBonus(player);
                gbl.attack_roll += 2;
            }
        }
    }


    private void AffectFrostBrand(Effect arg_0, object param, Player player) // sub_3B772
    {
        if (player.actions != null)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target != null &&
                gbl.spell_target.monsterType == MonsterType.fire)
            {
                gbl.attack_roll += 3;
                gbl.damage += 3;
            }
        }
    }


    private void AffectBerzerk(Effect arg_0, object param, Player player)
    {
        if (arg_0 == Effect.Add)
        {
            player.quick_fight = QuickFight.True;

            if (player.control_morale < Control.NPC_Base ||
                player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Berzerk;
            }
            else
            {
                player.control_morale = Control.NPC_Berzerk;
            }

            if (gbl.game_state == GameState.Combat)
            {
                player.actions.target = null;

                var scl = _ovr032.Rebuild_SortedCombatantList(player, 0xff, p => true);

                player.actions.target = scl[0].player;

                player.actions.can_cast = false;
                player.combat_team = player.actions.target.OppositeTeam();

                _ovr025.DisplayPlayerStatusString(true, 10, "goes berzerk", player);
            }
        }
        else
        {
            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Base;
            }

            player.combat_team = CombatTeam.Ours;
        }
    }


    private void sub_3B8D9(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        if (_ovr024.combat_heal(player.hit_point_current, player) == false)
        {
            addAffect(1, affect.affect_data, Affects.affect_4e, player);
        }
    }


    private void MagicFireAttack_2d10(Effect arg_0, object param, Player player) // sub_3B919
    {
        gbl.damage_flags = DamageType.Magic | DamageType.Fire;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(10, 2), player.actions.target);
    }


    private void AnkhegAcidAttack(Effect arg_0, object param, Player player) // sub_3B94C
    {
        gbl.damage_flags = DamageType.Acid;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(4, 1), player.actions.target);
    }


    private void half_damage(Effect arg_0, object param, Player player) /* sub_3B97F */
    {
        gbl.damage /= 2;
    }


    private void AffectResistFireAndCold(Effect arg_0, object param, Player player) // sub_3B990
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0 ||
            (gbl.damage_flags & DamageType.Cold) != 0)
        {
            if (_ovr024.RollSavingThrow(0, SaveVerseType.Spell, player) == true &&
                gbl.spell_id > 0 &&
                gbl.spellCastingTable[gbl.spell_id].damageOnSave != 0)
            {
                gbl.damage = 0;
            }
            else
            {
                gbl.damage /= 2;
            }
        }
    }


    private void AffectShamblerAbsorbLightning(Effect arg_0, object param, Player player) // sub_3B9E1
    {
        // Shambling Mounds absorb lighting and get more powerful.

        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            Protected();
            //byte var_1 = ovr024.roll_dice(8, 1);

            player.ac += 8;
        }
    }


    private void sub_3BA14(Effect arg_0, object param, Player player)
    {
        Item item = get_primary_weapon(gbl.SelectedPlayer);

        if (item != null &&
            gbl.ItemDataTable[item.type].field_7 == 1)
        {
            gbl.damage = 1;
        }
    }


    private void AffectDisplace(Effect arg_0, object param, Player player) /*sub_3BA55*/
    {
        Affect affect = (Affect)param;

        if (affect != null)
        {
            if (gbl.combat_round == 0 && gbl.attack_roll == 0)
            {
                affect.affect_data &= 0x0f;
            }
            else if ((affect.affect_data & 0x10) == 0)
            {
                gbl.attack_roll = -1;
                affect.affect_data |= 0x10;
            }
        }
    }


    private void CloudKillAffect(Effect arg_0, object param, Player player) // sub_3BAB9
    {
        Affect affect = (Affect)param;

        GasCloud cell = gbl.CloudKillCloud.Find(c => c.player == player && c.field_1C == (affect.affect_data >> 4));

        if (cell != null)
        {
            _ovr025.string_print01("The air clears a little...");

            for (int var_B = 0; var_B < 9; var_B++)
            {
                if (cell.present[var_B] == true)
                {
                    var tmp = cell.targetPos + gbl.MapDirectionDelta[gbl.CloudDirections[var_B]];

                    bool var_E = gbl.downedPlayers.Exists(c => c.target != null && c.map == tmp);

                    if (var_E == true)
                    {
                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_DownPlayer;
                    }
                    else
                    {
                        gbl.mapToBackGroundTile[tmp] = cell.groundTile[var_B];
                    }
                }
            }


            gbl.CloudKillCloud.Remove(cell);

            foreach (var var_4 in gbl.CloudKillCloud)
            {
                for (int var_B = 0; var_B < 9; var_B++)
                {
                    if (var_4.present[var_B] == true)
                    {
                        var tmp = var_4.targetPos + gbl.MapDirectionDelta[gbl.CloudDirections[var_B]];

                        gbl.mapToBackGroundTile[tmp] = gbl.Tile_CloudKill;
                    }
                }
            }
        }
    }


    private void half_fire_damage(Effect arg_0, object param, Player arg_6) // sub_3BD98
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            gbl.damage /= 2;
        }
    }


    private void sub_3BDB2(Effect arg_0, object param, Player arg_6)
    {
        Item item = get_primary_weapon(gbl.SelectedPlayer);

        if (item != null &&
            (gbl.ItemDataTable[item.type].field_7 & 0x81) != 0)
        {
            gbl.damage /= 2;
        }
    }


    private void sub_3BE06(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;
        affect.callAffectTable = false;

        if (player.in_combat == true)
        {
            _ovr024.KillPlayer("Falls dead", Status.dead, player);
        }
    }


    private void con_saving_bonus(Effect arg_0, object param, Player player) /* sub_3BE42 */
    {
        if (gbl.saveVerseType == SaveVerseType.Spell ||
            gbl.saveVerseType == SaveVerseType.RodStaffWand)
        {
            int save_bonus = 0;

            if (player.stats2.Con.full >= 4 && player.stats2.Con.full <= 6)
            {
                save_bonus = 1;
            }
            else if (player.stats2.Con.full >= 7 && player.stats2.Con.full <= 10)
            {
                save_bonus = 2;
            }
            else if (player.stats2.Con.full >= 11 && player.stats2.Con.full <= 13)
            {
                save_bonus = 3;
            }
            else if (player.stats2.Con.full >= 14 && player.stats2.Con.full <= 17)
            {
                save_bonus = 4;
            }
            else if (player.stats2.Con.full >= 18 && player.stats2.Con.full <= 20)
            {
                save_bonus = 5;
            }

            gbl.savingThrowRoll += save_bonus;
        }
    }


    private void AffectRegen3Hp(Effect arg_0, object param, Player player) // sub_3BEB8
    {
        player.hit_point_current += 3;

        if (player.hit_point_current > player.hit_point_max)
        {
            player.hit_point_current = player.hit_point_max;
        }
    }


    private void sub_3BEE8(Effect arg_0, object param, Player player)
    {
        Affect arg_2 = (Affect)param;

        byte heal_amount = 0;

        if (player.health_status == Status.dying &&
            player.actions.bleeding < 6)
        {
            heal_amount = (byte)(6 - player.actions.bleeding);
        }

        if (player.health_status == Status.unconscious)
        {
            heal_amount = 6;
        }

        if (heal_amount > 0 &&
            _ovr024.combat_heal(heal_amount, player) == true)
        {
            _ovr024.add_affect(true, 0xff, (ushort)(_ovr024.roll_dice(4, 1) + 1), Affects.affect_5F, player);
            arg_2.callAffectTable = false;
            _ovr024.remove_affect(arg_2, Affects.affect_63, player);
        }
    }


    private void AffectTrollFireOrAcid(Effect arg_0, object param, Player player)
    {
        if ((gbl.damage_flags & DamageType.Fire) == 0 &&
            (gbl.damage_flags & DamageType.Acid) == 0)
        {
            _ovr024.add_affect(true, 0xff, _ovr024.roll_dice(6, 3), Affects.TrollRegen, player);
        }
    }


    private void AffectTrollRegenerate(Effect arg_0, object param, Player player) // sp_regenerate
    {
        if (player.HasAffect(Affects.regen_3_hp) == false &&
            player.HasAffect(Affects.regenerate) == false)
        {
            _ovr024.add_affect(true, 0xff, 3, Affects.regenerate, player);
        }
    }


    private void AffectTrollRegen(Effect arg_0, object param, Player player) // sub_3C01E
    {
        Affect affect = (Affect)param;

        if (_ovr024.combat_heal(player.hit_point_max, player) == false)
        {
            addAffect(1, affect.affect_data, Affects.TrollRegen, player);
        }
    }


    private void AffectSalamanderHeatDamage(Effect arg_0, object param, Player player) // sub_3C05D
    {
        gbl.spell_target = player.actions.target;

        if (gbl.spell_target.HasAffect(Affects.resist_fire) == false &&
            gbl.spell_target.HasAffect(Affects.cold_fire_shield) == false &&
            gbl.spell_target.HasAffect(Affects.fire_resist) == false)
        {
            gbl.damage += _ovr024.roll_dice(6, 1);
        }
    }


    private void sub_3C0DA(Effect arg_0, object param, Player player)
    {
        AvoidMissleAttack(60, player);
    }


    private void ResistMagicPercent(int rollBase) // sub_3C0EE
    {
        int target_count = _ovr025.spellMaxTargetCount(gbl.spell_id);
        int rollNeeded = rollBase + ((11 - target_count) * 5);

        if (gbl.current_affect != 0 || (gbl.damage_flags & DamageType.Magic) != 0)
        {
            if (_ovr024.roll_dice(100, 1) <= rollNeeded)
            {
                Protected();
            }
        }
    }


    private void ResistMagic50Percent(Effect arg_0, object param, Player arg_6) // sub_3C14F
    {
        ResistMagicPercent(50);
    }


    private void ResistMagic15Percent(Effect arg_0, object param, Player arg_6) // sub_3C15D
    {
        ResistMagicPercent(15);
    }


    private void AffectElfRisistSleep(Effect arg_0, object param, Player arg_6) // sub_3C16B
    {
        if (_ovr024.roll_dice(100, 1) <= 90)
        {
            ProtectedIf(Affects.sleep);
            ProtectedIf(Affects.charm_person);
        }
    }


    private void AffectProtCharmSleep(Effect arg_0, object param, Player arg_6) // sub_3C18F
    {
        ProtectedIf(Affects.charm_person);
        ProtectedIf(Affects.sleep);
    }


    private void ResistParalyze(Effect arg_0, object param, Player arg_6) // sub_3C1A4
    {
        ProtectedIf(Affects.paralyze);
    }


    private void AffectImmuneToCold(Effect arg_0, object param, Player arg_6) // sub_3C1B2
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            Protected();
        }
    }


    private void sub_3C1C9(Effect arg_0, object param, Player arg_6)
    {
        ProtectedIf(Affects.poisoned);
        ProtectedIf(Affects.paralyze);

        if (gbl.saveVerseType == SaveVerseType.Poison)
        {
            gbl.savingThrowRoll = 100;
        }
    }


    private void AffectImmuneToFire(Effect arg_0, object param, Player arg_6) // sub_3C1EA
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            Protected();
        }
    }


    private void sub_3C201(Effect arg_0, object param, Player arg_6)
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            for (int i = 1; i <= gbl.dice_count; i++)
            {
                gbl.damage--;

                if (gbl.damage < gbl.dice_count)
                {
                    gbl.damage = gbl.dice_count;
                }
            }
        }
    }


    private void AffectProtectionFromElectricity(Effect arg_0, object param, Player player) // sub_3C246
    {
        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            gbl.damage /= 2;
        }
    }


    private void sub_3C260(Effect arg_0, object param, Player player)
    {
        Item weapon = get_primary_weapon(gbl.SelectedPlayer);

        if (weapon != null)
        {
            if (gbl.ItemDataTable[weapon.type].field_7 == 0 ||
                (gbl.ItemDataTable[weapon.type].field_7 & 1) != 0)
            {
                gbl.damage /= 2;
            }
        }
    }


    private void half_damage_if_weap_magic(Effect arg_0, object param, Player player) /* sub_3C2BF */
    {
        Item weapon = get_primary_weapon(gbl.SelectedPlayer);

        if (weapon != null &&
            weapon.plus > 0)
        {
            gbl.damage /= 2;
        }
    }


    private void sub_3C2F9(Effect arg_0, object param, Player player) // sub_3C2F9
    {
        Item item = gbl.SelectedPlayer.activeItems.primaryWeapon;

        if (item != null && item.type == ItemType.Type_85)
        {
            gbl.damage = _ovr024.roll_dice_save(6, 1) + 1;
        }
    }


    private void AffectProtCold(Effect arg_0, object param, Player player) // sub_3C33C
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            gbl.damage /= 2;
        }
    }


    private void AffectProtNonMagicWeapons(Effect arg_0, object param, Player player) // sub_3C356
    {
        Item weapon = get_primary_weapon(gbl.SelectedPlayer);

        if ((weapon == null || weapon.plus == 0) &&
            (gbl.SelectedPlayer.race > 0 || gbl.SelectedPlayer.HitDice < 4))
        {
            gbl.damage = 0;
        }
    }


    private void sub_3C3A2(Effect arg_0, object param, Player player)
    {
        Item field_151 = player.activeItems.primaryWeapon;

        if (field_151 != null)
        {
            if (field_151.type == ItemType.Type_87 || field_151.type == ItemType.Type_88)
            {
                AvoidMissleAttack(50, player);
            }
        }
    }


    private void sub_3C3F6(Effect arg_0, object param, Player player)
    {
        Affect affect = (Affect)param;

        gbl.spell_target = player.actions.target;

        if (_ovr024.roll_dice(100, 1) <= 25)
        {
            if (_ovr025.getTargetRange(gbl.spell_target, player) < 4)
            {
                _ovr025.clear_actions(player);

                _ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);

                _ovr025.load_missile_icons(0x17);

                _ovr025.draw_missile_attack(0x1e, 1, _ovr033.PlayerMapPos(gbl.spell_target), _ovr033.PlayerMapPos(player));

                int damage = _ovr024.roll_dice_save(4, 8);
                bool saved = _ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, gbl.spell_target);

                _ovr024.damage_person(saved, DamageOnSave.Half, damage, gbl.spell_target);

                _ovr024.remove_affect(affect, Affects.affect_79, player);
                _ovr024.remove_affect(null, Affects.ankheg_acid_attack, player);
            }
        }
    }


    private void AffectDracolichParalysis(Effect arg_0, object param, Player player) // spl_paralyze
    {
        gbl.spell_target = player.actions.target;

        if (_ovr024.RollSavingThrow(0, 0, gbl.spell_target) == false)
        {
            _ovr024.add_affect(false, 0xff, 0, Affects.paralyze, gbl.spell_target);

            _ovr025.DisplayPlayerStatusString(true, 10, "is paralyzed", gbl.spell_target);
        }
    }


    private void sub_3C59D(Effect arg_0, object param, Player player)
    {
        gbl.damage_flags = DamageType.Acid;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(8, 2), player.actions.target);
    }


    private void AffectHalfElfResistance(Effect arg_0, object param, Player player) // sub_3C5D0
    {
        if (_ovr024.roll_dice(100, 1) <= 30)
        {
            ProtectedIf(Affects.charm_person);
            ProtectedIf(Affects.sleep);
        }
    }


    private void sub_3C5F4(Effect arg_0, object param, Player player)
    {
        ProtectedIf(Affects.charm_person);
        ProtectedIf(Affects.sleep);
        ProtectedIf(Affects.paralyze);
        ProtectedIf(Affects.poisoned);

        if (gbl.saveVerseType != SaveVerseType.Poison)
        {
            gbl.savingThrowRoll = 100;
        }
    }


    private void AffectProtMagic(Effect arg_0, object param, Player player) // sub_3C623
    {
        if (gbl.current_affect != 0 ||
            (gbl.damage_flags & DamageType.Magic) != 0)
        {
            Protected();
        }
    }


    private void sub_3C643(Effect arg_0, object arg_2, Player player)
    {
        Item item;

        if (_ovr025.GetCurrentAttackItem(out item, gbl.SelectedPlayer) == true &&
            item != null &&
            item.type == ItemType.Quarrel &&
            item.namenum3 == 0x87)
        {
            player.health_status = Status.gone;
            player.in_combat = false;
            player.hit_point_current = 0;
            _ovr024.RemoveCombatAffects(player);
            _ovr024.CheckAffectsEffect(player, CheckType.Death);

            if (player.in_combat == true)
            {
                _ovr033.CombatantKilled(player);
            }
        }
    }


    private void do_items_affect(Effect remove_affect, object param, Player player) /* sub_3C6D3 */
    {
        Item item = (Item)param;

        gbl.applyItemAffect = false;

        if (remove_affect == Effect.Remove)
        {
            _ovr024.remove_affect(null, item.affect_2, player);
        }
        else
        {
            _ovr024.add_affect(true, 0xff, 0, item.affect_2, player);

            if (gbl.game_state != GameState.Combat)
            {
                CallAffectTable(Effect.Add, null, player, item.affect_2);
            }
        }
    }


    private void AffectDracolichA(Effect arg_0, object param, Player player) //sub_3C750
    {
        ProtectedIf(Affects.fear);
        ProtectedIf(Affects.ray_of_enfeeblement);
        ProtectedIf(Affects.feeblemind);

        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            Protected();
        }
    }


    private void AffectRangerVsGiant(Effect arg_0, object param, Player player) // sub_3C77C
    {
        gbl.spell_target = player.actions.target;

        if ((gbl.spell_target.field_14B & 8) != 0) // giant
        {
            gbl.damage += player.ranger_lvl;
        }
    }


    private void AffectProtElec(Effect arg_0, object param, Player player) //sub_3C7B5
    {
        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            Protected();
        }
    }


    private void AffectEntangle(Effect arg_0, object param, Player player) // sub_3C7CC
    {
        player.actions.move = 0;
    }


    private void sub_3C7E0(Effect arg_0, object param, Player player) // sub_3C7E0
    {
        Affect affect = (Affect)param;

        if (arg_0 == Effect.Add)
        {
            player.quick_fight = QuickFight.True;

            if (player.control_morale < Control.NPC_Base ||
                player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Berzerk;
            }
            else
            {
                player.control_morale = Control.NPC_Berzerk;
            }

            player.actions.target = null;

            var scl = _ovr032.Rebuild_SortedCombatantList(player, 0xff, p => true);

            player.actions.target = scl[0].player;
            player.combat_team = player.actions.target.OppositeTeam();
        }
        else
        {
            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = 0;
            }

            player.combat_team = (CombatTeam)affect.affect_data;
        }
    }


    private void add_affect_19(Effect arg_0, object param, Player player)
    {
        _ovr024.add_affect(false, 0xff, 0xff, Affects.invisibility, player);
    }


    private void PaladinCastCureRefresh(Effect add_remove, object param, Player player) // sub_3C8EF
    {
        if (add_remove == Effect.Remove)
        {
            player.paladinCuresLeft = (byte)(((player.SkillLevel(SkillType.Paladin) - 1) / 5) + 1);
        }
    }


    private void AffectFear(Effect add_remove, object param, Player player) /* sub_3C932 */
    {
        if (add_remove == Effect.Remove)
        {
            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Base;
                player.quick_fight = QuickFight.False;
            }

            player.actions.fleeing = false;
        }
    }


    private void sub_3C975(Effect arg_0, object arg_2, Player target)
    {
        if (_ovr025.getTargetRange(target, gbl.SelectedPlayer) < 2)
        {
            int bkup_damage = gbl.damage;
            DamageType bkup_damage_flags = gbl.damage_flags;

            gbl.damage *= 2;
            gbl.damage_flags = DamageType.Magic;

            _ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.SelectedPlayer);

            _ovr024.damage_person(false, 0, gbl.damage, gbl.SelectedPlayer);
            gbl.damage = bkup_damage;
            gbl.damage_flags = bkup_damage_flags;
        }
    }


    private void sp_dispel_evil(Effect arg_0, object param, Player player)
    {
        gbl.spell_target = player.actions.target;

        if ((gbl.spell_target.field_14B & 1) != 0 &&
            _ovr024.RollSavingThrow(0, SaveVerseType.Spell, gbl.spell_target) == false)
        {
            _ovr024.KillPlayer("is dispelled", Status.gone, gbl.spell_target);

            _ovr024.remove_affect(null, Affects.dispel_evil, gbl.SelectedPlayer);
            _ovr024.remove_affect(null, Affects.sp_dispel_evil, gbl.SelectedPlayer);
        }
        else
        {
            _ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.spell_target);
        }
    }

    private void empty(Effect arg_0, object param, Player player) { }

    private System.Collections.Generic.Dictionary<Affects, affectDelegate> affect_table;
}
