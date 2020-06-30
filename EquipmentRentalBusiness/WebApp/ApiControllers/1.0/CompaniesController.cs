using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App;

using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.CompanyDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Companies
    /// </summary>
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CompaniesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CompanyDTOMapper _mapper = new CompanyDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll">IAppBLL object</param>
        public CompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: api/Companies
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>Array of companies</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyDTO>))]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            return Ok((await _bll.Companies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }
        
        // GET: api/version/GetAppUserCompanies
        /// <summary>
        /// Get all AppUser companies
        /// </summary>
        /// <returns>Array of companies</returns>
        [HttpGet]
        [ActionName("AppUserCompanies")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyDTO>))]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAppUserCompanies()
        {
            return Ok((await _bll.Companies.GetAllAsync(User.UserGuidId())).Select(e => _mapper.Map(e)));
        }
        
        
        // GET: api/version/GetAppUserCompany/5
        /// <summary>
        /// Get single AppUser company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>CompanyDTO object</returns>
        [HttpGet("{id}")]
        [ActionName("AppUserCompany")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<CompanyDTO>> GetAppUserCompany(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id, User.UserGuidId());

            if (company == null)
            {
                return NotFound(new MessageDTO($"AppUser company with id {id} not found"));
            }

            return Ok(_mapper.Map(company));
        }
        

        // GET: api/Companies/5
        /// <summary>
        /// Get single company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>CompanyDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<CompanyDTO>> GetCompany(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id);

            if (company == null)
            {
                return NotFound(new MessageDTO($"Company with id {id} not found"));
            }

            return Ok(_mapper.Map(company));
        }

        // PUT: api/Companies/5
        /// <summary>
        /// Update the Company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <param name="companyDTO">CompanyDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutCompany(Guid id, CompanyDTO companyDTO)
        {
            if (id != companyDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and companyEditDTO.id do not match"));
            }
            
            if (!await _bll.Companies.ExistsAsync(companyDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have company with this id {id}"));
            }

            companyDTO.AppUserId = User.UserGuidId();
            await _bll.Companies.UpdateAsync(_mapper.Map(companyDTO));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Companies
        /// <summary>
        /// Post the new Company
        /// </summary>
        /// <param name="companyDTO">CompanyDTO object</param>
        /// <returns>Created Company object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CompanyDTO))]
        public async Task<ActionResult<CompanyDTO>> PostCompany(CompanyDTO companyDTO)
        {
            companyDTO.AppUserId = User.UserGuidId();

            var bllEntity = _mapper.Map(companyDTO);
            _bll.Companies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            companyDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetCompany", new { id = companyDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"  }, companyDTO);
        }

        // DELETE: api/Companies/5
        /// <summary>
        /// Delete the Company
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Deleted Company object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<CompanyDTO>> DeleteCompany(Guid id)
        {
            var company = await _bll.Companies.FirstOrDefaultAsync(id, User.UserGuidId());
            if (company == null)
            {
                return NotFound(new MessageDTO("Company not found"));
            }

            await _bll.Companies.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(company));
        }

    }
}
