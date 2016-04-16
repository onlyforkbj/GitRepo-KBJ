using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AditroProductManagementPortal.Models
{
    public class ImportProducts<T>
    {
        //public static T Deserialize(string json)
        //{
        //    var s = new JsonSerializer();
        //    return s.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        //}

        public void LoadJson()
        {
            using (var r = new StreamReader(File.ReadAllText(@"/internal/Products.json")))
            {
                var json = r.ReadToEnd();
                var productsList = JsonConvert.DeserializeObject<List<ProductModel>>(json);

            }
        }

        public IList<T> GetImportedProducts()
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "internal", "products.json");
            var stringData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(stringData);

            //using (var r = new StreamReader(stringData))
            //{
            //    var json = r.ReadToEnd();
            //    return JsonConvert.DeserializeObject<List<T>>(json);
            //}
        }
    }
}