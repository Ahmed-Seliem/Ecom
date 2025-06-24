using AutoMapper;
using Ecom.API.Helper;
using Ecom.CORE.DTO;
using Ecom.CORE.Entities.Product;
using Ecom.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> get()
        {
            try
            {
                var category = await work.CategoryRepositiry.GetAllAsync();
                if (category == null)
                    return BadRequest(new ResponseAPI(400, "Not Found") );
                return Ok(category);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.CategoryRepositiry.GetByIdAsync(id);
                if (category == null)
                    return BadRequest(new ResponseAPI(400 , "Not Found this Category"));
                return Ok(category);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> add(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepositiry.AddAsync(category);
                return Ok(new  ResponseAPI (200, "Category Item has been Added"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update-category")]
        public async Task<IActionResult> update(UpdateCategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                await work.CategoryRepositiry.UpdateAsync(category);

                return Ok(new ResponseAPI(200, "Category Item has been Updated"));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                await work.CategoryRepositiry.DeleteAsync(id);
                return Ok(new ResponseAPI(200, "Category Item has been Deleted"));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
