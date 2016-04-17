using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AditroProductMangement.Core.Products
{
    public interface IProductFacade<T>
    {
        List<T> GetImportedProducts();
        List<T> UploadProuctCatalogue(string filePath);
    }
}
