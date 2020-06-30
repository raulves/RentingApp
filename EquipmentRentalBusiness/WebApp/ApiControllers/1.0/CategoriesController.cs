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
using PublicApi.DTO.v1.CategoryDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Categories
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryDTOMapper _mapper = new CategoryDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        
        // GET: api/Categories
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Array of categories</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryDTO>))]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            return Ok((await _bll.Categories.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get single category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>CategoryDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound(new MessageDTO($"Category with id {id} not found"));
            }

            return Ok(_mapper.Map(category));
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Update the Category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="categoryDTO">CategoryDTO object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutCategory(Guid id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and categoryEditDTO.id do not match"));
            }
            
            if (!await _bll.Categories.ExistsAsync(categoryDTO.Id))
            {
                return NotFound(new MessageDTO($"Category with this id {id} not found"));
            }

            await _bll.Categories.UpdateAsync(_mapper.Map(categoryDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Categories
        /// <summary>
        /// Post the new Category
        /// </summary>
        /// <param name="categoryDTO">CategoryDTO object</param>
        /// <returns>Category object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            var bllEntity = _mapper.Map(categoryDTO);
            _bll.Categories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            categoryDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetCategory", new { id = categoryDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"  }, categoryDTO);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete the Category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>Deleted Category object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<CategoryDTO>> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound(new MessageDTO("Category not found"));
            }

            await _bll.Categories.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(category));
        }

    }
}
