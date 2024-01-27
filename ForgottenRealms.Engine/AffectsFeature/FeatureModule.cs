using Microsoft.Extensions.DependencyInjection;

namespace ForgottenRealms.Engine.AffectsFeature;

public static class FeatureModule
{
    public static IServiceCollection RegisterAffectsFeature(this IServiceCollection services)
    {
        return services;
    }
}
