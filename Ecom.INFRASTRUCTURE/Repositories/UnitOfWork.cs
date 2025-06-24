using AutoMapper;
using Ecom.CORE.Interfaces;
using Ecom.CORE.Services;
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
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;
        public UnitOfWork(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService)
        {
            _context = context;
            CategoryRepositiry = new CategoryRepositiry(context);
            ProductRepositiry = new ProductRepositiry(_context, mapper, imageManagementService);
            PhotoRepositiry = new PhotoRepositiry(context);
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public ICategoryRepositiry CategoryRepositiry { get; }
        public IPhotoRepositiry PhotoRepositiry { get; }
        public IProductRepositiry ProductRepositiry { get; }
    }
}
