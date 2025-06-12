using Ecom.CORE.Interfaces;
using Ecom.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.INFRASTRUCTURE.Repositories
{
    public  class UnitOfWork :IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            CategoryRepositiry= new CategoryRepositiry(context);
            ProductRepositiry= new ProductRepositiry(context);
            PhotoRepositiry= new PhotoRepositiry(context);  
        }

        public ICategoryRepositiry CategoryRepositiry { get; }
        public IPhotoRepositiry PhotoRepositiry { get; }
        public IProductRepositiry ProductRepositiry { get; }
    }
}
