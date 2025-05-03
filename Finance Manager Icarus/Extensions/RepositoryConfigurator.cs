using System.Reflection;

namespace Finance_Manager_Icarus.Extensions;

public static class RepositoryConfigurator
{
    public static void AddRepositories(IServiceCollection services, Assembly assembly, Type baseType)
    {
        var repositoryTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));

        foreach (var type in repositoryTypes)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Length > 0)
            {
                foreach (var interfaceType in interfaces)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
            else
            {
                services.AddScoped(type);
            }
        }
    }
}

