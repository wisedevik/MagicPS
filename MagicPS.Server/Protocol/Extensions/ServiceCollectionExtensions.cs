using MagicPS.Server.Protocol.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MagicPS.Server.Protocol.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(t => t.GetCustomAttribute<ServiceNodeAttribute>() != null);

        foreach (Type type in types)
        {
            services.AddScoped(type);
        }

        return services;
    }
}
