using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1.PriceDTOs;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Prices
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PriceDTOMapper _mapper = new PriceDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Prices
        /// <summary>
        /// Get all Prices
        /// </summary>
        /// <returns>Array of Prices</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PriceDTO>))]
        public async Task<ActionResult<IEnumerable<PriceDTO>>> GetPrices()
        {
            return Ok((await _bll.Prices.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Prices/5
        /// <summary>
        /// Get single Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>PriceDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PriceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PriceDTO>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound(new MessageDTO($"Price with id {id} not found"));
            }

            return Ok(_mapper.Map(price));
        }

        // PUT: api/Prices/5
        /// <summary>
        /// Update the Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="priceDTO">PriceDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutPrice(Guid id, PriceDTO priceDTO)
        {
            if (id != priceDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and priceEditDTO.id do not match"));
            }
            
            if (!await _bll.Prices.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Price with this id {id} not found"));
            }

            priceDTO.AppUserId = User.UserGuidId();
            await _bll.Prices.UpdateAsync(_mapper.Map(priceDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Prices
        /// <summary>
        /// Post the new Price
        /// </summary>
        /// <param name="priceDTO">PriceDTO object</param>
        /// <returns>Created Price object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PriceDTO))]
        public async Task<ActionResult<PriceDTO>> PostPrice(PriceDTO priceDTO)
        {
            priceDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(priceDTO);
            _bll.Prices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            priceDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetPrice", new { id = priceDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, priceDTO);
        }

        // DELETE: api/Prices/5
        /// <summary>
        /// Delete the Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>Deleted Price object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PriceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PriceDTO>> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (price == null)
            {
                return NotFound(new MessageDTO("Price not found"));
            }

            await _bll.Prices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            

            return Ok(_mapper.Map(price));
        }

    }
}
