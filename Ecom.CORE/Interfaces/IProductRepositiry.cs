using Ecom.CORE.DTO;
using Ecom.CORE.Entities.Product;
using Ecom.CORE.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.CORE.Interfaces
{
    public interface IProductRepositiry : IGenericRepositiry<Product>
    {
        Task<bool> AddAsync(AddProductDTO productDto);
        Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO);
        Task DeleteAsync(Product product);
        Task<ReturnProductDTO> GetAllAsync(ProductParams productParams);

    }
}
