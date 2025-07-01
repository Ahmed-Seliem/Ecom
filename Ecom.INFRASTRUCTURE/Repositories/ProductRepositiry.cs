using AutoMapper;
using Ecom.CORE.DTO;
using Ecom.CORE.Entities.Product;
using Ecom.CORE.Interfaces;
using Ecom.CORE.Services;
using Ecom.CORE.Sharing;
using Ecom.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.INFRASTRUCTURE.Repositories
{
    public class ProductRepositiry : GenericRepositiry<Product>, IProductRepositiry
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly IImageManagementService imageManagementService;

        public ProductRepositiry(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync(ProductParams productParams)
        {
            var query = context.Products
                .Include(p => p.Category)
                .Include(p => p.Photos)
                .AsNoTracking();

            //filtering by Word
            if (!string.IsNullOrEmpty(productParams.Search))
            //query= query.Where(p=>p.Name.ToLower().Contains(productParams.Search.ToLower()) 
            // || 
            //  p.Description.ToLower().Contains(productParams.Search.ToLower()));
            {
                var searchOfWords=productParams.Search.Split(' ');
                query = query.Where(m => searchOfWords.All(word => 
                m.Name.ToLower().Contains(word.ToLower())||
                m.Description.ToLower().Contains(word.ToLower()) 
                ));
            }



            //filtering by Category
            if (productParams.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == productParams.CategoryId);
            }

            //sorting Product
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                query = productParams.Sort switch
                {
                    "PriceAce" => query.OrderBy(p => p.NewPrice),
                    "PriceDce" => query.OrderByDescending(p => p.NewPrice),
                    _ => query.OrderBy(p => p.Name),
                };
            }
            // Product Pagination 
            query= query.Skip((productParams.PageSize) *(productParams.PageNumber -1)).Take(productParams.PageSize);
            productParams.PageNumber = productParams.PageNumber > 0 ? productParams.PageNumber : 1;
            productParams.PageSize   = productParams.PageSize > 0 ? productParams.PageSize  : 3;

            var result = mapper.Map<List<ProductDTO>>(query);
            return result;
        }

        public async Task<bool> AddAsync(AddProductDTO productDto)
        {
            if (productDto == null) return false;

            var product= mapper.Map<Product>(productDto);     
             await context.Products.AddAsync(product);

            var ImagePaths = await imageManagementService.AddImageAsync(productDto.Photo, productDto.Name);
            var photos = ImagePaths.Select(path =>  new Photo
            {
                PhotoName = path,
                ProductId = product.Id,

            }).ToList();
            if (photos.Any())
            {
                await context.Photos.AddRangeAsync(photos);

            }               
            await context.SaveChangesAsync(); // Save photo records

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO == null) return false;

            var FindProduct= await context.Products.Include(m =>m.Category).Include(m=>m.Photos).FirstOrDefaultAsync(m=>m.Id == updateProductDTO.Id);

            if (FindProduct == null) return false;

            mapper.Map(updateProductDTO,FindProduct);  

            var FindPhoto= await context.Photos.Where(m=>m.ProductId == updateProductDTO.Id).ToListAsync();
            foreach (var item in FindPhoto)
            {
                imageManagementService.DeleteImageAsync(item.PhotoName);
            }
            context.Photos.RemoveRange(FindPhoto);

            var ImagePath= await imageManagementService.AddImageAsync(updateProductDTO.Photo,updateProductDTO.Name);

            var photo= ImagePath.Select(path => new Photo
            {
                PhotoName=path,
                ProductId=updateProductDTO.Id,
            }).ToList();

            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;

        }

        public async Task DeleteAsync(Product product)
        {
            var photo= await context.Photos.Where(x=>x.ProductId == product.Id).ToListAsync();
            foreach (var item in photo)
            {
                imageManagementService.DeleteImageAsync(item.PhotoName);
            }
            context.Products.Remove(product);
             await context.SaveChangesAsync();
        }
    }
}
