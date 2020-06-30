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
using PublicApi.DTO.v1.RentalPeriodDTOs;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Rental periods
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class RentalPeriodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RentalPeriodDTOMapper _mapper = new RentalPeriodDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public RentalPeriodsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/RentalPeriods
        /// <summary>
        /// Get all rental periods
        /// </summary>
        /// <returns>Array of rental periods</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalPeriodDTO>))]
        public async Task<ActionResult<IEnumerable<RentalPeriodDTO>>> GetRentalPeriods()
        {
            return Ok((await _bll.RentalPeriods.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/RentalPeriods/5
        /// <summary>
        /// Get single RentalPeriod
        /// </summary>
        /// <param name="id">RentalPeriod id</param>
        /// <returns>RentalPeriodDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalPeriodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<RentalPeriodDTO>> GetRentalPeriod(Guid id)
        {
            var rentalPeriod = await _bll.RentalPeriods.FirstOrDefaultAsync(id);

            if (rentalPeriod == null)
            {
                return NotFound(new MessageDTO($"Rental period with id {id} not found"));
            }

            return Ok(_mapper.Map(rentalPeriod));
        }

        // PUT: api/RentalPeriods/5
        /// <summary>
        /// Update the RentalPeriod
        /// </summary>
        /// <param name="id">RentalPeriod id</param>
        /// <param name="rentalPeriodDTO">RentalPeriodDTO object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutRentalPeriod(Guid id, RentalPeriodDTO rentalPeriodDTO)
        {
            if (id != rentalPeriodDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and rentalPeriodEditDTO.id do not match"));
            }
            
            if (!await _bll.RentalPeriods.ExistsAsync(rentalPeriodDTO.Id))
            {
                return NotFound(new MessageDTO($"Rental period with this id {id} not found"));
            }

            await _bll.RentalPeriods.UpdateAsync(_mapper.Map(rentalPeriodDTO));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/RentalPeriods
        /// <summary>
        /// Post the new RentalPeriod
        /// </summary>
        /// <param name="rentalPeriodDTO">RentalPeriodDTO object</param>
        /// <returns>Created RentalPeriod object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RentalPeriodDTO))]
        public async Task<ActionResult<RentalPeriodDTO>> PostRentalPeriod(RentalPeriodDTO rentalPeriodDTO)
        {
            var bllEntity = _mapper.Map(rentalPeriodDTO);
            _bll.RentalPeriods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            rentalPeriodDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetRentalPeriod", new { id = rentalPeriodDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, rentalPeriodDTO);
        }

        // DELETE: api/RentalPeriods/5
        /// <summary>
        /// Delete the RentalPeriod
        /// </summary>
        /// <param name="id">RentalPeriod id</param>
        /// <returns>Deleted RentalPeriod object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalPeriodDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<RentalPeriodDTO>> DeleteRentalPeriod(Guid id)
        {
            var rentalPeriod = await _bll.RentalPeriods.FirstOrDefaultAsync(id);
            if (rentalPeriod == null)
            {
                return NotFound(new MessageDTO("RentalPeriod not found"));
            }

            await _bll.RentalPeriods.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(rentalPeriod));
        }

    }
}
