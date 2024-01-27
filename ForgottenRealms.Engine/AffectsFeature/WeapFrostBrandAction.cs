using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class WeapFrostBrandAction : IAffectAction
{
    public Affects ActionForAffect => Affects.weap_frost_brand;
    public void Execute(Effect effect, object param, Player player)
    {
    }
}
