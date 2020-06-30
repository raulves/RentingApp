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
using PublicApi.DTO.v1.Mappers;
using PublicApi.DTO.v1.PaymentDTOs;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Payments
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentDTOMapper _mapper = new PaymentDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Payments
        /// <summary>
        /// Get all AppUser payments
        /// </summary>
        /// <returns>Array of Payments</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaymentDTO>))]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            return Ok((await _bll.Payments.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: api/Payments/5
        /// <summary>
        /// Get single AppUser payment
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>PaymentDTO object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaymentDTO>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());

            if (payment == null)
            {
                return NotFound(new MessageDTO($"AppUser company with id {id} not found"));
            }

            return Ok(_mapper.Map(payment));
        }

        // PUT: api/Payments/5
        /// <summary>
        /// Update the Payment
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <param name="paymentDTO">PaymentDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutPayment(Guid id, PaymentDTO paymentDTO)
        {
            if (id != paymentDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and paymentEditDTO.id do not match"));
            }
            
            if (!await _bll.Payments.ExistsAsync(paymentDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have payment with this id {id}"));
            }

            paymentDTO.AppUserId = User.UserGuidId();
            await _bll.Payments.UpdateAsync(_mapper.Map(paymentDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Payments
        /// <summary>
        /// Post the new Payment
        /// </summary>
        /// <param name="paymentDTO">PaymentDTO object</param>
        /// <returns>Created Payment object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentDTO))]
        public async Task<ActionResult<PaymentDTO>> PostPayment(PaymentDTO paymentDTO)
        {
            paymentDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(paymentDTO);
            _bll.Payments.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetPayment", new { id = paymentDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, paymentDTO);
        }

        // DELETE: api/Payments/5
        /// <summary>
        /// Delete the Payment
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <returns>Deleted Payment object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaymentDTO>> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (payment == null)
            {
                return NotFound(new MessageDTO("Payment not found"));
            }

            await _bll.Payments.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return Ok(_mapper.Map(payment));
        }

    }
}
