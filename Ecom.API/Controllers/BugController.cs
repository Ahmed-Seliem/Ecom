﻿using AutoMapper;
using Ecom.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("Not-Found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await work.CategoryRepositiry.GetByIdAsync(100);
            if (category == null)  return NotFound(); 
            return Ok(category);
        }

        [HttpGet("Server-Error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await work.CategoryRepositiry.GetByIdAsync(100);
            category.Name = "";
            return Ok(category);
        }

        [HttpGet("Bad-Request/{id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {
           
            return Ok();
        }

        [HttpGet("Bad-Request/")]
        public async Task<ActionResult> GetBadRequest()
        {

            return BadRequest();
        }
    }
}
