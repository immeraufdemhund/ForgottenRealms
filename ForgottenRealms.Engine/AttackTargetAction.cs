using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;

namespace ForgottenRealms.Engine;

public class AttackTargetAction
{
    private readonly BackStabMath _backStabMath;
    private readonly DisplayDriver _displayDriver;
    private readonly KeyboardDriver _keyboardDriver;
    private readonly SoundDriver _soundDriver;
    private readonly TargetDirectionMath _targetDirectionMath;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr033 _ovr033;

    public AttackTargetAction(BackStabMath backStabMath, DisplayDriver displayDriver, KeyboardDriver keyboardDriver, SoundDriver soundDriver, TargetDirectionMath targetDirectionMath, ovr024 ovr024, ovr025 ovr025, ovr033 ovr033)
    {
        _backStabMath = backStabMath;
        _displayDriver = displayDriver;
        _keyboardDriver = keyboardDriver;
        _soundDriver = soundDriver;
        _targetDirectionMath = targetDirectionMath;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr033 = ovr033;
    }

    internal bool AttackTarget(Item rangedWeapon, int attackType, Player target, Player attacker) // sub_3F9DB
    {
        int dir = 0;

        gbl.focusCombatAreaOnPlayer = true;
        gbl.display_hitpoints_ac = true;

        gbl.combat_round_no_action_limit = gbl.combat_round + gbl.combat_round_no_action_value;

        if (target.actions.AttacksReceived < 2 && attackType == 0)
        {
            dir = _targetDirectionMath.getTargetDirection(attacker, target);

            target.actions.direction = (dir + 4) % 8;
        }
        else if (_ovr033.PlayerOnScreen(false, target) == true)
        {
            dir = target.actions.direction;

            if (attackType == 0)
            {
                target.actions.direction = (dir + 4) % 8;
            }
        }

        if (_ovr033.PlayerOnScreen(false, target) == true)
        {
            _ovr033.draw_74B3F(false, Icon.Normal, dir, target);
        }

        dir = _targetDirectionMath.getTargetDirection(target, attacker);
        _ovr025.CombatDisplayPlayerSummary(attacker);

        _ovr033.draw_74B3F(false, Icon.Attack, dir, attacker);

        attacker.actions.target = target;

        _keyboardDriver.SysDelay(100);

        if (rangedWeapon != null)
        {
            DrawRangedAttack(rangedWeapon, target, attacker);
        }

        if (attacker.activeItems.primaryWeapon != null &&
            (attacker.activeItems.primaryWeapon.type == ItemType.Sling ||
             attacker.activeItems.primaryWeapon.type == ItemType.StaffSling))
        {
            DrawRangedAttack(attacker.activeItems.primaryWeapon, target, attacker);
        }

        bool turnComplete = true;

        if (attacker.attack1_AttacksLeft > 0 ||
            attacker.attack2_AttacksLeft > 0)
        {
            Player player_bkup = gbl.SelectedPlayer;

            gbl.SelectedPlayer = attacker;

            turnComplete = AttackTarget01(rangedWeapon, attackType, target, attacker);

            if (rangedWeapon != null)
            {
                if (rangedWeapon.count > 0)
                {
                    rangedWeapon.count = gbl.bytes_1D900[1];
                }

                if (rangedWeapon.count == 0)
                {
                    if (_ovr025.is_weapon_ranged_melee(attacker) == true &&
                        rangedWeapon.affect_3 != Affects.affect_89)
                    {
                        Item new_item = rangedWeapon.ShallowClone();
                        new_item.readied = false;

                        gbl.items_pointer.Add(new_item);

                        _ovr025.lose_item(rangedWeapon, attacker);
                        gbl.item_ptr = new_item;
                    }
                    else
                    {
                        _ovr025.lose_item(rangedWeapon, attacker);
                    }
                }
            }

            _ovr025.reclac_player_values(attacker);
            gbl.SelectedPlayer = player_bkup;
        }

        if (turnComplete == true)
        {
            _ovr025.clear_actions(attacker);
        }

        if (_ovr033.PlayerOnScreen(false, attacker) == true)
        {
            _ovr033.draw_74B3F(true, Icon.Attack, attacker.actions.direction, attacker);
            _ovr033.draw_74B3F(false, Icon.Normal, attacker.actions.direction, attacker);
        }

        return turnComplete;
    }

