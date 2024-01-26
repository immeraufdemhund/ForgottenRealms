using ForgottenRealms.Engine.Classes;
using ForgottenRealms.Engine.Classes.Combat;

namespace ForgottenRealms.Engine;

public class ovr014
{
    private readonly DisplayDriver _displayDriver;
    private readonly KeyboardDriver _keyboardDriver;
    private readonly KeyboardService _keyboardService;
    private readonly SoundDriver _soundDriver;
    private readonly ovr013 _ovr013;
    private readonly ovr020 _ovr020;
    private readonly ovr023 _ovr023;
    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    private readonly ovr027 _ovr027;
    private readonly ovr032 _ovr032;
    private readonly ovr033 _ovr033;
    private readonly seg037 _seg037;

    public ovr014(DisplayDriver displayDriver, KeyboardDriver keyboardDriver, KeyboardService keyboardService, SoundDriver soundDriver, ovr013 ovr013, ovr020 ovr020, ovr023 ovr023, ovr024 ovr024, ovr025 ovr025, ovr027 ovr027, ovr032 ovr032, ovr033 ovr033, seg037 seg037)
    {
        _displayDriver = displayDriver;
        _keyboardDriver = keyboardDriver;
        _keyboardService = keyboardService;
        _soundDriver = soundDriver;
        _ovr013 = ovr013;
        _ovr020 = ovr020;
        _ovr023 = ovr023;
        _ovr024 = ovr024;
        _ovr025 = ovr025;
        _ovr027 = ovr027;
        _ovr032 = ovr032;
        _ovr033 = ovr033;
        _seg037 = seg037;
    }

    internal void CalculateInitiative(Player player) // sub_3E000
    {
        Action action = player.actions;

        action.spell_id = 0;
        action.can_cast = true;
        action.can_use = true;
        action.field_8 = false;
        action.attackIdx = 2;

        reclac_attacks(player);
        gbl.halfActionsLeft = player.baseHalfMoves;

        gbl.resetMovesLeft = false;

        _ovr024.CheckAffectsEffect(player, CheckType.Movement);

        player.attack2_AttacksLeft = (byte)ThisRoundActionCount(gbl.halfActionsLeft);

        action.maxSweapTargets = player.attackLevel;

        if (player.in_combat == true)
        {
            action.delay = (sbyte)(_ovr024.roll_dice(6, 1) + _ovr025.DexReactionAdj(player));

            if (action.delay < 1)
            {
                action.delay = 1;
            }

            if ((((int)player.combat_team + 1) & gbl.area2_ptr.field_596) != 0)
            {
                action.delay -= 6;
            }

            if (action.delay < 0 ||
                action.delay > 20)
            {
                action.delay = 0;
            }
        }
        else
        {
            action.delay = 0;
        }

        player.actions.move = CalcMoves(player);
    }

    internal int CalcMoves(Player player) // sub_3E124
    {
        int moves = player.movement;

        if (player.in_combat == false)
        {
            moves += gbl.area2_ptr.field_6E4;
        }

        if (moves < 1 || moves > 96)
        {
            moves = 1;
        }

        gbl.halfActionsLeft = moves * 2;

        gbl.resetMovesLeft = true;

        _ovr024.CheckAffectsEffect(player, CheckType.Movement);

        gbl.resetMovesLeft = false;

        return gbl.halfActionsLeft;
    }

    private void sub_3E192(int index, Player target, Player attacker)
    {
        gbl.damage = _ovr024.roll_dice_save(attacker.attackDiceSize(index), attacker.attackDiceCount(index));
        gbl.damage += attacker.attackDamageBonus(index);

        if (gbl.damage < 0)
        {
            gbl.damage = 0;
        }

        if (CanBackStabTarget(target, attacker) == true)
        {
            gbl.damage *= ((attacker.SkillLevel(SkillType.Thief) - 1) / 4) + 2;
        }

        gbl.damage_flags = 0;
        _ovr024.CheckAffectsEffect(attacker, CheckType.SpecialAttacks);
        _ovr024.CheckAffectsEffect(target, CheckType.Type_5);
    }

    private enum AttackType
    {
        Normal = 0,
        Behind = 1,
        Backstab = 2,
        Slay = 3
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

    private void move_step_into_attack(Player target) /* sub_3E65D */
    {
        var nearTargets = _ovr025.BuildNearTargets(1, target);

        if (target.in_combat)
        {
            foreach (var cpi in nearTargets)
            {
                Player attacker = cpi.player;

                if (attacker.actions.guarding == true &&
                    attacker.IsHeld() == false)
                {
                    _ovr033.redrawCombatArea(8, 2, _ovr033.PlayerMapPos(target));

                    attacker.actions.guarding = false;

                    RecalcAttacksReceived(target, attacker);

                    AttackTarget(null, 0, target, attacker);
                }
            }
        }
    }

