using AutoMapper;
using Ecom.API.Helper;
using Ecom.CORE.DTO;
using Ecom.CORE.Entities.Product;
using Ecom.CORE.Interfaces;
using Ecom.CORE.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get([FromQuery]ProductParams productParams)
        {
            try
            {
                  var product = await work.ProductRepositiry.GetAllAsync(productParams);

                  var totalCount= await work.ProductRepositiry.CountAsync();
                    return Ok(new Pagination<ProductDTO>(productParams.PageNumber,productParams.PageSize,totalCount,product) );

                return BadRequest(new ResponseAPI(400, "Products Not Found"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Get-By-Id/{id}")]
        public async  Task<IActionResult> getById(int id)
        {
            try
            {
                var product= await work.ProductRepositiry .GetByIdAsync(id ,x=>x.Category, x=>x.Photos);
                var result=mapper.Map<ProductDTO>(product);
                if(product != null)
                {
                    return Ok(result);
                }
                return BadRequest(new ResponseAPI(400, "This Product Not Found"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add-Product")]
        public async Task<IActionResult> add(AddProductDTO productDTO) 
        {
            try
            {
                await work.ProductRepositiry.AddAsync(productDTO);

            }
            catch (Exception ex)
            {
                // This will show the real database error
                var errorMessage = ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;
                return BadRequest(errorMessage);
            }
            return Ok(new ResponseAPI(200, "Product Added Successfully"));

        }

        [HttpPut("Update-Product")]
        public async Task<IActionResult> update(UpdateProductDTO updateProductDTO)
        {
            try
            {
                await work.ProductRepositiry.UpdateAsync(updateProductDTO);
                return Ok(new ResponseAPI(200, "Product Updated Successfully"));
            }
            catch ( Exception ex)
            {
               return BadRequest(new ResponseAPI(400, ex.Message));
            }
     
        }


        [HttpDelete("Delete-product/{Id}")]
        public async Task<IActionResult> delete(int Id) 
        {
            try
            {
                var product = await work.ProductRepositiry.GetByIdAsync(Id, x => x.Photos, x => x.Category);
                await work.ProductRepositiry.DeleteAsync(product);
                return Ok(new ResponseAPI(200, "Product Deleted Successfully"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, ex.Message));
            }
        }


    }
}