    private void DrawRangedAttack(Item item, Player target, Player attacker)
    {
        _soundDriver.PlaySound(Sound.sound_c);

        int dir = _targetDirectionMath.getTargetDirection(target, attacker);

        int frame_count = 1;
        int delay = 10;
        int iconId = 13;

        switch (item.type)
        {
            case ItemType.Dart:
            case ItemType.Javelin:
            case ItemType.DartOfHornetsNest:
            case ItemType.Quarrel:
            case ItemType.Spear:
            case ItemType.Arrow:
                if ((dir & 1) == 1)
                {
                    if (dir == 3 || dir == 5)
                    {
                        _ovr025.load_missile_dax((dir == 5), 0, Icon.Attack, iconId + 1);
                    }
                    else
                    {
                        _ovr025.load_missile_dax((dir == 7), 0, Icon.Normal, iconId + 1);
                    }
                }
                else
                {
                    if (dir >= 4)
                    {
                        _ovr025.load_missile_dax(false, 0, Icon.Attack, iconId + (dir % 4));
                    }
                    else
                    {
                        _ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + (dir % 4));
                    }
                }
                _soundDriver.PlaySound(Sound.sound_c);
                break;

            case ItemType.HandAxe:
            case ItemType.Club:
            case ItemType.Glaive:
                _ovr025.load_missile_icons(iconId + 3);
                frame_count = 4;
                delay = 50;
                _soundDriver.PlaySound(Sound.sound_9);
                break;

            case ItemType.Type_85:
            case ItemType.FlaskOfOil:
                _ovr025.load_missile_icons(iconId + 4);
                frame_count = 4;
                delay = 50;
                _soundDriver.PlaySound(Sound.sound_6);
                break;

            case ItemType.StaffSling:
            case ItemType.Sling:
            case ItemType.Spine:
                iconId++;
                _ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + 7);
                _ovr025.load_missile_dax(false, 1, Icon.Attack, iconId + 7);
                frame_count = 2;
                delay = 10;
                _soundDriver.PlaySound(Sound.sound_6);
                break;

