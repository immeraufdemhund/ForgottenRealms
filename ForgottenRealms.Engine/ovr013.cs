using System;
using System.Collections.Generic;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ovr013
{
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr032 _ovr032;
    private readonly DisplayDriver _displayDriver;
    private readonly PlayerPrimaryWeapon _playerPrimaryWeapon;
    private readonly AffectsProtectedAction _affectsProtectedAction;
    private readonly AvoidMissleAttackAction _avoidMissleAttackAction;
    private readonly ApplyAffectTable _applyAffectTable;
    private Dictionary<Affects, affectDelegate> affect_table;

    public ovr013(ovr024 ovr024, ovr025 ovr025, ovr032 ovr032, DisplayDriver displayDriver, PlayerPrimaryWeapon playerPrimaryWeapon,
        AffectsProtectedAction affectsProtectedAction, AvoidMissleAttackAction avoidMissleAttackAction, ApplyAffectTable applyAffectTable)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr032 = ovr032;
        _displayDriver = displayDriver;
        _playerPrimaryWeapon = playerPrimaryWeapon;
        _affectsProtectedAction = affectsProtectedAction;
        _avoidMissleAttackAction = avoidMissleAttackAction;
        _applyAffectTable = applyAffectTable;
    }

    [Obsolete("already setup with dependency injection", true)]
    internal void SetupAffectTables() // setup_spells2
    {
        affect_table = new System.Collections.Generic.Dictionary<Affects, affectDelegate>();

        affect_table.Add(Affects.berserk, AffectBerzerk);
        affect_table.Add(Affects.bestow_curse, affect_curse);
        affect_table.Add(Affects.blinded, AffectBlinded);
        affect_table.Add(Affects.blink, AffectBlink);
        affect_table.Add(Affects.camouflage, AffectCamouflage);
        affect_table.Add(Affects.cause_disease_1, AffectCauseDisease);
        affect_table.Add(Affects.cause_disease_2, sub_3B0C2);
        affect_table.Add(Affects.charm_person, affect_charm_person);
        affect_table.Add(Affects.clear_movement, AffectClearMovement);
        affect_table.Add(Affects.cold_fire_shield, ColdFireShield);
        affect_table.Add(Affects.confuse, AffectConfuse);
        affect_table.Add(Affects.con_saving_bonus, con_saving_bonus);
        affect_table.Add(Affects.displace, AffectDisplace);
        affect_table.Add(Affects.do_items_affect, do_items_affect);
        affect_table.Add(Affects.dracolich_paralysis, AffectDracolichParalysis);
        affect_table.Add(Affects.dwarf_and_gnome_vs_giants, AffectDwarfGnomeVsGiants);
        affect_table.Add(Affects.dwarf_vs_orc, AffectDwarfVsOrc);
        affect_table.Add(Affects.dwarf_and_gnome_vs_giants, AffectDwarfGnomeVsGiants);
        affect_table.Add(Affects.displace, AffectDisplace);
        affect_table.Add(Affects.dracolich_paralysis, AffectDracolichParalysis);
        affect_table.Add(Affects.do_items_affect, do_items_affect);
        affect_table.Add(Affects.elf_resist_sleep, AffectElfRisistSleep);
        affect_table.Add(Affects.entangle, AffectEntangle);
        affect_table.Add(Affects.faerie_fire, FaerieFire);
        affect_table.Add(Affects.fear, AffectFear);
        affect_table.Add(Affects.feeblemind, AffectFeebleMind);
        affect_table.Add(Affects.fireAttack_2d10, MagicFireAttack_2d10);
        affect_table.Add(Affects.fire_resist, AffectFireResist);
        affect_table.Add(Affects.fumbling, sub_3A071);
        affect_table.Add(Affects.gnome_vs_man_sized_giant, AffectGnomeVsManSizedGiant);
        affect_table.Add(Affects.haste, AffectHaste);
        affect_table.Add(Affects.helpless, sub_3A071);
        affect_table.Add(Affects.hot_fire_shield, HotFireShield);
        affect_table.Add(Affects.highConRegen, AffectHighConRegen);
        affect_table.Add(Affects.half_damage, half_damage);
        affect_table.Add(Affects.halfelf_resistance, AffectHalfElfResistance);
        affect_table.Add(Affects.invisibility, sub_3A6C6);
        affect_table.Add(Affects.mirror_image, MirrorImage);
        affect_table.Add(Affects.poison_damage, AffectPoisonDamage);
        affect_table.Add(Affects.protection_from_evil, affect_protect_evil);
        affect_table.Add(Affects.protection_from_good, affect_protect_good);
        affect_table.Add(Affects.ray_of_enfeeblement, three_quarters_damage);
        affect_table.Add(Affects.reduce, Suffocates);
        affect_table.Add(Affects.resist_cold, affect_resist_cold);
        affect_table.Add(Affects.resist_fire, AffectResistFire);
        affect_table.Add(Affects.shield, AffectShield);
        affect_table.Add(Affects.silence_15_radius, is_silenced1);
        affect_table.Add(Affects.slow_poison, AffectSlowPoison);
        affect_table.Add(Affects.stinking_cloud, StinkingCloud);
        affect_table.Add(Affects.prot_from_normal_missiles, AffectProtNormalMissles);
        affect_table.Add(Affects.slow, AffectSlow);
        affect_table.Add(Affects.weaken, weaken);
        affect_table.Add(Affects.prot_from_evil_10_radius, affect_protect_evil);
        affect_table.Add(Affects.prot_from_good_10_radius, affect_protect_good);
        affect_table.Add(Affects.prayer, AffectPrayer);
        affect_table.Add(Affects.snake_charm, sub_3A071);
        affect_table.Add(Affects.paralyze, sub_3A071);
        affect_table.Add(Affects.sleep, sub_3A071);
        affect_table.Add(Affects.item_invisibility, sub_3B27B);
        affect_table.Add(Affects.regenerate, AffectRegenration);
        affect_table.Add(Affects.resist_normal_weapons, AffectResistWeapons);
        affect_table.Add(Affects.minor_globe_of_invulnerability, AffectMinorGlobeOfInvulnerability);
        affect_table.Add(Affects.poison_plus_0, AffectPoisonPlus0);
        affect_table.Add(Affects.poison_plus_4, AffectPoisonPlus4);
        affect_table.Add(Affects.poison_plus_2, AffectPoisonPlus2);
        affect_table.Add(Affects.thri_kreen_paralyze, ThriKreenParalyze);
        affect_table.Add(Affects.invisible_to_animals, AffectInvisToAnimals);
        affect_table.Add(Affects.poison_neg_2, AffectPoisonNeg2);
        affect_table.Add(Affects.invisible, AffectInvisible);
        affect_table.Add(Affects.prot_drag_breath, ProtDragonsBreath);
        affect_table.Add(Affects.weap_dragon_slayer, AffectDragonSlayer);
        affect_table.Add(Affects.weap_frost_brand, AffectFrostBrand);
        affect_table.Add(Affects.resist_fire_and_cold, AffectResistFireAndCold);
        affect_table.Add(Affects.shambling_absorb_lightning, AffectShamblerAbsorbLightning);
        affect_table.Add(Affects.regen_3_hp, AffectRegen3Hp);
        affect_table.Add(Affects.troll_fire_or_acid, AffectTrollFireOrAcid);
        affect_table.Add(Affects.troll_regenerate, AffectTrollRegenerate);
        affect_table.Add(Affects.TrollRegen, AffectTrollRegen);
        affect_table.Add(Affects.salamander_heat_damage, AffectSalamanderHeatDamage);
        affect_table.Add(Affects.thri_kreen_dodge_missile, sub_3C0DA);
        affect_table.Add(Affects.resist_magic_50_percent, ResistMagic50Percent);
        affect_table.Add(Affects.resist_magic_15_percent, ResistMagic15Percent);
        affect_table.Add(Affects.protect_charm_sleep, AffectProtCharmSleep);
        affect_table.Add(Affects.resist_paralyze, ResistParalyze);
        affect_table.Add(Affects.immune_to_cold, AffectImmuneToCold);
        affect_table.Add(Affects.protect_magic, AffectProtMagic);
        affect_table.Add(Affects.immune_to_fire, AffectImmuneToFire);
        affect_table.Add(Affects.ranger_vs_giant, AffectRangerVsGiant);
        affect_table.Add(Affects.protect_elec, AffectProtElec);
        affect_table.Add(Affects.paladinDailyCureRefresh, PaladinCastCureRefresh);
        affect_table.Add(Affects.sp_dispel_evil, sp_dispel_evil);
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
            _affectsProtectedAction.Protected();

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

    private void AffectBlinded(Effect arg_0, object param, Player player) // sub_3A951
    {
        gbl.attack_roll -= 4;

        player.ac -= 4;
        player.ac_behind -= 4;

        gbl.savingThrowRoll -= 4;
    }

    private void AffectCauseDisease(Effect add_remove, object param, Player player) // sub_3A974
    {
        _applyAffectTable.CallAffectTable(add_remove, param, player, Affects.weaken);
        _applyAffectTable.CallAffectTable(add_remove, param, player, Affects.cause_disease_2);
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
            _applyAffectTable.CallAffectTable(Effect.Add, null, player, Affects.affect_89);
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

    private void AffectProtNormalMissles(Effect arg_0, object param, Player player) // sub_3AFE0
    {
        Item item = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

        if (item != null && item.plus == 0)
        {
            _avoidMissleAttackAction.AvoidMissleAttack(100, player);
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
        Item weapon = _playerPrimaryWeapon.get_primary_weapon(gbl.SelectedPlayer);

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
                _affectsProtectedAction.Protected();
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
            _affectsProtectedAction.Protected();
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
            _affectsProtectedAction.Protected();
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

    private void MagicFireAttack_2d10(Effect arg_0, object param, Player player) // sub_3B919
    {
        gbl.damage_flags = DamageType.Magic | DamageType.Fire;

        _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(10, 2), player.actions.target);
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
            _affectsProtectedAction.Protected();
            //byte var_1 = ovr024.roll_dice(8, 1);

            player.ac += 8;
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
        _avoidMissleAttackAction.AvoidMissleAttack(60, player);
    }

    private void ResistMagicPercent(int rollBase) // sub_3C0EE
    {
        int target_count = _ovr025.spellMaxTargetCount(gbl.spell_id);
        int rollNeeded = rollBase + ((11 - target_count) * 5);

        if (gbl.current_affect != 0 || (gbl.damage_flags & DamageType.Magic) != 0)
        {
            if (_ovr024.roll_dice(100, 1) <= rollNeeded)
            {
                _affectsProtectedAction.Protected();
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
            _affectsProtectedAction.ProtectedIf(Affects.sleep);
            _affectsProtectedAction.ProtectedIf(Affects.charm_person);
        }
    }

    private void AffectProtCharmSleep(Effect arg_0, object param, Player arg_6) // sub_3C18F
    {
        _affectsProtectedAction.ProtectedIf(Affects.charm_person);
        _affectsProtectedAction.ProtectedIf(Affects.sleep);
    }

    private void ResistParalyze(Effect arg_0, object param, Player arg_6) // sub_3C1A4
    {
        _affectsProtectedAction.ProtectedIf(Affects.paralyze);
    }

    private void AffectImmuneToCold(Effect arg_0, object param, Player arg_6) // sub_3C1B2
    {
        if ((gbl.damage_flags & DamageType.Cold) != 0)
        {
            _affectsProtectedAction.Protected();
        }
    }

    private void AffectImmuneToFire(Effect arg_0, object param, Player arg_6) // sub_3C1EA
    {
        if ((gbl.damage_flags & DamageType.Fire) != 0)
        {
            _affectsProtectedAction.Protected();
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

    private void AffectHalfElfResistance(Effect arg_0, object param, Player player) // sub_3C5D0
    {
        if (_ovr024.roll_dice(100, 1) <= 30)
        {
            _affectsProtectedAction.ProtectedIf(Affects.charm_person);
            _affectsProtectedAction.ProtectedIf(Affects.sleep);
        }
    }

    private void AffectProtMagic(Effect arg_0, object param, Player player) // sub_3C623
    {
        if (gbl.current_affect != 0 ||
            (gbl.damage_flags & DamageType.Magic) != 0)
        {
            _affectsProtectedAction.Protected();
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
                _applyAffectTable.CallAffectTable(Effect.Add, null, player, item.affect_2);
            }
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
            _affectsProtectedAction.Protected();
        }
    }

    private void AffectEntangle(Effect arg_0, object param, Player player) // sub_3C7CC
    {
        player.actions.move = 0;
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
}
