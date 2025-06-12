using Ecom.CORE.Entities.Product;
using Ecom.CORE.Interfaces;
using Ecom.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.INFRASTRUCTURE.Repositories
{
    public class ProductRepositiry : GenericRepositiry<Product>, IProductRepositiry
    {
        public ProductRepositiry(AppDbContext context) : base(context)
        {
        }
    }
}
