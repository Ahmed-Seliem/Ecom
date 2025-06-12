using AutoMapper;
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
                    return NotFound();
                return Ok(category);


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var category = await work.CategoryRepositiry.GetByIdAsync(id);
                if (category == null)
                    return NotFound();
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
                return Ok(new { message = "Category Item has been Added" });

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
                var category = mapper.Map<Category>(UpdateCategoryDTO);
                await work.CategoryRepositiry.UpdateAsync(category);

                return BadRequest(new { message = "Category Item Has been Updated" });
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
                return Ok(new {message ="Category Item has been deleted"});

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