            default:
                _ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + 7);
                _ovr025.load_missile_dax(false, 1, Icon.Attack, iconId + 7);
                frame_count = 2;
                delay = 20;
                _soundDriver.PlaySound(Sound.sound_9);
                break;
        }

        _ovr025.draw_missile_attack(delay, frame_count, _ovr033.PlayerMapPos(target), _ovr033.PlayerMapPos(attacker));
    }

    private bool AttackTarget01(Item item, int arg_8, Player target, Player attacker) // sub_3F4EB
    {
        int target_ac;
        bool turnComplete = true;
        bool BehindAttack = arg_8 != 0;
        turnComplete = false;
        gbl.bytes_1D2C9[1] = 0;
        gbl.bytes_1D2C9[2] = 0;
        gbl.bytes_1D900[1] = 0;
        gbl.bytes_1D900[2] = 0;
        bool var_11 = false;
        bool targetNotInCombat = false;
        gbl.damage = 0;

        attacker.actions.field_8 = true;

        if (target.IsHeld() == true)
        {
            _soundDriver.PlaySound(Sound.sound_attackHeld);

            while (attacker.AttacksLeft(attacker.actions.attackIdx) == 0)
            {
                attacker.actions.attackIdx--;
            }

            gbl.bytes_1D900[attacker.actions.attackIdx] += 1;

            DisplayAttackMessage(true, 1, target.hit_point_current + 5, AttackType.Slay, target, attacker);
            _ovr024.remove_invisibility(attacker);

            attacker.attack1_AttacksLeft = 0;
            attacker.attack2_AttacksLeft = 0;

            var_11 = true;
            turnComplete = true;
        }
        else
        {
            if (attacker.activeItems.primaryWeapon != null &&
                (target.field_DE > 0x80 || (target.field_DE & 7) > 1))
            {
                ItemData itemData = gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type];

                attacker.attack1_DiceCount = itemData.diceCountLarge;
                attacker.attack1_DiceSize = itemData.diceSizeLarge;
                attacker.attack1_DamageBonus -= itemData.bonusNormal;
                attacker.attack1_DamageBonus += itemData.bonusLarge;
            }

            _ovr025.reclac_player_values(target);
            _ovr024.CheckAffectsEffect(target, CheckType.Type_11);

            if (_backStabMath.CanBackStabTarget(target, attacker) == true)
            {
                target_ac = target.ac_behind - 4;
            }
            else
            {
                if (target.actions.AttacksReceived > 1 &&
                    _targetDirectionMath.getTargetDirection(target, attacker) == target.actions.direction &&
                    target.actions.directionChanges > 4)
                {
                    BehindAttack = true;
                }

                if (BehindAttack == true)
                {
                    target_ac = target.ac_behind;
                }
                else
                {
                    target_ac = target.ac;
                }
            }

            target_ac += RangedDefenseBonus(target, attacker);
            AttackType attack_type = AttackType.Normal;
            if (BehindAttack == true)
            {
                attack_type = AttackType.Behind;
            }

            if (_backStabMath.CanBackStabTarget(target, attacker) == true)
            {
                attack_type = AttackType.Backstab;
            }

            for (int attackIdx = attacker.actions.attackIdx; attackIdx >= 1; attackIdx--)
            {
                while (attacker.AttacksLeft(attackIdx) > 0 &&
                       targetNotInCombat == false)
                {
                    attacker.AttacksLeftDec(attackIdx);
                    attacker.actions.attackIdx = attackIdx;

                    gbl.bytes_1D900[attackIdx] += 1;

                    if (_ovr024.PC_CanHitTarget(target_ac, target, attacker) ||
                        target.IsHeld() == true)
                    {
                        gbl.bytes_1D2C9[attackIdx] += 1;

                        _soundDriver.PlaySound(Sound.sound_attackHeld);
                        var_11 = true;
                        sub_3E192(attackIdx, target, attacker);
                        DisplayAttackMessage(true, gbl.damage, gbl.damage, attack_type, target, attacker);

                        if (target.in_combat == true)
                        {
                            _ovr024.CheckAffectsEffect(attacker, (CheckType)attackIdx + 1);
                        }

                        if (target.in_combat == false)
                        {
                            targetNotInCombat = true;
                        }

                        if (attacker.in_combat == false)
                        {
                            attacker.AttacksLeftSet(attackIdx, 0);
                        }
                    }
                }
            }

            if (item != null &&
                item.count == 0 &&
                item.type == ItemType.DartOfHornetsNest)
            {
                attacker.attack1_AttacksLeft = 0;
                attacker.attack2_AttacksLeft = 0;
            }

            if (var_11 == false)
            {
                _soundDriver.PlaySound(Sound.sound_9);
                DisplayAttackMessage(false, 0, 0, attack_type, target, attacker);
            }

            turnComplete = true;
            if (attacker.attack1_AttacksLeft > 0 ||
                attacker.attack2_AttacksLeft > 0)
            {
                turnComplete = false;
            }

            attacker.actions.maxSweapTargets = 0;
        }

        if (attacker.in_combat == false)
        {
            turnComplete = true;
        }

        if (turnComplete == true)
        {
            _ovr025.clear_actions(attacker);
        }

        return turnComplete;
    }

    private void sub_3E192(int index, Player target, Player attacker)
    {
        gbl.damage = _ovr024.roll_dice_save(attacker.attackDiceSize(index), attacker.attackDiceCount(index));
        gbl.damage += attacker.attackDamageBonus(index);

        if (gbl.damage < 0)
        {
            gbl.damage = 0;
        }

        if (_backStabMath.CanBackStabTarget(target, attacker) == true)
        {
            gbl.damage *= ((attacker.SkillLevel(SkillType.Thief) - 1) / 4) + 2;
        }

        gbl.damage_flags = 0;
        _ovr024.CheckAffectsEffect(attacker, CheckType.SpecialAttacks);
        _ovr024.CheckAffectsEffect(target, CheckType.Type_5);
    }

    private void DisplayAttackMessage(bool attackHits, int attackDamge, int actualDamage, AttackType attack, Player target, Player attacker) /* backstab */
    {
        string text;

        if (attack == AttackType.Backstab)
        {
            text = "-Backstabs-";
        }
        else if (attack == AttackType.Slay)
        {
            text = "slays helpless";
        }
        else
        {
            text = "Attacks";
        }

        _ovr025.DisplayPlayerStatusString(false, 10, text, attacker);
        int line = 12;

        _ovr025.displayPlayerName(false, line, 0x17, target);
        line++;

        if (attack == AttackType.Behind)
        {
            text = "(from behind) ";
        }
        else
        {
            text = string.Empty;
        }

        if (attackHits == true)
        {
            if (attack == AttackType.Slay)
            {
                text = "with one cruel blow";
            }
            else
            {
                text += "Hitting for " + attackDamge.ToString();

                if (attackDamge == 1)
                {
                    text += " point ";
                }
                else
                {
                    text += " points ";
                }

                text += "of damage";

            }

            _ovr025.damage_player(actualDamage, target);
        }
        else
        {
            text += "and Misses";
        }

        if (target.health_status != Status.gone)
        {
            _displayDriver.press_any_key(text, true, 10, line + 3, 0x26, line, 0x17);
        }

        line = gbl.textYCol + 1;

        _displayDriver.GameDelay();

        if (actualDamage > 0)
        {
            _ovr024.TryLooseSpell(target);
        }

        if (target.in_combat == false)
        {
            _ovr025.DisplayPlayerStatusString(false, line, "goes down", target);
            line += 2;

            if (target.health_status == Status.dying)
            {
                _displayDriver.displayString("and is Dying", 0, 10, line, 0x17);
            }

            if (target.health_status == Status.dead ||
                target.health_status == Status.stoned ||
                target.health_status == Status.gone )
            {
                _ovr025.DisplayPlayerStatusString(false, line, "is killed", target);
            }

            line += 2;

            _ovr024.RemoveCombatAffects(target);

            _ovr024.CheckAffectsEffect(target, CheckType.Death);

            if (target.in_combat == false)
            {
                _ovr033.CombatantKilled(target);
            }
            else
            {
                _displayDriver.GameDelay();
            }
        }

        _ovr025.ClearPlayerTextArea();
    }

    private int RangedDefenseBonus(Player target, Player attacker) /* sub_3FCED */
    {
        if (_ovr025.is_weapon_ranged(attacker) == true)
        {
            int range = _ovr025.getTargetRange(target, attacker);

            int oneThirdRange = (gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type].range - 1) / 3;
            int acAdjustment = 0;

            if (range > oneThirdRange)
            {
                range -= oneThirdRange;
                acAdjustment += 2;
            }

            if (range > oneThirdRange)
            {
                range -= oneThirdRange;
                acAdjustment += 3;
            }

            return acAdjustment;
        }
        else
        {
            return 0;
        }
    }

    private enum AttackType
    {
        Normal = 0,
        Behind = 1,
        Backstab = 2,
        Slay = 3
    }
}
