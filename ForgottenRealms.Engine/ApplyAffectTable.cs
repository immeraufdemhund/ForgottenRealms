using System.Collections.Generic;
using System.Linq;
using ForgottenRealms.Engine.AffectsFeature;
using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine;

public class ApplyAffectTable
{
    private readonly Dictionary<Affects, IAffectAction> _table;

    public ApplyAffectTable(IEnumerable<IAffectAction> affectActions)
    {
        _table = affectActions.ToDictionary(x => x.ActionForAffect);
    }

    internal void CallAffectTable(Effect add_remove, object parameter, Player player, Affects affect)
    {
        if (gbl.applyItemAffect == true)
        {
            affect = Affects.do_items_affect;
        }

        if (_table.TryGetValue(affect, out var func))
        {
            func.Execute(add_remove, parameter, player);
        }
    }
}
