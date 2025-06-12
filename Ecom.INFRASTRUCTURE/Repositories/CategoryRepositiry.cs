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
    public class CategoryRepositiry : GenericRepositiry<Category>, ICategoryRepositiry
    {
        public CategoryRepositiry(AppDbContext context) : base(context)
        {
        }
    }
}
