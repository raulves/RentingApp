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
using PublicApi.DTO.v1.ItemDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Items
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemDTOMapper _mapper = new ItemDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ItemsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Items
        /// <summary>
        /// Get all Items
        /// </summary>
        /// <returns>Array of Items</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemDTO>))]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            return Ok((await _bll.Items.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Items/5
        /// <summary>
        /// Get single Item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>ItemDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id);

            if (item == null)
            {
                return NotFound(new MessageDTO($"Item with id {id} not found"));
            }
            
            return Ok(_mapper.Map(item));
        }

        // PUT: api/Items/5
        /// <summary>
        /// Update the Item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <param name="itemDTO">ItemDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutItem(Guid id, ItemDTO itemDTO)
        {
            if (id != itemDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and itemEditDTO.id do not match"));
            }
            
            if (!await _bll.Items.ExistsAsync(id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Item with this id {id} not found"));
            }

            itemDTO.AppUserId = User.UserGuidId();
            await _bll.Items.UpdateAsync(_mapper.Map(itemDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Items
        /// <summary>
        /// Post the Item
        /// </summary>
        /// <param name="itemDTO">ItemDTO object</param>
        /// <returns>Created Item object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemDTO))]
        public async Task<ActionResult<ItemDTO>> PostItem(ItemDTO itemDTO)
        {
            itemDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(itemDTO);
            _bll.Items.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetItem", new { id = itemDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, itemDTO);
        }

        // DELETE: api/Items/5
        /// <summary>
        /// Delete the Item
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Deleted Item object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemDTO>> DeleteItem(Guid id)
        {
            var item = await _bll.Items.FirstOrDefaultAsync(id, User.UserGuidId());
            if (item == null)
            {
                return NotFound(new MessageDTO("Item not found"));
            }

            await _bll.Items.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(item));
        }

    }
}
