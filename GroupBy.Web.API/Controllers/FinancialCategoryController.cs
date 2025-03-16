using GroupBy.Design.DTO.FinancialCategory;
using GroupBy.Design.Exceptions;
using GroupBy.Design.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class FinancialCategoryController : ControllerBase
    {
        private readonly IFinancialCategoryService financialCategoryService;

        public FinancialCategoryController(IFinancialCategoryService financialCategoryService)
        {
            this.financialCategoryService = financialCategoryService;
        }

        [HttpGet(Name = "GetAllFinancialCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FinancialCategoryDTO>>> GetAllAsync()
        {
            return Ok(await financialCategoryService.GetAllAsync());
        }
        [HttpGet("{id}", Name = "GetFinancialCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialCategoryDTO>> GetAsync(Guid id)
        {
            try
            {
                var financialCategory = await financialCategoryService.GetAsync(new FinancialCategoryDTO { Id = id });
                return Ok(financialCategory);
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }
        [HttpPost("add", Name = "AddFinancialCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FinancialCategoryDTO>> CreateAsync([FromBody] FinancialCategoryCreateDTO model)
        {
            try
            {
                var financialCategory = await financialCategoryService.CreateAsync(model);
                return Ok(financialCategory);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }
        [HttpDelete("delete/{id}", Name = "DeleteFinancialCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await financialCategoryService.DeleteAsync(new FinancialCategoryDTO { Id = id });

                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (DeleteNotPermittedException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}