    internal void sub_3E748(int direction, Player player)
    {
        int player_index = _ovr033.GetPlayerIndex(player);

        Point oldPos = gbl.CombatMap[player_index].pos;
        Point newPos = oldPos + gbl.MapDirectionDelta[direction];

        // TODO does this solve more problems than it causes? Regarding AI flee
        if (newPos.MapInBounds() == false)
        {
            return;
        }

        int costToMove = 0;
        if ((direction & 0x01) != 0)
        {
            // Diagonal walking...
            costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newPos]].move_cost * 3;
        }
        else
        {
            costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newPos]].move_cost * 2;
        }

        if (costToMove > player.actions.move)
        {
            player.actions.move = 0;
        }
        else
        {
            player.actions.move -= costToMove;
        }

        byte radius = 1;

        if (player.quick_fight == QuickFight.True)
        {
            radius = 3;

            if (_ovr033.CoordOnScreen(newPos - gbl.mapToBackGroundTile.mapScreenTopLeft) == false &&
                gbl.focusCombatAreaOnPlayer == true)
            {
                _ovr033.redrawCombatArea(8, 2, oldPos);
            }
        }

        if (gbl.focusCombatAreaOnPlayer == true)
        {
            _ovr033.RedrawPlayerBackground(player_index);
        }

        gbl.CombatMap[player_index].pos = newPos;

        _ovr033.setup_mapToPlayerIndex_and_playerScreen();

        if (gbl.focusCombatAreaOnPlayer == true)
        {
            _ovr033.redrawCombatArea(8, radius, newPos);
        }

        player.actions.AttacksReceived = 0;
        player.actions.directionChanges = 0;
        _soundDriver.PlaySound(Sound.sound_a);

        move_step_into_attack(player);

        if (player.in_combat == false ||
            player.IsHeld() == true)
        {
            player.actions.move = 0;
        }
    }

    internal void move_step_away_attack(int direction, Player player) /* sub_3E954 */
    {
        var originAttackers = _ovr025.BuildNearTargets(1, player);

        if (originAttackers.Count == 0)
        {
            return;
        }

        var combatmap = gbl.CombatMap[_ovr033.GetPlayerIndex(player)];

        // move to destination position
        combatmap.pos += gbl.MapDirectionDelta[direction];

        var destAttackers = _ovr025.BuildNearTargets(1, player);

        // move back to original position
        combatmap.pos -= gbl.MapDirectionDelta[direction];

        // remove attackers from both locations
        foreach (var cpiB in destAttackers)
        {
            originAttackers.RemoveAll(cpiA => cpiA.player == cpiB.player);
        }

        if (player.in_combat == false)
        {
            //what the heck are we doing here then?
            // and why is this test not earlier in the function.
            //throw new System.NotSupportedException();
            return;
        }

        foreach (var cpiA in originAttackers)
        {
            gbl.display_hitpoints_ac = true;
            gbl.focusCombatAreaOnPlayer = true;
            bool found = false;

            Player attacker = cpiA.player;

            if (attacker.IsHeld() == false &&
                CanSeeTargetA(player, attacker) == true &&
                attacker.HasAffect(Affects.weap_dragon_slayer) == false &&
                attacker.HasAffect(Affects.affect_4a) == false)
            {
                int end_dir = attacker.actions.direction + 10;

                for (int tmpDir = attacker.actions.direction + 6; tmpDir <= end_dir; tmpDir++)
                {
                    if (found == false)
                    {
                        if (attacker.actions.delay > 0 ||
                            attacker.actions.AttacksReceived == 0 ||
                            _ovr032.CanSeeCombatant(tmpDir % 8, _ovr033.PlayerMapPos(player), _ovr033.PlayerMapPos(attacker)) == true)
                        {
                            byte attackIndex = 1;
                            if (attacker.attacksCount == 0)
                            {
                                attackIndex = 2;
                            }

                            if (attacker.attack1_AttacksLeft > 0)
                            {
                                attackIndex = 1;
                            }

                            if (attacker.attack2_AttacksLeft > 0)
                            {
                                attackIndex = 2;
                            }

                            if (attacker.AttacksLeft(attackIndex) == 0)
                            {
                                attacker.AttacksLeftSet(attackIndex, 1);
                            }

                            attacker.actions.attackIdx = attackIndex;

                            Player backupTarget = attacker.actions.target;

                            AttackTarget(null, 1, player, attacker);
                            found = true;

                            attacker.actions.target = backupTarget;

                            if (player.in_combat == true)
                            {
                                gbl.display_hitpoints_ac = true;
                                _ovr025.CombatDisplayPlayerSummary(player);
                            }
                        }
                    }
                }
            }
        }
    }

    internal void flee_battle(Player player)
    {
        bool gets_away = false;

        if (_ovr025.BuildNearTargets(0xff, player).Count == 0)
        {
            gets_away = true;
        }
        else
        {
            int var_4 = CalcMoves(player) / 2;
            int var_3 = MaxOppositionMoves(player);

            if (var_3 < var_4)
            {
                gets_away = true;
            }
            else if (var_3 == var_4 && _ovr024.roll_dice(2, 1) == 1)
            {
                gets_away = true;
            }
        }

        if (gets_away == true)
        {
            _ovr024.RemoveFromCombat("Got Away", Status.running, player);
        }
        else
        {
            _ovr025.string_print01("Escape is blocked");
        }

        _ovr025.clear_actions(player);
    }

    internal void reclac_attacks(Player player) // sub_3EDD4
    {
        bool foundRanged = false;
        Item rangedItem = null;
        int origAttacks = player.attack1_AttacksLeft;
        player.attack1_AttacksLeft = player.attacksCount;

        if (_ovr025.is_weapon_ranged(player) == true &&
            _ovr025.GetCurrentAttackItem(out rangedItem, player) == true)
        {
            foundRanged = true;
            int numAttacks = gbl.ItemDataTable[player.activeItems.primaryWeapon.type].numberAttacks;

            if (numAttacks < 2)
            {
                numAttacks = 2;
            }

            gbl.halfActionsLeft = numAttacks;
        }
        else
        {
            gbl.halfActionsLeft = player.attack1_AttacksLeft;
        }

        gbl.resetMovesLeft = false;
        _ovr024.CheckAffectsEffect(player, CheckType.Movement);

        int attacks = ThisRoundActionCount(gbl.halfActionsLeft);

        if (foundRanged == true &&
            rangedItem != null)
        {
            int rangedAmmo = 1;
            if (rangedItem.count > rangedAmmo)
            {
                rangedAmmo = rangedItem.count;
            }

            if (rangedAmmo < attacks &&
                rangedItem.count > 0)
            {
                attacks = rangedAmmo;
            }
        }

        if (player.actions.field_8 == false ||
            attacks < origAttacks ||
            (player.actions.field_8 == true &&
             attacks < (origAttacks * 2) &&
             foundRanged == false))
        {
            player.attack1_AttacksLeft = (byte)attacks;
        }
    }

    private int ThisRoundActionCount(int halfActionsLeft) // sub_3EF0D
    {
        if ((gbl.combat_round & 1) == 1)
        {
            halfActionsLeft++;
        }

        return halfActionsLeft / 2;
    }

    internal bool TrySweepAttack(Player target, Player attacker) // sub_3EF3D
    {
        if (attacker.attack1_AttacksLeft < attacker.actions.maxSweapTargets &&
            target.HitDice == 0 &&
            _ovr025.getTargetRange(target, attacker) == 1)
        {
            var nearTargets = _ovr025.BuildNearTargets(1, attacker);

            var targetepi = nearTargets.Find(epi => epi.player == target);
            int sweepableCount = nearTargets.FindAll(epi => epi.player.HitDice == 0).Count;

            if (sweepableCount > attacker.attack1_AttacksLeft)
            {
                if (sweepableCount > attacker.actions.maxSweapTargets)
                {
                    sweepableCount = attacker.actions.maxSweapTargets;
                }

                _ovr025.DisplayPlayerStatusString(true, 10, "sweeps", attacker);

                nearTargets.Remove(targetepi);
                nearTargets.Insert(0, targetepi);

                foreach (var sweepepi in nearTargets.FindAll(e => e.player.hitBonus == 0).GetRange(0, sweepableCount))
                {
                    var sweeptarget = sweepepi.player;
                    RecalcAttacksReceived(sweeptarget, attacker);

                    attacker.attack1_AttacksLeft = 1;

                    AttackTarget(null, 0, sweeptarget, attacker);
                }

                return true;
            }
        }

        return false;
    }

    internal bool CanSeeTargetA(Player targetA, Player targetB) //sub_3F143
    {
        if (targetA != null)
        {
            if (targetB == targetA)
            {
                return true;
            }
            else
            {
                gbl.targetInvisible = false;

                _ovr024.CheckAffectsEffect(targetA, CheckType.Visibility);

                if (gbl.targetInvisible == false)
                {
                    var old_target = targetB.actions.target;

                    targetB.actions.target = targetA;

                    _ovr024.CheckAffectsEffect(targetB, CheckType.None);

                    targetB.actions.target = old_target;
                }

                return (gbl.targetInvisible == false);
            }
        }

        return false;
    }

    private sbyte[] /*seg600:0369*/ unk_16679 = { 0,
        17, 17,  0,  0,  1, 17,  0,  0, 32, 32,
        10,  7,  4,  1,  1,  0,  0, -1, -1, -1 };

    internal void turns_undead(Player player)
    {
        _ovr025.DisplayPlayerStatusString(false, 10, "turns undead...", player);
        _ovr027.ClearPromptArea();
        _displayDriver.GameDelay();

        bool any_turned = false;
        byte var_6 = 0;

        player.actions.hasTurnedUndead = true;

        byte var_3 = 6;
        int var_2 = _ovr024.roll_dice(12, 1);
        int var_1 = _ovr024.roll_dice(20, 1);

        int clericLvl = player.SkillLevel(SkillType.Cleric);

        byte var_B;

        if (clericLvl >= 1 && clericLvl <= 8)
        {
            var_B = player.cleric_lvl;
        }
        else if (clericLvl >= 9 && clericLvl <= 13)
        {
            var_B = 9;
        }
        else
        {
            var_B = 10;
        }

        Player target;
        while (FindLowestE9Target(out target, player) == true && var_2 > 0 && var_6 == 0)
        {
            int var_4 = unk_16679[(target.field_E9 * 10) + var_B];

            if (var_1 >= System.Math.Abs(var_4))
            {
                any_turned = true;

                _ovr033.RedrawCombatIfFocusOn(false, 3, target);
                gbl.display_hitpoints_ac = true;
                _ovr025.CombatDisplayPlayerSummary(target);

                if (var_4 > 0)
                {
                    target.actions.fleeing = true;
                    _ovr025.MagicAttackDisplay("is turned", true, target);
                }
                else
                {
                    _ovr025.DisplayPlayerStatusString(false, 10, "Is destroyed", target);
                    _ovr033.CombatantKilled(target);
                    target.health_status = Status.gone;
                    target.in_combat = false;
                }

                if (var_3 > 0)
                {
                    var_3 -= 1;
                }

                var_2 -= 1;

                if (var_2 == 0 && var_3 > 0 && var_4 < 0)
                {
                    var_2++;
                }

                _ovr025.ClearPlayerTextArea();
            }
            else
            {
                var_6 = 1;
            }
        }

        if (any_turned == false)
        {
            _ovr025.string_print01("Nothing Happens...");
        }

        _ovr025.CountCombatTeamMembers();
        _ovr025.clear_actions(player);

        _ovr025.ClearPlayerTextArea();
    }

    internal bool FindLowestE9Target(out Player output, Player player) /* sub_3F433 */
    {
        output = null;

        byte minE9 = 13;

        var nearTargets = _ovr025.BuildNearTargets(0xff, player);
        bool result = false;

        foreach (var epi in nearTargets)
        {
            Player target = epi.player;

            if (target.actions.fleeing == false &&
                target.field_E9 > 0 &&
                target.field_E9 < minE9)
            {
                minE9 = target.field_E9;
                output = target;
                result = true;
            }
        }

        return result;
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

            if (CanBackStabTarget(target, attacker) == true)
            {
                target_ac = target.ac_behind - 4;
            }
            else
            {
                if (target.actions.AttacksReceived > 1 &&
                    getTargetDirection(target, attacker) == target.actions.direction &&
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

            if (CanBackStabTarget(target, attacker) == true)
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

    internal void RecalcAttacksReceived(Player target, Player attacker) // sub_3F94D
    {
        target.actions.AttacksReceived++;

        int targetDir = getTargetDirection(attacker, target);

        int dirDiff = ((targetDir - target.actions.direction) + 8) % 8;

        if (dirDiff > 4)
        {
            dirDiff = 8 - dirDiff;
        }

        target.actions.directionChanges = (target.actions.directionChanges + dirDiff) % 8;
    }


    internal bool AttackTarget(Item rangedWeapon, int attackType, Player target, Player attacker) // sub_3F9DB
    {
        int dir = 0;

        gbl.focusCombatAreaOnPlayer = true;
        gbl.display_hitpoints_ac = true;

        gbl.combat_round_no_action_limit = gbl.combat_round + gbl.combat_round_no_action_value;

        if (target.actions.AttacksReceived < 2 && attackType == 0)
        {
            dir = getTargetDirection(attacker, target);

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

        dir = getTargetDirection(target, attacker);
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

    internal bool find_healing_target(out Player target, Player healer) /* sub_3FDFE */
    {
        Player lowest_target = null;
        int lowest_hp = 0x0FF;
        DownedPlayerTile var_8 = null;

        for (int dir = 0; dir <= 8; dir++)
        {
            var map = gbl.MapDirectionDelta[dir] + _ovr033.PlayerMapPos(healer);

            int ground_tile;
            int player_index;
            _ovr033.AtMapXY(out ground_tile, out player_index, map);

            if (player_index > 0)
            {
                target = gbl.player_array[player_index];

                if (target.combat_team == healer.combat_team &&
                    target.hit_point_current < target.hit_point_max)
                {
                    if (target.hit_point_current < lowest_hp ||
                        (target == healer && target.hit_point_current < (target.hit_point_max / 2)))
                    {
                        lowest_target = target;
                        lowest_hp = target.hit_point_current;
                    }
                }
            }
            else if (ground_tile == gbl.Tile_DownPlayer)
            {
                var_8 = gbl.downedPlayers.FindLast(cell => cell.target != null && cell.map == map &&
                                                           cell.target.health_status != Status.tempgone && cell.target.health_status != Status.running &&
                                                           cell.target.health_status != Status.unconscious);
            }
        }

        if (lowest_hp < 8 ||
            var_8 == null)
        {
            target = lowest_target;
        }
        else
        {
            target = var_8.target;
        }

        bool target_found = (target != null);

        return target_found;
    }

    private Affects[] unk_18ADB = { Affects.bless, Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless }; // seg600:27CB first is filler (off by 1)

    private bool sub_4001C(DownedPlayerTile arg_0, bool canTargetEmptyGround, QuickFight quick_fight, int spellId)
    {
        bool var_2 = false;
        if (quick_fight == QuickFight.False)
        {
            bool allowTarget = spellId != 0x53;

            var_2 = aim_menu(arg_0, allowTarget, canTargetEmptyGround, false, _ovr023.SpellRange(spellId), gbl.SelectedPlayer);
            gbl.SelectedPlayer.actions.target = arg_0.target;
        }
        else if (gbl.spellCastingTable[spellId].field_E == 0)
        {
            arg_0.target = gbl.SelectedPlayer;

            if (spellId != 3 || find_healing_target(out arg_0.target, gbl.SelectedPlayer))
            {
                arg_0.map = _ovr033.PlayerMapPos(arg_0.target);
                var_2 = true;
            }
        }
        else
        {
            int var_9 = 1;

            while (var_9 > 0 &&
                   var_2 == false)
            {
                bool var_3 = true;

                if (find_target(true, 0, _ovr023.SpellRange(spellId), gbl.SelectedPlayer) == true)
                {
                    Player target = gbl.SelectedPlayer.actions.target;

                    if (target.IsHeld() == true)
                    {
                        for (int i = 1; i <= 4; i++)
                        {
                            if (gbl.spellCastingTable[spellId].affect_id == unk_18ADB[i])
                            {
                                var_3 = false;
                            }
                        }
                    }

                    if (var_3 == true)
                    {
                        arg_0.target = gbl.SelectedPlayer.actions.target;
                        arg_0.map = _ovr033.PlayerMapPos(arg_0.target);
                        var_2 = true;
                    }
                }

                var_9 -= 1;
            }
        }


        if (var_2 == true)
        {
            gbl.targetPos = arg_0.map;
        }
        else
        {
            arg_0.Clear();
        }

        return var_2;
    }

    internal bool target(QuickFight quick_fight, int spellId)
    {
        DownedPlayerTile var_C = new DownedPlayerTile();

        bool castSpell = true;
        gbl.spellTargets.Clear();
        gbl.byte_1D2C7 = false;

        gbl.targetPos = _ovr033.PlayerMapPos(gbl.SelectedPlayer);

        int tmp1 = gbl.spellCastingTable[spellId].field_6 & 0x0F;

        if (tmp1 == 0)
        {
            gbl.spellTargets.Clear();
            gbl.spellTargets.Add(gbl.SelectedPlayer);
        }
        else if (tmp1 == 5)
        {
            int var_5 = 0;
            gbl.spellTargets.Clear();

            int var_4;

            if (spellId == 0x4F)
            {
                var_4 = _ovr025.spellMaxTargetCount(0x4F);
            }
            else
            {
                var_4 = _ovr024.roll_dice(4, 2);
            }

            bool stop_loop = false;

            do
            {
                if (sub_4001C(var_C, false, quick_fight, spellId) == true)
                {
                    bool found = gbl.spellTargets.Exists(st => st == var_C.target);

                    if (found == false)
                    {
                        Player target = var_C.target;
                        gbl.spellTargets.Add(target);

                        gbl.targetPos = _ovr033.PlayerMapPos(var_C.target);

                        if (spellId != 0x4f)
                        {
                            byte hitDice = target.HitDice;

                            if (hitDice == 0 || hitDice == 1)
                            {
                                var_5 += 1;
                            }
                            else if (hitDice == 2)
                            {
                                var_5 += 2;
                            }
                            else if (hitDice == 3)
                            {
                                var_5 += 4;
                            }
                            else
                            {
                                var_5 += 8;
                            }
                        }
                        else
                        {
                            byte al = target.field_DE;

                            if (al == 1)
                            {
                                var_5 += 1;
                            }
                            else if (al == 2 || al == 3)
                            {
                                var_5 += 2;
                            }
                            else if (al == 4)
                            {
                                var_5 += 4;
                            }
                        }

                        if (gbl.spellTargets.Count > 0 && var_5 > var_4)
                        {
                            stop_loop = true;
                        }
                    }
                    else
                    {
                        if (quick_fight != 0)
                        {
                            var_4 -= 1;
                        }
                        else
                        {
                            _ovr025.string_print01("Already been targeted");
                        }
                    }

                    _ovr033.RedrawPosition(_ovr033.PlayerMapPos(var_C.target));
                }
                else
                {
                    stop_loop = true;
                }
            } while (stop_loop == false && var_4 != 0);
        }
        else if (tmp1 == 0x0F)
        {
            if (sub_4001C(var_C, false, quick_fight, spellId) == true)
            {
                if (gbl.SelectedPlayer.actions.target != null)
                {
                    gbl.spellTargets.Clear();
                    gbl.spellTargets.Add(gbl.SelectedPlayer.actions.target);
                }
                else
                {
                    /* TODO it doesn't make sense to mask the low nibble then shift it out */
                    var scl = _ovr032.Rebuild_SortedCombatantList(1, (gbl.spellCastingTable[spellId].field_6 & 0x0f) >> 4, gbl.targetPos, sc => true);

                    gbl.spellTargets.Clear();
                    foreach (var sc in scl)
                    {
                        gbl.spellTargets.Add(sc.player);
                    }
                    gbl.byte_1D2C7 = true;
                }
            }
            else
            {
                castSpell = false;
            }
        }
        else if (tmp1 >= 8 && tmp1 <= 0x0E)
        {
            if (sub_4001C(var_C, true, quick_fight, spellId) == true)
            {
                var scl = _ovr032.Rebuild_SortedCombatantList(1, gbl.spellCastingTable[spellId].field_6 & 7, gbl.targetPos, sc => true);

                gbl.spellTargets.Clear();
                foreach (var sc in scl)
                {
                    gbl.spellTargets.Add(sc.player);
                }

                gbl.byte_1D2C7 = true;
            }
            else
            {
                castSpell = false;
            }
        }
        else
        {
            int max_targets = (gbl.spellCastingTable[spellId].field_6 & 3) + 1;
            gbl.spellTargets.Clear();

            while (max_targets > 0)
            {
                if (sub_4001C(var_C, false, quick_fight, spellId) == true)
                {
                    bool found = gbl.spellTargets.Exists(st => st == var_C.target);

                    if (found == false)
                    {
                        gbl.spellTargets.Add(var_C.target);
                        max_targets -= 1;

                        gbl.targetPos = _ovr033.PlayerMapPos(var_C.target);
                    }
                    else
                    {
                        if (quick_fight == 0)
                        {
                            _ovr025.string_print01("Already been targeted");
                        }
                        else
                        {
                            max_targets -= 1;
                        }
                    }

                    _ovr033.RedrawPosition(_ovr033.PlayerMapPos(var_C.target));
                }
                else
                {
                    max_targets = 0;
                }
            }

            if (gbl.spellTargets.Count == 0)
            {
                castSpell = false;
                gbl.targetPos = new Point();
            }

            //gbl.targetPos = ovr033.PlayerMapPos(gbl.spellTargets[gbl.spellTargets.Count-1]);
        }

        return castSpell;
    }


    internal void spell_menu3(out bool casting_spell, QuickFight quick_fight, int spell_id)
    {
        Player player = gbl.SelectedPlayer;
        bool var_6 = true;
        int var_5 = -1;
        casting_spell = false;

        if (spell_id == 0)
        {
            spell_id = _ovr020.spell_menu2(out var_6, ref var_5, SpellSource.Cast, SpellLoc.memory);
        }

        if (spell_id > 0 &&
            gbl.spellCastingTable[spell_id].whenCast == SpellWhen.Camp)
        {
            _ovr025.string_print01("Camp Only Spell");
            spell_id = 0;
        }

        if (quick_fight == QuickFight.False)
        {
            _ovr025.RedrawCombatScreen();
            gbl.focusCombatAreaOnPlayer = true;
            gbl.display_hitpoints_ac = true;

            _ovr033.RedrawCombatIfFocusOn(true, 3, player);
            _ovr025.CombatDisplayPlayerSummary(player);
        }

        if (spell_id > 0)
        {
            sbyte delay = (sbyte)(gbl.spellCastingTable[spell_id].castingDelay / 3);

            if (delay == 0)
            {
                _ovr023.sub_5D2E1(true, quick_fight, spell_id);

                casting_spell = true;
                _ovr025.clear_actions(player);
            }
            else
            {
                casting_spell = true;
                _ovr025.DisplayPlayerStatusString(true, 10, "Begins Casting", player);

                player.actions.spell_id = spell_id;

                if (player.actions.delay > delay)
                {
                    player.actions.delay = delay;
                }
                else
                {
                    player.actions.delay = 1;
                }
            }
        }
    }


    private bool CanBackStabTarget(Player target, Player attacker) /* sub_408D7 */
    {
        if (attacker.SkillLevel(SkillType.Thief) > 0)
        {
            Item weapon = attacker.activeItems.primaryWeapon;

            if (weapon == null ||
                weapon.type == ItemType.DrowLongSword ||
                weapon.type == ItemType.Club ||
                weapon.type == ItemType.Dagger ||
                weapon.type == ItemType.BroadSword ||
                weapon.type == ItemType.LongSword ||
                weapon.type == ItemType.ShortSword)
            {
                if (target.actions.AttacksReceived > 1 &&
                    (target.field_DE & 0x7F) <= 1 &&
                    getTargetDirection(target, attacker) == target.actions.direction)
                {
                    return true;
                }
            }
        }

        return false;
    }


    internal byte getTargetDirection(Player playerB, Player playerA) /* sub_409BC */
    {
        Point plyr_a = _ovr033.PlayerMapPos(playerA);
        Point plyr_b = _ovr033.PlayerMapPos(playerB);

        int diff_x = System.Math.Abs(plyr_b.x - plyr_a.x);
        int diff_y = System.Math.Abs(plyr_b.y - plyr_a.y);

        byte direction = 0;
        bool solved = false;

        while (solved == false)
        {
            switch (direction)
            {
                case 0:
                    if (plyr_b.y > plyr_a.y ||
                        ((0x26A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 2:
                    if (plyr_b.x < plyr_a.x ||
                        ((0x6A * diff_x) / 0x100) < diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 4:
                    if (plyr_b.y < plyr_a.y ||
                        ((0x26A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 6:
                    if (plyr_b.x > plyr_a.x ||
                        ((0x6A * diff_x) / 0x100) < diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 1:
                    if (plyr_b.y > plyr_a.y ||
                        plyr_b.x < plyr_a.x ||
                        ((0x26A * diff_x) / 0x100) < diff_y ||
                        ((0x6A * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 3:
                    if (plyr_b.y < plyr_a.y ||
                        plyr_b.x < plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 5:
                    if (plyr_b.y < plyr_a.y ||
                        plyr_b.x > plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;

                case 7:
                    if (plyr_b.y > plyr_a.y ||
                        plyr_b.x > plyr_a.x ||
                        ((0x26a * diff_x) / 0x100) < diff_y ||
                        ((0x6a * diff_x) / 0x100) > diff_y)
                    {
                        solved = false;
                    }
                    else
                    {
                        solved = true;
                    }
                    break;
            }

            if (solved == false)
            {
                direction++;
            }
        }

        return direction;
    }

    private void DrawRangedAttack(Item item, Player target, Player attacker) /* sub_40BF1 */
    {
        _soundDriver.PlaySound(Sound.sound_c);

        int dir = getTargetDirection(target, attacker);

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

    internal void calc_enemy_health_percentage() /* sub_40E00 */
    {
        int maxTotal = 0;
        int currentTotal = 0;

        foreach (Player player in gbl.TeamList)
        {
            if (player.combat_team == CombatTeam.Enemy)
            {
                if (player.in_combat == true)
                {
                    currentTotal += player.hit_point_current;
                }

                maxTotal += player.hit_point_max;
            }
        }

        if (maxTotal > 0)
        {
            gbl.enemyHealthPercentage = ((20 * currentTotal) / maxTotal) * 5;
        }
    }

    internal int MaxOppositionMoves(Player player) // sub_40E8F
    {
        int maxMoves = 0;

        foreach (Player mob in gbl.TeamList)
        {
            if (player.OppositeTeam() == mob.combat_team && mob.in_combat == true)
            {
                int moves = CalcMoves(mob) / 2;

                maxMoves = System.Math.Max(moves, maxMoves);
            }
        }

        return maxMoves;
    }

    internal bool can_attack_target(Player target, Player attacker) /* sub_40F1F */
    {
        bool result;

        if (target.OppositeTeam() == attacker.combat_team ||
            attacker.quick_fight == QuickFight.True)
        {
            result = true;
        }
        else if (_ovr027.yes_no(gbl.defaultMenuColors, "Attack Ally: ") != 'Y')
        {
            result = false;
        }
        else
        {
            result = true;
            gbl.area2_ptr.field_666 = 1;

            foreach (Player player in gbl.TeamList)
            {
                if (player.health_status == Status.okey &&
                    player.control_morale >= Control.NPC_Base)
                {
                    player.combat_team = CombatTeam.Enemy;
                    player.actions.target = null;
                }
            }

            _ovr025.CountCombatTeamMembers();
        }

        return result;
    }

    private char aim_sub_menu(bool showTarget, bool showRange, int maxRange, Player target, Player attacker) /* Aim_menu */
    {
        string text = string.Empty;
        int range = _ovr025.getTargetRange(target, attacker);
        int direction = getTargetDirection(target, attacker);

        if (showRange == true)
        {
            string range_txt = "Range = " + range.ToString() + "  ";
            _displayDriver.displayString(range_txt, 0, 10, 0x17, 0);
        }

        if (range <= maxRange)
        {
            if (showRange == false)
            {
                if (showTarget == true)
                {
                    text = "Target ";
                }
                else
                {
                    text = string.Empty;
                }
            }
            else if (target != attacker)
            {
                if (_ovr025.is_weapon_ranged(attacker) == false)
                {
                    text = "Target ";
                }
                else
                {
                    Item dummyItem;
                    if (_ovr025.GetCurrentAttackItem(out dummyItem, attacker) == true &&
                        (_ovr025.BuildNearTargets(1, attacker).Count == 0 || _ovr025.is_weapon_ranged_melee(attacker) == true))
                    {
                        text = "Target ";
                    }
                }
            }
        }

        text = "Next Prev Manual " + text + "Center Exit";
        _ovr033.RedrawCombatIfFocusOn(true, 3, target);
        gbl.display_hitpoints_ac = true;
        _ovr025.CombatDisplayPlayerSummary(target);

        char input_key = _ovr027.displayInput(false, 1, gbl.defaultMenuColors, text, "Aim:");

        return input_key;
    }


    private bool sub_411D8(DownedPlayerTile arg_0, bool showRange, Player target, Player attacker)
    {
        bool arg_4 = true;

        if (showRange &&
            can_attack_target(target, attacker) == false)
        {
            arg_4 = false;
        }

        if (arg_4 == true)
        {
            arg_0.target = target;
            arg_0.map = _ovr033.PlayerMapPos(target);
            gbl.mapToBackGroundTile.drawTargetCursor = false;

            _ovr033.redrawCombatArea(8, 3, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);

            if (showRange)
            {
                if (TrySweepAttack(target, attacker) == true)
                {
                    arg_4 = true;
                    _ovr025.clear_actions(attacker);
                }
                else
                {
                    RecalcAttacksReceived(target, attacker);

                    Item rangedWeapon = null;

                    if (_ovr025.is_weapon_ranged(attacker) == true &&
                        _ovr025.GetCurrentAttackItem(out rangedWeapon, attacker) == true &&
                        _ovr025.is_weapon_ranged_melee(attacker) == true &&
                        _ovr025.getTargetRange(target, attacker) == 0)
                    {
                        rangedWeapon = null;
                    }

                    arg_4 = AttackTarget(rangedWeapon, 0, target, attacker);
                }
            }
        }
        else
        {
            arg_0.Clear();
        }

        return arg_4;
    }

    private Set asc_41342 = new Set(0, 69, 84);

    private bool Target(DownedPlayerTile arg_0, bool allowTarget, bool canTargetEmptyGround, bool showRange, int maxRange, Player target, Player player01)
    {
        Item dummyItem;

        arg_0.Clear();

        var pos = _ovr033.PlayerMapPos(target);

        char input_key = ' ';
        byte dir = 8;

        bool arg_4 = false;

        gbl.mapToBackGroundTile.drawTargetCursor = true;
        gbl.mapToBackGroundTile.size = 1;

        while (asc_41342.MemberOf(input_key) == false)
        {
            _ovr033.redrawCombatArea(dir, 3, pos);
            pos += gbl.MapDirectionDelta[dir];
            pos.MapBoundaryTrunc();

            int groundTile;
            int playerAtXY;

            _ovr033.AtMapXY(out groundTile, out playerAtXY, pos);
            _keyboardService.clear_keyboard();
            bool can_target = false;
            int range = 255;

            if (_ovr032.canReachTarget(ref range, pos, _ovr033.PlayerMapPos(player01)) == true)
            {
                can_target = true;

                if (showRange)
                {
                    string range_text = "Range = " + (range / 2).ToString() + "  ";

                    _displayDriver.displayString(range_text, 0, 10, 0x17, 0);
                }
            }
            else
            {
                if (showRange)
                {
                    _seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
                }
            }

            range /= 2;
            target = null;

            if (can_target)
            {
                if (playerAtXY > 0)
                {
                    target = gbl.player_array[playerAtXY];
                }
                else if (groundTile == gbl.Tile_DownPlayer)
                {
                    var c = gbl.downedPlayers.Find(cell => cell.map == pos);
                    if (c != null && c.target != null)
                    {
                        target = c.target;
                    }
                }
            }

            if (target != null)
            {
                gbl.display_hitpoints_ac = true;
                _ovr025.CombatDisplayPlayerSummary(target);
            }
            else
            {
                _seg037.draw8x8_clear_area(TextRegion.CombatSummary);
            }

            if (range > maxRange ||
                gbl.BackGroundTiles[groundTile].move_cost == 0xff)
            {
                can_target = false;
            }

            if (target != null)
            {
                if (CanSeeTargetA(target, player01) == false ||
                    allowTarget == false)
                {
                    can_target = false;
                }

                if (showRange)
                {
                    if (player01 == target ||
                        (playerAtXY == 0 && groundTile == 0x1f))
                    {
                        can_target = false;
                    }
                    else if (_ovr025.is_weapon_ranged(player01) == true &&
                             (_ovr025.GetCurrentAttackItem(out dummyItem, player01) == false ||
                              (_ovr025.BuildNearTargets(1, player01).Count > 0 &&
                               _ovr025.is_weapon_ranged_melee(player01) == false)))
                    {
                        can_target = false;
                    }
                }
            }
            else if (canTargetEmptyGround == false)
            {
                can_target = false;
            }

            string text = "Center Exit";

            if (can_target)
            {
                text = "Target " + text;
            }

            input_key = _ovr027.displayInput(false, 1, gbl.defaultMenuColors, text, "(Use Cursor keys) ");

            switch (input_key)
            {
                case '\r':
                case 'T':
                    gbl.mapToBackGroundTile.drawTargetCursor = false;

                    if (can_target)
                    {
                        arg_0.map = pos;

                        if (target != null)
                        {
                            arg_0.target = target;
                        }
                        else
                        {
                            arg_0.target = null;
                        }

                        if (showRange)
                        {
                            arg_4 = sub_411D8(arg_0, showRange, arg_0.target, player01);
                        }
                        else
                        {
                            arg_4 = true;
                        }
                    }

                    if (can_target == false ||
                        arg_4 == false)
                    {
                        _ovr033.RedrawPosition(pos);
                        arg_4 = false;
                        arg_0.Clear();
                    }
                    break;

                case 'H':
                    dir = 0;
                    break;

                case 'I':
                    dir = 1;
                    break;

                case 'M':
                    dir = 2;
                    break;

                case 'Q':
                    dir = 3;
                    break;

                case 'P':
                    dir = 4;
                    break;

                case 'O':
                    dir = 5;
                    break;

                case 'K':
                    dir = 6;
                    break;

                case 'G':
                    dir = 7;
                    break;

                case '\0':
                case 'E':
                    _ovr033.RedrawPosition(pos);
                    arg_0.Clear();
                    arg_4 = false;
                    break;

                case 'C':
                    _ovr033.redrawCombatArea(8, 0, pos);
                    dir = 8;
                    break;

                default:
                    dir = 8;
                    break;
            }
        }

        return arg_4;
    }


    internal SortedCombatant[] copy_sorted_players(Player player) /* sub_4188F */
    {
        var scl = _ovr032.Rebuild_SortedCombatantList(player, 0x7F, p => true);

        return scl.ToArray();
    }


    internal Player step_combat_list(bool arg_2, int step, ref int list_index, ref Point attackerPos, SortedCombatant[] sorted_list) /* sub_41932 */
    {
        if (arg_2 == true)
        {
            attackerPos = sorted_list[list_index - 1].pos;
        }
        else
        {
            _ovr033.RedrawPosition(attackerPos);
        }

        list_index += step;

        if (list_index == 0)
        {
            list_index = sorted_list.GetLength(0);
        }

        if (list_index > sorted_list.GetLength(0))
        {
            list_index = 1;
        }

        Player newTarget = sorted_list[list_index - 1].player;

        var targetPos = sorted_list[list_index - 1].pos;

        if (arg_2 == true)
        {
            _ovr025.draw_missile_attack(0, 1, targetPos, attackerPos);
            attackerPos = targetPos;
        }

        return newTarget;
    }

    private Set unk_41AE5 = new Set(0, 69);
    private Set unk_41B05 = new Set(71, 72, 73, 75, 77, 79, 80, 81);


    internal bool aim_menu(DownedPlayerTile arg_0, bool allowTarget, bool canTargetEmptyGround, bool showRange, int maxRange, Player attacker) /* sub_41B25 */
    {
        Player target; /* var_E5 */

        _ovr025.load_missile_dax(false, 0, 0, 0x19);

        arg_0.Clear();

        bool arg_4 = false;

        if (maxRange == -1 || maxRange == 0xff)
        {
            if (attacker.activeItems.primaryWeapon != null)
            {
                maxRange = gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type].range - 1;
            }
            else
            {
                maxRange = 1;
            }
        }

        if (maxRange == 0 ||
            maxRange == -1 || maxRange == 0xff)
        {
            maxRange = 1;
        }

        SortedCombatant[] sorted_list = copy_sorted_players(attacker);

        int list_index = 1;
        int next_prev_step = 0;
        int target_step = 0;

        Point attackerPos = new Point();

        target = step_combat_list(true, next_prev_step, ref list_index, ref attackerPos, sorted_list);

        next_prev_step = 1;
        char input = ' ';

        while (arg_4 == false && unk_41AE5.MemberOf(input) == false)
        {
            if (CanSeeTargetA(target, attacker) == false)
            {
                target = step_combat_list(false, next_prev_step, ref list_index, ref attackerPos, sorted_list);
            }
            else
            {
                input = aim_sub_menu(allowTarget, showRange, maxRange, target, attacker);

                if (gbl.displayInput_specialKeyPressed == false)
                {
                    switch (input)
                    {
                        case 'N':
                            next_prev_step = 1;
                            target_step = 1;
                            break;

                        case 'P':
                            next_prev_step = -1;
                            target_step = -1;
                            break;

                        case 'M':
                        case 'H':
                        case 'K':
                        case 'G':
                        case 'O':
                        case 'Q':
                        case 'I':
                            arg_4 = Target(arg_0, allowTarget, canTargetEmptyGround, showRange, maxRange, target, attacker);
                            _ovr025.load_missile_dax(false, 0, 0, 0x19);

                            sorted_list = copy_sorted_players(attacker);
                            target_step = 0;
                            break;

                        case 'T':
                            arg_4 = sub_411D8(arg_0, showRange, target, attacker);
                            _ovr025.load_missile_dax(false, 0, 0, 0x19);

                            sorted_list = copy_sorted_players(attacker);
                            target_step = 0;
                            break;

                        case 'C':
                            _ovr033.redrawCombatArea(8, 0, _ovr033.PlayerMapPos(target));
                            target_step = 0;
                            break;
                    }
                }
                else if (unk_41B05.MemberOf(input) == true)
                {
                    arg_4 = Target(arg_0, allowTarget, canTargetEmptyGround, showRange, maxRange, target, attacker);
                    _ovr025.load_missile_dax(false, 0, 0, 0x19);
                    sorted_list = copy_sorted_players(attacker);
                    target_step = 0;
                }

                _ovr033.RedrawPosition(_ovr033.PlayerMapPos(target));

                target = step_combat_list((arg_4 == false && unk_41AE5.MemberOf(input) == false), target_step,
                    ref list_index, ref attackerPos, sorted_list);
            }
        }

        if (showRange)
        {
            _seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
        }

        return arg_4;
    }


    internal bool find_target(bool clear_target, byte arg_2, int max_range, Player player) /* sub_41E44 */
    {
        bool target_found = false;

        Player target = player.actions.target;

        if (clear_target == true ||
            (target != null &&
             (target.combat_team == player.combat_team ||
              target.in_combat == false ||
              CanSeeTargetA(target, player) == false)))
        {
            player.actions.target = null;
        }

        if (player.actions.target != null)
        {
            target_found = true;
        }

        bool secondPass = false;
        bool var_5 = false;
        while (target_found == false && var_5 == false)
        {
            var_5 = secondPass;

            if (secondPass == true && clear_target == false)
            {
                gbl.mapToBackGroundTile.ignoreWalls = true;
            }

            int tryCount = 20;
            var nearTargets = _ovr025.BuildNearTargets(max_range, player);

            while (tryCount > 0 && target_found == false && nearTargets.Count > 0)
            {
                tryCount--;
                int roll = _ovr024.roll_dice(nearTargets.Count, 1);

                var epi = nearTargets[roll - 1];
                target = epi.player;

                if ((arg_2 != 0 && gbl.mapToBackGroundTile.ignoreWalls == true) ||
                    CanSeeTargetA(target, player) == true)
                {
                    target_found = true;
                    player.actions.target = target;
                }
                else
                {
                    nearTargets.Remove(epi);
                }
            }

            if (secondPass == false)
            {
                secondPass = true;
            }
        }

        gbl.mapToBackGroundTile.ignoreWalls = false;

        return target_found;
    }


    internal void engulfs(Effect arg_0, object param, Player attacker)
    {
        Player target = attacker.actions.target;

        if (gbl.bytes_1D2C9[1] == 2 &&
            target.in_combat == true &&
            target.HasAffect(Affects.clear_movement) == false &&
            target.HasAffect(Affects.reduce) == false)
        {
            target = attacker.actions.target;
            _ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + target.name, attacker);
            _ovr024.add_affect(false, _ovr033.GetPlayerIndex(target), 0, Affects.clear_movement, target);

            _ovr013.CallAffectTable(Effect.Add, null, target, Affects.clear_movement);
            _ovr024.add_affect(false, _ovr024.roll_dice(4, 2), 0, Affects.reduce, target);
            _ovr024.add_affect(true, _ovr033.GetPlayerIndex(target), 0, Affects.affect_8b, attacker);
        }
    }


    internal void LoadMissleIconAndDraw(int icon_id, Player target, Player attacker) //sub_42159
    {
        _ovr025.load_missile_icons(icon_id + 13);

        _ovr025.draw_missile_attack(0x1E, 1, _ovr033.PlayerMapPos(target), _ovr033.PlayerMapPos(attacker));
    }


    internal bool sub_421C1(bool clear_target, ref int range, Player player) // sub_421C1
    {
        bool var_5 = true;

        if (find_target(clear_target, 0, 0xff, player) == true)
        {
            var target = _ovr033.PlayerMapPos(player.actions.target);

            if (_ovr032.canReachTarget(ref range, target, _ovr033.PlayerMapPos(player)) == true)
            {
                var_5 = false;
            }
        }

        return var_5;
    }


    internal void attack_or_kill(Effect arg_0, object param, Player attacker)
    {
        int range = 0xFF; /* simeon */

        byte attacksTired = 0;
        int attackTiresLeft = 4;

        attacker.actions.target = null;
        sub_421C1(true, ref range, attacker);

        do
        {
            Player target = attacker.actions.target;

            range = _ovr025.getTargetRange(target, attacker);
            attackTiresLeft--;

            if (target != null)
            {
                if (range == 2 && (attacksTired & 1) == 0)
                {
                    attacksTired |= 1;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", attacker);
                    LoadMissleIconAndDraw(5, target, attacker);

                    if (_ovr024.RollSavingThrow(0, SaveVerseType.BreathWeapon, target) == false)
                    {
                        _ovr024.KillPlayer("is disintergrated", Status.gone, target);
                    }

                    sub_421C1(false, ref range, attacker);
                }
                else if (range == 3 && (attacksTired & 2) == 0)
                {
                    attacksTired |= 2;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", attacker);
                    LoadMissleIconAndDraw(10, target, attacker);

                    if (_ovr024.RollSavingThrow(0, SaveVerseType.Petrification, target) == false)
                    {
                        _ovr024.KillPlayer("is Stoned", Status.stoned, target);
                    }

                    sub_421C1(false, ref range, attacker);
                }
                else if (range == 4 && (attacksTired & 4) == 0)
                {
                    attacksTired |= 4;

                    _ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", attacker);
                    LoadMissleIconAndDraw(5, target, attacker);

                    if (_ovr024.RollSavingThrow(0, 0, target) == false)
                    {
                        _ovr024.KillPlayer("is killed", Status.dead, target);
                    }

                    sub_421C1(false, ref range, attacker);
                }
                else if (range == 5 && (attacksTired & 8) == 0)
                {
                    attacksTired |= 8;

                    _ovr025.DisplayPlayerStatusString(true, 10, "wounds you", attacker);
                    LoadMissleIconAndDraw(5, target, attacker);

                    _ovr024.damage_person(false, 0, _ovr024.roll_dice_save(8, 2) + 1, target);
                    sub_421C1(false, ref range, attacker);
                }
                else if ((attacksTired & 0x10) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x54);
                    attacksTired |= 0x10;
                }
                else if ((attacksTired & 0x20) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x37);
                    attacksTired |= 0x20;
                }
                else if ((attacksTired & 0x40) == 0)
                {
                    _ovr023.sub_5D2E1(true, QuickFight.True, 0x15);
                    attacksTired |= 0x40;
                }
            }
        } while (attackTiresLeft > 0 && attacker.actions.target != null);
    }


    internal void sub_425C6(Effect add_remove, object param, Player player)
    {
        Affect affect = (Affect)param;

        gbl.spell_target = gbl.player_array[affect.affect_data];

        if (add_remove == Effect.Remove ||
            player.in_combat == false ||
            gbl.spell_target.in_combat == false)
        {
            _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            _ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);

            if (add_remove == Effect.Add)
            {
                affect.callAffectTable = false;

                _ovr024.remove_affect(affect, Affects.affect_8b, player);
            }
        }
        else
        {
            player.attack1_AttacksLeft = 2;
            player.attack2_AttacksLeft = 0;
            player.attack1_DiceCount = 2;
            player.attack1_DiceSize = 8;

            AttackTarget(null, 1, gbl.spell_target, player);

            _ovr025.clear_actions(player);

            if (gbl.spell_target.in_combat == false)
            {
                _ovr024.remove_affect(null, Affects.affect_8b, player);
                _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                _ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);
            }
        }
    }


    internal void AffectOwlbearHugRoundAttack(Effect arg_0, object param, Player player) // sub_426FC
    {
        Affect affect = (Affect)param;

        gbl.spell_target = gbl.player_array[affect.affect_data];

        if (arg_0 == Effect.Remove ||
            player.in_combat == false ||
            gbl.spell_target.in_combat == false)
        {
            _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            if (arg_0 == Effect.Add)
            {
                affect.callAffectTable = false;
                _ovr024.remove_affect(affect, Affects.owlbear_hug_round_attack, player);
            }
        }
        else
        {
            player.attack1_AttacksLeft = 1;
            player.attack2_AttacksLeft = 0;
            player.attack1_DiceCount = 2;
            player.attack1_DiceSize = 8;


            AttackTarget(null, 2, gbl.spell_target, player);

            _ovr025.clear_actions(player);

            if (gbl.spell_target.in_combat == false)
            {
                _ovr024.remove_affect(null, Affects.owlbear_hug_round_attack, player);
                _ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
            }
        }
    }


    internal void AffectOwlbearHugAttackCheck(Effect arg_0, object param, Player player) // hugs
    {
        if (gbl.attack_roll >= 18)
        {
            gbl.spell_target = player.actions.target;
            _ovr025.DisplayPlayerStatusString(true, 12, "hugs " + gbl.spell_target.name, player);

            _ovr024.add_affect(false, _ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.clear_movement, gbl.spell_target);
            _ovr013.CallAffectTable(Effect.Add, null, gbl.spell_target, Affects.clear_movement);

            _ovr024.add_affect(true, _ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.owlbear_hug_round_attack, player);
        }
    }


    internal bool god_intervene()
    {
        bool intervened = false;

        if (Cheats.allow_gods_intervene)
        {
            intervened = true;
            _ovr025.string_print01("The Gods intervene!");

            foreach (Player player in gbl.TeamList)
            {
                if (player.combat_team == CombatTeam.Enemy)
                {
                    player.in_combat = false;
                    player.health_status = Status.dead;

                    gbl.CombatMap[_ovr033.GetPlayerIndex(player)].size = 0;
                }

                _ovr025.clear_actions(player);
            }

            _ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);
        }

        return intervened;
    }
}
