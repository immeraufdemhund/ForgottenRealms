using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect6FAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_6f;

    private readonly AffectsProtectedAction _affectsProtectedAction;
    public Affect6FAction(AffectsProtectedAction affectsProtectedAction)
    {
        _affectsProtectedAction = affectsProtectedAction;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        _affectsProtectedAction.ProtectedIf(Affects.poisoned);
        _affectsProtectedAction.ProtectedIf(Affects.paralyze);

        if (gbl.saveVerseType == SaveVerseType.Poison)
        {
            gbl.savingThrowRoll = 100;
        }
    }
}
