using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DwarfVsOrcAction : IAffectAction
{
    public Affects ActionForAffect => Affects.dwarf_vs_orc;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
