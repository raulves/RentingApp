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
using PublicApi.DTO.v1.ItemDescriptionDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Item descriptions
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemDescriptionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemDescriptionDTOMapper _mapper = new ItemDescriptionDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ItemDescriptionsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ItemDescriptions
        /// <summary>
        /// Get all Item descriptions
        /// </summary>
        /// <returns>Array of Item descriptions</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemDescriptionDTO>))]
        public async Task<ActionResult<IEnumerable<ItemDescriptionDTO>>> GetItemDescriptions()
        {
            return Ok((await _bll.ItemDescriptions.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ItemDescriptions/5
        /// <summary>
        /// Get single Item description
        /// </summary>
        /// <param name="id">ItemDescription id</param>
        /// <returns>ItemDescriptionDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDescriptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemDescriptionDTO>> GetItemDescription(Guid id)
        {
            var itemDescription = await _bll.ItemDescriptions.FirstOrDefaultAsync(id);

            if (itemDescription == null)
            {
                return NotFound(new MessageDTO($"ItemDescription with id {id} not found"));
            }

            return Ok(_mapper.Map(itemDescription));
        }

        // PUT: api/ItemDescriptions/5
        /// <summary>
        /// Update the ItemDescription
        /// </summary>
        /// <param name="id">ItemDescription id</param>
        /// <param name="itemDescriptionDTO">ItemDescriptionDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutItemDescription(Guid id, ItemDescriptionDTO itemDescriptionDTO)
        {
            if (id != itemDescriptionDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and itemDescriptionEditDTO.id do not match"));
            }
            
            if (!await _bll.ItemDescriptions.ExistsAsync(itemDescriptionDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"ItemDescription with this id {id} not found"));
            }

            itemDescriptionDTO.AppUserId = User.UserGuidId();
            await _bll.ItemDescriptions.UpdateAsync(_mapper.Map(itemDescriptionDTO));
            await _bll.SaveChangesAsync();
            

            return NoContent();
        }

        // POST: api/ItemDescriptions
        /// <summary>
        /// Post the new ItemDescription
        /// </summary>
        /// <param name="itemDescriptionDTO">ItemDescriptionDTO object</param>
        /// <returns>Created ItemDescription object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemDescriptionDTO))]
        public async Task<ActionResult<ItemDescriptionDTO>> PostItemDescription(ItemDescriptionDTO itemDescriptionDTO)
        {
            itemDescriptionDTO.AppUserId = User.UserGuidId();
            var bllEntity = _mapper.Map(itemDescriptionDTO);
            _bll.ItemDescriptions.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemDescriptionDTO.Id = bllEntity.Id;
            
            return CreatedAtAction("GetItemDescription", new { id = itemDescriptionDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, itemDescriptionDTO);
        }

        // DELETE: api/ItemDescriptions/5
        /// <summary>
        /// Delete the ItemDescription
        /// </summary>
        /// <param name="id">ItemDescription id</param>
        /// <returns>Deleted ItemDescription object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemDescriptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemDescriptionDTO>> DeleteItemDescription(Guid id)
        {
            var itemDescription = await _bll.ItemDescriptions.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemDescription == null)
            {
                return NotFound(new MessageDTO("ItemDescription not found"));
            }

            await _bll.ItemDescriptions.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(itemDescription));
        }

    }
}
