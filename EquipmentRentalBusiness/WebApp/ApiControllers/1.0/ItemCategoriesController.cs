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
using PublicApi.DTO.v1.ItemCategoryDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// ItemCategories
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ItemCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ItemCategoryDTOMapper _mapper = new ItemCategoryDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ItemCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/ItemCategories
        /// <summary>
        /// Get all Item categories
        /// </summary>
        /// <returns>Array of Item categories</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemCategoryDTO>))]
        public async Task<ActionResult<IEnumerable<ItemCategoryDTO>>> GetItemCategories()
        {
            return Ok((await _bll.ItemCategories.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/ItemCategories/5
        /// <summary>
        /// Get single Item category
        /// </summary>
        /// <param name="id">ItemCategory id</param>
        /// <returns>ItemCategoryDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemCategoryDTO>> GetItemCategory(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id);

            if (itemCategory == null)
            {
                return NotFound(new MessageDTO($"Item category with id {id} not found"));
            }

            return Ok(_mapper.Map(itemCategory));
        }

        // PUT: api/ItemCategories/5
        /// <summary>
        /// Update the ItemCategory
        /// </summary>
        /// <param name="id">ItemCategory id</param>
        /// <param name="itemCategoryDTO">ItemCategoryDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutItemCategory(Guid id, ItemCategoryDTO itemCategoryDTO)
        {
            if (id != itemCategoryDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and itemCategoryEditDTO.id do not match"));
            }
            
            if (!await _bll.ItemCategories.ExistsAsync(itemCategoryDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Item category with this id {id} not found"));
            }

            itemCategoryDTO.AppUserId = User.UserGuidId();
            await _bll.ItemCategories.UpdateAsync(_mapper.Map(itemCategoryDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ItemCategories
        /// <summary>
        /// Post the new ItemCategory
        /// </summary>
        /// <param name="itemCategoryDTO">ItemCategoryDTO object</param>
        /// <returns>Created ItemCategory object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemCategoryDTO))]
        public async Task<ActionResult<ItemCategoryDTO>> PostItemCategory(ItemCategoryDTO itemCategoryDTO)
        {
            itemCategoryDTO.AppUserId = User.UserGuidId();
            var bllEntity = _mapper.Map(itemCategoryDTO);
            _bll.ItemCategories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            itemCategoryDTO.Id = bllEntity.Id;
            
            return CreatedAtAction("GetItemCategory", new { id = itemCategoryDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, itemCategoryDTO);
        }

        // DELETE: api/ItemCategories/5
        /// <summary>
        /// Delete the ItemCategory
        /// </summary>
        /// <param name="id">ItemCategory id</param>
        /// <returns>Deleted ItemCategory object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemCategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ItemCategoryDTO>> DeleteItemCategory(Guid id)
        {
            var itemCategory = await _bll.ItemCategories.FirstOrDefaultAsync(id, User.UserGuidId());
            if (itemCategory == null)
            {
                return NotFound(new MessageDTO("ItemCategory not found"));
            }

            await _bll.ItemCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(itemCategory));
        }

    }
}
