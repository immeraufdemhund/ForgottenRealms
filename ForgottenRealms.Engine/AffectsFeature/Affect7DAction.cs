using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect7DAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_7d;

    private readonly AffectsProtectedAction _affectsProtectedAction;
    public Affect7DAction(AffectsProtectedAction affectsProtectedAction)
    {
        _affectsProtectedAction = affectsProtectedAction;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        _affectsProtectedAction.ProtectedIf(Affects.charm_person);
        _affectsProtectedAction.ProtectedIf(Affects.sleep);
        _affectsProtectedAction.ProtectedIf(Affects.paralyze);
        _affectsProtectedAction.ProtectedIf(Affects.poisoned);

        if (gbl.saveVerseType != SaveVerseType.Poison)
        {
            gbl.savingThrowRoll = 100;
        }
    }
}
