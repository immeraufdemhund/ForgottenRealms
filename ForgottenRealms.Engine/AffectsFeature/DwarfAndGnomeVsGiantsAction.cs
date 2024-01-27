using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class DwarfAndGnomeVsGiantsAction : IAffectAction
{
    public Affects ActionForAffect => Affects.dwarf_and_gnome_vs_giants;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
