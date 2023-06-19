using DataEngine.Abstraction.Interfaces;
using DataEngine.Core.DataBlocks.Interfaces;
using DataEngine.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace DataEngine.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<ITaskManagement, TaskManagementService>();
        services.AddSingleton<IFileSystem, FileSystemService>();
        services.AddScoped<IDataPipeline, DataPipelineService>();
        services.AddScoped<IDataFlowFactory, DataFlowFactory>();
        
        return services.RegiesterDataBlocks();
    }

    public static IServiceCollection RegiesterDataBlocks(this IServiceCollection services) 
    {
        var definedTypes = Assembly.Load("DataEngine.Core").DefinedTypes;

        var interfaces = GetInterfaces(definedTypes);

        var dic = new Dictionary<string, Type>();

        foreach (var type in interfaces) 
        {
            var implementation = definedTypes.GetImplementation(type);

            if (!implementation.IsInterface)
            {
                dic.Add(implementation.Name, type);

                services.AddScoped(type, implementation);
            }
        }

        File.WriteAllText(Path.Combine("data", "data-blocks.json"), JsonConvert.SerializeObject(dic));

        return services;
    }

    private static Type GetImplementation(this IEnumerable<TypeInfo> definedTypes, Type type) 
    {
        return definedTypes.FirstOrDefault(p => type.IsAssignableFrom(p));
    }

    private static IEnumerable<Type> GetInterfaces(IEnumerable<TypeInfo> definedTypes)
    {
        var type = typeof(IDataBlock);
        
        return definedTypes.Where(p => type.IsAssignableFrom(p) && p != type && p.IsInterface);
    }
}
