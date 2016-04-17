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
            return LoadJSon(jsonFilePath);
        }

        public IList<T> UploadProuctCatalogue(string filePath)
        {
            return LoadJSon(filePath);
        }

        private static IList<T> LoadJSon(string filePath)
        {
            var stringData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(stringData);
        }
    }
}
