using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AditroProductMangement.Core.Products
{
    public class ProductFacade<T> : IProductFacade<T>
    {
        public IList<T> GetImportedProducts()
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "internal", "products.json");
            var stringData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(stringData);
        }
    }
}
