using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class BackStabMath
{
    private readonly TargetDirectionMath _targetDirectionMath;
    public BackStabMath(TargetDirectionMath targetDirectionMath)
    {
        _targetDirectionMath = targetDirectionMath;
    }

    public bool CanBackStabTarget(Player target, Player attacker) /* sub_408D7 */
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
                    _targetDirectionMath.getTargetDirection(target, attacker) == target.actions.direction)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
