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
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Payment types
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentTypeDTOMapper _mapper = new PaymentTypeDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/PaymentTypes
        /// <summary>
        /// Get all Payment types
        /// </summary>
        /// <returns>Array of Payment types</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaymentTypeDTO>))]
        public async Task<ActionResult<IEnumerable<PaymentTypeDTO>>> GetPaymentTypes()
        {
            return Ok((await _bll.PaymentTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/PaymentTypes/5
        /// <summary>
        /// Get single Payment type
        /// </summary>
        /// <param name="id">PaymentType id</param>
        /// <returns>PaymentTypeDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaymentTypeDTO>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound(new MessageDTO($"Payment type with id {id} not found"));
            }

            return Ok(_mapper.Map(paymentType));
        }

        // PUT: api/PaymentTypes/5
        /// <summary>
        /// Update the PaymentType
        /// </summary>
        /// <param name="id">PaymentType id</param>
        /// <param name="paymentTypeDTO">PaymentTypeDTO object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentTypeDTO paymentTypeDTO)
        {
            if (id != paymentTypeDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and paymentTypeEditDTO.id do not match"));
            }
            
            if (!await _bll.PaymentTypes.ExistsAsync(paymentTypeDTO.Id))
            {
                return NotFound(new MessageDTO($"Payment type with this id {id} not found"));
            }

            await _bll.PaymentTypes.UpdateAsync(_mapper.Map(paymentTypeDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PaymentTypes
        /// <summary>
        /// Post the new PaymentType
        /// </summary>
        /// <param name="paymentTypeDTO">PaymentTypeDTO object</param>
        /// <returns>Created PaymentType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentTypeDTO))]
        public async Task<ActionResult<PaymentTypeDTO>> PostPaymentType(PaymentTypeDTO paymentTypeDTO)
        {
            var bllEntity = _mapper.Map(paymentTypeDTO);
            _bll.PaymentTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentTypeDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetPaymentType", new { id = paymentTypeDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, paymentTypeDTO);
        }

        // DELETE: api/PaymentTypes/5
        /// <summary>
        /// Delete the PaymentType
        /// </summary>
        /// <param name="id">PaymentType id</param>
        /// <returns>Deleted PaymentType object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentTypeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaymentTypeDTO>> DeletePaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound(new MessageDTO("PaymentType not found"));
            }

            await _bll.PaymentTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(paymentType));
        }

    }
}
