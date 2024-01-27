using ForgottenRealms.Engine.Classes;

namespace ForgottenRealms.Engine.AffectsFeature;

public class ElfResistSleepAction : IAffectAction
{
    public Affects ActionForAffect => Affects.elf_resist_sleep;
    public void Execute(Effect effect, object affect, Player player)
    {
    }
}
