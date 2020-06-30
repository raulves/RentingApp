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
using PublicApi.DTO.v1.BookingDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Bookings
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class BookingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BookingDTOMapper _mapper = new BookingDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public BookingsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Bookings
        /// <summary>
        /// Get all appUser bookings
        /// </summary>
        /// <returns>Array of bookings</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookingDTO>))]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetBookings()
        {
            return Ok((await _bll.Bookings.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: api/Bookings/5
        /// <summary>
        /// Get a single appUser booking
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <returns>BookingDTO object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BookingDTO>> GetBooking(Guid id)
        {
            var booking = await _bll.Bookings.FirstOrDefaultAsync(id, User.UserGuidId());

            if (booking == null)
            {
                return NotFound(new MessageDTO($"Booking with id {id} not found"));
            }

            return Ok(_mapper.Map(booking));
        }

        // PUT: api/Bookings/5
        /// <summary>
        /// Update the booking
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <param name="bookingDTO">BookingDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutBooking(Guid id, BookingDTO bookingDTO)
        {
            if (id != bookingDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and bookingEditDTO.id do not match"));
            }
            
            if (!await _bll.Bookings.ExistsAsync(bookingDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have booking with this id {id}"));
            }

            bookingDTO.AppUserId = User.UserGuidId();
            await _bll.Bookings.UpdateAsync(_mapper.Map(bookingDTO));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Bookings
        /// <summary>
        /// Post the new Booking
        /// </summary>
        /// <param name="bookingDTO">BookingDTO object</param>
        /// <returns>Created Booking object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookingDTO))]
        public async Task<ActionResult<BookingDTO>> PostBooking(BookingDTO bookingDTO)
        {
            bookingDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(bookingDTO);
            _bll.Bookings.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bookingDTO.Id = bllEntity.Id;
            
            return CreatedAtAction("GetBooking", 
                new { id = bookingDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, bookingDTO);
        }

        // DELETE: api/Bookings/5
        /// <summary>
        /// Delete the Booking
        /// </summary>
        /// <param name="id">Booking id</param>
        /// <returns>Deleted Booking object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BookingDTO>> DeleteBooking(Guid id)
        {
            var booking = await _bll.Bookings.FirstOrDefaultAsync(id, User.UserGuidId());
            if (booking == null)
            {
                return NotFound(new MessageDTO("Booking not found"));
            }

            await _bll.Bookings.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return Ok(_mapper.Map(booking));
        }
        
    }
}
