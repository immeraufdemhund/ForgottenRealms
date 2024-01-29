using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class AffectsProtectedAction
{
    /// <summary>
    /// If same as current affect damage set to zero, or if affect is zero
    /// </summary>
    public void ProtectedIf(Affects affect)
    {
        if (gbl.current_affect == affect)
        {
            Protected();
        }
    }

    public void Protected()
    {
        gbl.damage = 0;
        gbl.current_affect = 0;
    }
}
