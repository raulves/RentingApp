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
using PublicApi.DTO.v1.ItemBookedDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Items booked
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemsBookedController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemBookedDTOMapper _mapper = new ItemBookedDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ItemsBookedController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ItemsBooked
        /// <summary>
        /// Get all booked Items
        /// </summary>
        /// <returns>Array of booked Items</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemBookedDTO>))]
        public async Task<ActionResult<IEnumerable<ItemBookedDTO>>> GetItemsBooked()
        {
            return Ok((await _bll.ItemsBooked.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ItemsBooked/5
        /// <summary>
        /// Get single booked item
        /// </summary>
        /// <param name="id">ItemBooked id</param>
        /// <returns>ItemBookedDTO object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemBookedDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemBookedDTO>> GetItemBooked(Guid id)
        {
            var itemBooked = await _bll.ItemsBooked.FirstOrDefaultAsync(id);

            if (itemBooked == null)
            {
                return NotFound(new MessageDTO($"Booked Item with id {id} not found"));
            }

            return Ok(_mapper.Map(itemBooked));
        }

        // PUT: api/ItemsBooked/5
        /// <summary>
        /// Update the ItemBooked
        /// </summary>
        /// <param name="id">ItemBooked id</param>
        /// <param name="itemBookedDTO">ItemBookedDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutItemBooked(Guid id, ItemBookedDTO itemBookedDTO)
        {
            if (id != itemBookedDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and itemBookedEditDTO.id do not match"));
            }
            
            if (!await _bll.ItemsBooked.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Booked Item with this id {id} not found"));
            }

            itemBookedDTO.AppUserId = User.UserGuidId();
            await _bll.ItemsBooked.UpdateAsync(_mapper.Map(itemBookedDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemsBooked
        /// <summary>
        /// Post the new ItemBooked
        /// </summary>
        /// <param name="itemBookedDTO">ItemBookedDTO object</param>
        /// <returns>Created ItemBooked obkect</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemBookedDTO))]
        public async Task<ActionResult<ItemBookedDTO>> PostItemBooked(ItemBookedDTO itemBookedDTO)
        {
            itemBookedDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(itemBookedDTO);
            _bll.ItemsBooked.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemBookedDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetItemBooked", new { id = itemBookedDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, itemBookedDTO);
        }

        // DELETE: api/ItemsBooked/5
        /// <summary>
        /// Delete the ItemBooked
        /// </summary>
        /// <param name="id">ItemBooked id</param>
        /// <returns>Deleted ItemBooked object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemBookedDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemBookedDTO>> DeleteItemBooked(Guid id)
        {
            var itemBooked = await _bll.ItemsBooked.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemBooked == null)
            {
                return NotFound(new MessageDTO("ItemBooked not found"));
            }

            await _bll.ItemsBooked.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(itemBooked));
        }

    }
}
