using GroupBy.Design.Services;
using GroupBy.Design.DTO.Document;
using GroupBy.Design.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GroupBy.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService documentService;

        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }
        [HttpGet("", Name = "GetAllDocuments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DocumentSimpleDTO>>> GetAllAsync(bool includeLocal = false)
        {
            return Ok(await documentService.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentDTO>> GetAsync(Guid id)
        {
            try
            {
                return Ok(await documentService.GetAsync(new DocumentDTO { Id = id }));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
        }

        [HttpPost("add", Name = "CreateNewDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentDTO>> CreateAsync([FromBody] DocumentCreateDTO document)
        {
            try
            {
                return Ok(await documentService.CreateAsync(document));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
        }

        [HttpPut("update", Name = "UpdateDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentDTO>> UpdateAsync([FromBody] DocumentUpdateDTO document)
        {
            try
            {
                return Ok(await documentService.UpdateAsync(document));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { Id = e.Key, e.Message });
            }
            catch (ValidationException e)
            {
                return BadRequest(e.ValidationErrors);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await documentService.DeleteAsync(new DocumentDTO { Id = id });
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
