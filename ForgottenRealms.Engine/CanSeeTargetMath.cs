using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class CanSeeTargetMath
{
    private readonly ovr024 _ovr024;
    public CanSeeTargetMath(ovr024 ovr024)
    {
        _ovr024 = ovr024;
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
}
