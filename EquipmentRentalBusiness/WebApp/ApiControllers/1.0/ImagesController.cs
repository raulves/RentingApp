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
using PublicApi.DTO.v1.ImageDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Images
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ImagesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ImageDTOMapper _mapper = new ImageDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">IAppBLL object</param>
        public ImagesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Images
        /// <summary>
        /// Get all AppUser images
        /// </summary>
        /// <returns>Array of images</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageDTO>))]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages()
        {
            return Ok((await _bll.Images.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: api/Images/5
        /// <summary>
        /// Get single AppUser image
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns>ImageDTO object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ImageDTO>> GetImage(Guid id)
        {
            var image = await _bll.Images.FirstOrDefaultAsync(id, User.UserGuidId());

            if (image == null)
            {
                return NotFound(new MessageDTO($"AppUser image with id {id} not found"));
            }
            
            return Ok(_mapper.Map(image));
        }

        // PUT: api/Images/5
        /// <summary>
        /// Update the Image
        /// </summary>
        /// <param name="id">Image id</param>
        /// <param name="imageDTO">ImageDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutImage(Guid id, ImageDTO imageDTO)
        {
            if (id != imageDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and imageEditDTO.id do not match"));
            }

            if (!await _bll.Images.ExistsAsync(imageDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have image with this id {id}"));
            }

            imageDTO.AppUserId = User.UserGuidId();
            await _bll.Images.UpdateAsync(_mapper.Map(imageDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Images
        /// <summary>
        /// Post the new Image
        /// </summary>
        /// <param name="imageDTO">ImageDTO object</param>
        /// <returns>Created Image object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ImageBLL))]
        public async Task<ActionResult<ImageBLL>> PostImage(ImageDTO imageDTO)
        {

            imageDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(imageDTO);
            _bll.Images.Add(bllEntity);
            await _bll.SaveChangesAsync();
            imageDTO.Id = bllEntity.Id;
            
            return CreatedAtAction("GetImage", new { id = imageDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"  }, imageDTO);
        }

        // DELETE: api/Images/5
        /// <summary>
        /// Delete the Image
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns>Deleted Image object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ImageDTO>> DeleteImage(Guid id)
        {
            var image = await _bll.Images.FirstOrDefaultAsync(id, User.UserGuidId());
            if (image == null)
            {
                return NotFound(new MessageDTO("Image not found"));
            }

            await _bll.Images.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(image));
        }

    }
}
