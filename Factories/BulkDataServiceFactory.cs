using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Carrental.Factories
{
    public class BulkDataServiceFactory : IBulkDataServiceFactory
    {
        private readonly Dictionary<string, IBulkOperation> _services
           = new Dictionary<string, IBulkOperation>(StringComparer.OrdinalIgnoreCase);

        public BulkDataServiceFactory(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                _services.Add("type", scope.ServiceProvider.GetService<CarTypeBulkOperation>());
                _services.Add("car", scope.ServiceProvider.GetService<CarBulkOperation>());
                _services.Add("order", scope.ServiceProvider.GetService<OrderBulkOperation>());
            }
        }

        public IBulkOperation Add(string typeName)
        {
            return _services.ContainsKey(typeName) ? _services[typeName] : _services["type"];
        }
    }

}
