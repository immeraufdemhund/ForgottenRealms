
using Microsoft.Extensions.DependencyInjection;

namespace ForgottenRealms.Engine.CharacterFeature;

public static class FeatureModule
{
    public static IServiceCollection RegisterCharacterFeature(this IServiceCollection services)
    {
        return services
            .AddTransient<CreatePlayerFeature.CreatePlayerService>()
            .AddTransient<CreatePlayerFeature.IconBuilder>()
            .AddTransient<DropCharacterFeature.DropCharacterService>()
            .AddTransient<ModifyCharacterFeature.ModifyCharacterService>()
            .AddTransient<TrainCharacterFeature.TrainCharacterService>()
            .AddTransient<ConstitutionHitPointsAdjustmentTable>()
            .AddTransient<ExperienceTable>()
            .AddTransient<HitPointTable>()
            .AddTransient<Thac0Table>();
    }
}
