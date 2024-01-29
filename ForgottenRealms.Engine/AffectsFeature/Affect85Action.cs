using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect85Action : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_85;

    private readonly AffectsProtectedAction _affectsProtectedAction;
    public Affect85Action(AffectsProtectedAction affectsProtectedAction)
    {
        _affectsProtectedAction = affectsProtectedAction;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        _affectsProtectedAction.ProtectedIf(Affects.fear);
        _affectsProtectedAction.ProtectedIf(Affects.ray_of_enfeeblement);
        _affectsProtectedAction.ProtectedIf(Affects.feeblemind);

        if ((gbl.damage_flags & DamageType.Electricity) != 0)
        {
            _affectsProtectedAction.Protected();
        }
    }
}
