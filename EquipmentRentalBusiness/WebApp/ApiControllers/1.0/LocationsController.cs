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
using PublicApi.DTO.v1.LocationDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Locations
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class LocationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly LocationDTOMapper _mapper = new LocationDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public LocationsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Locations
        /// <summary>
        /// Get all AppUser locations
        /// </summary>
        /// <returns>Array of Locations</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LocationDTO>))]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            return Ok((await _bll.Locations.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: api/Locations/5
        /// <summary>
        /// Get single AppUser Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <returns>LocationDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<LocationDTO>> GetLocation(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id, User.UserGuidId());

            if (location == null)
            {
                return NotFound(new MessageDTO($"AppUser location with id {id} not found"));
            }

            return Ok(_mapper.Map(location));
        }

        // PUT: api/Locations/5
        /// <summary>
        /// Update the Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <param name="locationDTO">LocationDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutLocation(Guid id, LocationDTO locationDTO)
        {
            if (id != locationDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and locationEditDTO.id do not match"));
            }
            
            if (!await _bll.Locations.ExistsAsync(locationDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have location with this id {id}"));
            }

            locationDTO.AppUserId = User.UserGuidId();
            await _bll.Locations.UpdateAsync(_mapper.Map(locationDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Locations
        /// <summary>
        /// Post the new Location
        /// </summary>
        /// <param name="locationDTO">LocationDTO object</param>
        /// <returns>Created Location object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LocationDTO))]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDTO)
        {
            locationDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(locationDTO);
            _bll.Locations.Add(bllEntity);
            await _bll.SaveChangesAsync();
            locationDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetLocation", new { id = locationDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, locationDTO);
        }

        // DELETE: api/Locations/5
        /// <summary>
        /// Delete the Location
        /// </summary>
        /// <param name="id">Location id</param>
        /// <returns>Deleted Location object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LocationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<LocationDTO>> DeleteLocation(Guid id)
        {
            var location = await _bll.Locations.FirstOrDefaultAsync(id, User.UserGuidId());
            if (location == null)
            {
                return NotFound(new MessageDTO("Location not found"));
            }

            await _bll.Locations.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return Ok(_mapper.Map(location));
        }

    }
}
