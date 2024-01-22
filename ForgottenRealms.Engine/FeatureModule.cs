using ForgottenRealms.Engine.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ForgottenRealms.Engine;

public static class FeatureModule
{
    public static IServiceCollection RegisterEngineFeature(this IServiceCollection services)
    {
        return services
            .AddSingleton<Config>();
    }
}
