using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1.ProductDescriptionDTOs;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Product descriptions
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProductDescriptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductDescriptionDTOMapper _mapper = new ProductDescriptionDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductDescriptionsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ProductDescriptions
        /// <summary>
        /// Get all product descriptions
        /// </summary>
        /// <returns>Array of product descriptions</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDescriptionDTO>))]
        public async Task<ActionResult<IEnumerable<ProductDescriptionDTO>>> GetProductDescriptions()
        {
            return Ok((await _bll.ProductDescriptions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ProductDescriptions/5
        /// <summary>
        /// Get single product description
        /// </summary>
        /// <param name="id">ProductDescription id</param>
        /// <returns>ProductDescriptionDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDescriptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ProductDescriptionDTO>> GetProductDescription(Guid id)
        {
            var productDescription = await _bll.ProductDescriptions.FirstOrDefaultAsync(id);

            if (productDescription == null)
            {
                return NotFound(new MessageDTO($"Product description with id {id} not found"));
            }

            return Ok(_mapper.Map(productDescription));
        }

        // PUT: api/ProductDescriptions/5
        /// <summary>
        /// Update the product description
        /// </summary>
        /// <param name="id">ProductDescription id</param>
        /// <param name="productDescriptionDTO">ProductDescriptionDTO object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutProductDescription(Guid id, ProductDescriptionDTO productDescriptionDTO)
        {
            if (id != productDescriptionDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and productDescriptionEditDTO.id do not match"));
            }
            
            if (!await _bll.ProductDescriptions.ExistsAsync(productDescriptionDTO.Id))
            {
                return NotFound(new MessageDTO($"Product description with this id {id} not found"));
            }

            await _bll.ProductDescriptions.UpdateAsync(_mapper.Map(productDescriptionDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductDescriptions
        /// <summary>
        /// Post the new ProductDescription
        /// </summary>
        /// <param name="productDescriptionDTO">ProductDescriptionDTO object</param>
        /// <returns>Created ProductDescription object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDescriptionDTO))]
        public async Task<ActionResult<ProductDescriptionDTO>> PostProductDescription(ProductDescriptionDTO productDescriptionDTO)
        {
            var bllEntity = _mapper.Map(productDescriptionDTO);
            _bll.ProductDescriptions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            productDescriptionDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetProductDescription", new { id = productDescriptionDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, productDescriptionDTO);
        }

        // DELETE: api/ProductDescriptions/5
        /// <summary>
        /// Delete the ProductDescription
        /// </summary>
        /// <param name="id">ProductDescription id</param>
        /// <returns>Deleted ProductDescription object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDescriptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ProductDescriptionDTO>> DeleteProductDescription(Guid id)
        {
            var productDescription = await _bll.ProductDescriptions.FirstOrDefaultAsync(id);
            if (productDescription == null)
            {
                return NotFound(new MessageDTO("ProductDescription not found"));
            }

            await _bll.ProductDescriptions.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(productDescription));
        }

    }
}
