using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RedBot.Startup
{
    public class DependencyInjection
    {
        public static void AddServices(IServiceCollection services)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            AssemblyName[] assemblyNames = entryAssembly.GetReferencedAssemblies();
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(entryAssembly);
            foreach(AssemblyName assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }

            foreach (Assembly assembly in assemblies)
            {
                Type[] serviceTypes = assembly.GetTypes()
                    .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                    .ToArray();
                foreach (Type serviceType in serviceTypes)
                {
                    Type[] interfaces = serviceType.GetInterfaces()
                        .Where(i => i.Name.EndsWith("Service"))
                        .ToArray();
                    foreach(Type interfaceType in interfaces)
                    {
                        services.Add(new ServiceDescriptor(interfaceType, serviceType, ServiceLifetime.Scoped));
                    }
                }
            }
        }
    }
}
