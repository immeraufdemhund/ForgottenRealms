using ForgottenRealms.Engine.CharacterFeature;
using ForgottenRealms.Engine.Classes.DaxFiles;
using ForgottenRealms.Engine.CommandsFeature;
using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ForgottenRealms.Engine;

public static class FeatureModule
{
    public static IServiceCollection RegisterEngineFeature(this IServiceCollection services)
    {
        return services
            .RegisterCommandsFeature()
            .RegisterCharacterFeature()
            .AddSingleton<Config>()
            .AddSingleton<DisplayDriver>()
            .AddSingleton<SoundDriver>()
            .AddSingleton<KeyboardDriver>()
            .AddSingleton<MainGameEngine>()
            .AddSingleton<ovr038>()
            .AddTransient<AddPlayerAction>()
            .AddTransient<DaxFileDecoder>()
            .AddTransient<DaxBlockReader>()
            .AddTransient<DrawPictureAction>()
            .AddTransient<GameFileLoader>()
            .AddTransient<KeyboardService>()
            .AddTransient<MapCursor>()
            .AddTransient<TitleScreenAction>()
            .AddSingleton<ovr003>()
            .AddTransient<ovr004>()
            .AddTransient<ovr006>()
            .AddTransient<ovr007>()
            .AddTransient<ovr008>()
            .AddTransient<ovr009>()
            .AddTransient<ovr010>()
            .AddTransient<ovr011>()
            .AddTransient<ovr013>()
            .AddTransient<ovr014>()
            .AddTransient<ovr015>()
            .AddTransient<ovr016>()
            .AddTransient<ovr017>()
            .AddTransient<ovr018>()
            .AddTransient<ovr019>()
            .AddTransient<ovr020>()
            .AddTransient<ovr021>()
            .AddTransient<ovr022>()
            .AddTransient<ovr023>()
            .AddTransient<ovr024>()
            .AddTransient<ovr025>()
            .AddTransient<ovr026>()
            .AddTransient<ovr027>()
            .AddTransient<ovr029>()
            .AddTransient<ovr030>()
            .AddTransient<ovr031>()
            .AddTransient<ovr032>()
            .AddTransient<ovr033>()
            .AddTransient<ovr034>()
            .AddTransient<seg037>()
            .AddTransient<seg040>()
            .AddTransient<seg042>()
            .AddTransient<seg051>()
            .AddTransient<BackStabMath>()
            .AddTransient<CanSeeTargetMath>()
            .AddTransient<FindTargetMath>()
            .AddTransient<TargetDirectionMath>()
            .AddTransient<AreaDamageTargetsBuilder>()
            .AddTransient<PlayerPrimaryWeapon>()
            .AddTransient<AffectsProtectedAction>()
            .AddTransient<AvoidMissleAttackAction>()
            .AddTransient<ElectricalDamageMath>()
            .AddTransient<Subroutine5FA44>()
            .AddTransient<ApplyAffectTable>()
            ;
    }
}
