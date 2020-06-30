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
using PublicApi.DTO.v1.InvoiceDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Invoices
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceDTOMapper _mapper = new InvoiceDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Invoices
        /// <summary>
        /// Get all AppUser invoices
        /// </summary>
        /// <returns>Array of invoices</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InvoiceDTO>))]
        public async Task<ActionResult<IEnumerable<InvoiceDTO>>> GetInvoices()
        {
            return Ok((await _bll.Invoices.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }

        // GET: api/Invoices/5
        /// <summary>
        /// Get single AppUser invoice
        /// </summary>
        /// <param name="id">Invoice id</param>
        /// <returns>InvoiceDTO object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());

            if (invoice == null)
            {
                return NotFound(new MessageDTO($"AppUser invoice with id {id} not found"));
            }

            return Ok(_mapper.Map(invoice));
        }

        // PUT: api/Invoices/5
        /// <summary>
        /// Update the Invoice
        /// </summary>
        /// <param name="id">Invoice id</param>
        /// <param name="invoiceDTO">InvoiceDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutInvoice(Guid id, InvoiceDTO invoiceDTO)
        {
            if (id != invoiceDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and invoiceEditDTO.id do not match"));
            }
            
            if (!await _bll.Invoices.ExistsAsync(invoiceDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have invoice with this id {id}"));
            }

            invoiceDTO.AppUserId = User.UserGuidId();
            await _bll.Invoices.UpdateAsync(_mapper.Map(invoiceDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Invoices
        /// <summary>
        /// Post the new Invpice
        /// </summary>
        /// <param name="invoiceDTO">InvoiceDTO object</param>
        /// <returns>Created Invoice object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InvoiceDTO))]
        public async Task<ActionResult<InvoiceDTO>> PostInvoice(InvoiceDTO invoiceDTO)
        {
            invoiceDTO.AppUserId = User.UserGuidId();
            
            var bllEntity = _mapper.Map(invoiceDTO);
            _bll.Invoices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            invoiceDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetInvoice", new { id = invoiceDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"   }, invoiceDTO);
        }

        // DELETE: api/Invoices/5
        /// <summary>
        /// Delete the Invoice
        /// </summary>
        /// <param name="id">Invoice id</param>
        /// <returns>Deleted Invoice object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<InvoiceDTO>> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id, User.UserGuidId());
            if (invoice == null)
            {
                return NotFound(new MessageDTO("Invoice not found"));
            }

            await _bll.Invoices.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(invoice));
        }

    }
}
