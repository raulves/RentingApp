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
using PublicApi.DTO.v1.ItemOwnershipDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Item ownerships
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemOwnershipsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemOwnershipDTOMapper _mapper = new ItemOwnershipDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ItemOwnershipsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ItemOwnerships
        /// <summary>
        /// Get all AppUser Item ownerships
        /// </summary>
        /// <returns>Array of Item ownerships</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemOwnershipDTO>))]
        public async Task<ActionResult<IEnumerable<ItemOwnershipDTO>>> GetItemOwnerships()
        {
            return Ok((await _bll.ItemOwnerships.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ItemOwnerships/5
        /// <summary>
        /// Get single AppUser Item ownership
        /// </summary>
        /// <param name="id">ItemOwnership id</param>
        /// <returns>ItemOwnershipDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemOwnershipDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemOwnershipDTO>> GetItemOwnership(Guid id)
        {
            var itemOwnership = await _bll.ItemOwnerships.FirstOrDefaultAsync(id);

            if (itemOwnership == null)
            {
                return NotFound(new MessageDTO($"Item ownership with id {id} not found"));
            }

            return Ok(_mapper.Map(itemOwnership));
        }

        // PUT: api/ItemOwnerships/5
        /// <summary>
        /// Update the ItemOwnership
        /// </summary>
        /// <param name="id">ItemOwnership id</param>
        /// <param name="itemOwnershipDTO">ItemOwnershipDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutItemOwnership(Guid id, ItemOwnershipDTO itemOwnershipDTO)
        {
            if (id != itemOwnershipDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and itemOwnershipEditDTO.id do not match"));
            }
            
            if (!await _bll.ItemOwnerships.ExistsAsync(itemOwnershipDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have Item ownership with this id {id}"));
            }

            itemOwnershipDTO.AppUserId = User.UserGuidId();
            await _bll.ItemOwnerships.UpdateAsync(_mapper.Map(itemOwnershipDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemOwnerships
        /// <summary>
        /// Post the ItemOwnership
        /// </summary>
        /// <param name="itemOwnershipDTO">ItemOwnershipDTO object</param>
        /// <returns>Created ItemOwnership object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemOwnershipDTO))]
        public async Task<ActionResult<ItemOwnershipDTO>> PostItemOwnership(ItemOwnershipDTO itemOwnershipDTO)
        {
            itemOwnershipDTO.AppUserId = User.UserGuidId();
            var bllEntity = _mapper.Map(itemOwnershipDTO);
            _bll.ItemOwnerships.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemOwnershipDTO.Id = bllEntity.Id;
            
            return CreatedAtAction("GetItemOwnership", new { id = itemOwnershipDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, itemOwnershipDTO);
        }

        // DELETE: api/ItemOwnerships/5
        /// <summary>
        /// Delete the ItemOwnership
        /// </summary>
        /// <param name="id">ItemOwnership id</param>
        /// <returns>Deleted ItemOwnership object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemOwnershipDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemOwnershipDTO>> DeleteItemOwnership(Guid id)
        {
            var itemOwnership = await _bll.ItemOwnerships.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemOwnership == null)
            {
                return NotFound(new MessageDTO("ItemOwnership not found"));
            }

            await _bll.ItemOwnerships.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(itemOwnership));
        }

    }
}
