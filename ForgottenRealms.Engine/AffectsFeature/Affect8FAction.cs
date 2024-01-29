using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class Affect8FAction : IAffectAction
{
    public Affects ActionForAffect => Affects.affect_8f;

    private readonly ovr024 _ovr024;
    private readonly ovr025 _ovr025;
    public Affect8FAction(ovr024 ovr024, ovr025 ovr025)
    {
        _ovr024 = ovr024;
        _ovr025 = ovr025;
    }

    public void Execute(Effect effect, object param, Player player)
    {
        if (_ovr025.getTargetRange(player, gbl.SelectedPlayer) < 2)
        {
            int bkup_damage = gbl.damage;
            DamageType bkup_damage_flags = gbl.damage_flags;

            gbl.damage *= 2;
            gbl.damage_flags = DamageType.Magic;

            _ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.SelectedPlayer);

            _ovr024.damage_person(false, 0, gbl.damage, gbl.SelectedPlayer);
            gbl.damage = bkup_damage;
            gbl.damage_flags = bkup_damage_flags;
        }
    }
}
