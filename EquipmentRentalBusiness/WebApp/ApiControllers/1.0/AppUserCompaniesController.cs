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
using PublicApi.DTO.v1.AppUserCompanyDTOs;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// AppUser companies
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AppUserCompaniesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AppUserCompanyDTOMapper _mapper = new AppUserCompanyDTOMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public AppUserCompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/AppUserCompanies
        /// <summary>
        /// Get all AppUser companies
        /// </summary>
        /// <returns>Array of AppUser companies</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AppUserCompanyDTO>))]
        public async Task<ActionResult<IEnumerable<AppUserCompanyDTO>>> GetAppUserCompanies()
        {
            return Ok((await _bll.AppUserCompanies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/AppUserCompanies/5
        /// <summary>
        /// Get a single AppUser company
        /// </summary>
        /// <param name="id">AppUserCompany id</param>
        /// <returns>AppUserCompanyDTO object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserCompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<AppUserCompanyDTO>> GetAppUserCompany(Guid id)
        {
            var appUserCompany = await _bll.AppUserCompanies.FirstOrDefaultAsync(id);

            if (appUserCompany == null)
            {
                return NotFound(new MessageDTO($"AppUser company with id {id} not found"));
            }

            return Ok(_mapper.Map(appUserCompany));
        }
        
        // PUT: api/AppUserCompanies/5
        /// <summary>
        /// Update the AppUserCompany
        /// </summary>
        /// <param name="id">AppUserCompany id</param>
        /// <param name="appUserCompanyDTO">AppUserCompanyEditDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutAppUserCompany(Guid id, AppUserCompanyDTO appUserCompanyDTO)
        {
            
            if (id != appUserCompanyDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and appUserCompanyEditDTO.id do not match"));
            }

            if (!await _bll.AppUserCompanies.ExistsAsync(appUserCompanyDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have that company."));
            }
            
            appUserCompanyDTO.AppUserId = User.UserGuidId();

            await _bll.AppUserCompanies.UpdateAsync(_mapper.Map(appUserCompanyDTO), User.UserGuidId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/AppUserCompanies
        /// <summary>
        /// Post the new AppUserCompany
        /// </summary>
        /// <param name="appUserCompanyDTO">AppUserCompanyCreateDTO object</param>
        /// <returns>Created AppUserCompany object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppUserCompanyDTO))]
        public async Task<ActionResult<AppUserCompanyDTO>> PostAppUserCompany(AppUserCompanyDTO appUserCompanyDTO)
        {
            appUserCompanyDTO.AppUserId = User.UserGuidId();

            var bllEntity = _mapper.Map(appUserCompanyDTO);
            _bll.AppUserCompanies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            appUserCompanyDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetAppUserCompany", 
                new { id = appUserCompanyDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, appUserCompanyDTO);
        }

        // DELETE: api/AppUserCompanies/5
        /// <summary>
        /// Delete the AppUserCompany
        /// </summary>
        /// <param name="id">AppUserCompany id</param>
        /// <returns>Deleted AppUserCompany object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppUserCompanyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<AppUserCompanyDTO>> DeleteAppUserCompany(Guid id)
        {
            var appUserCompany = await _bll.AppUserCompanies.FirstOrDefaultAsync(id, User.UserGuidId());
            if (appUserCompany == null)
            {
                return NotFound(new MessageDTO("AppUserCompany not found"));
            }

            await _bll.AppUserCompanies.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_mapper.Map(appUserCompany));
        }
    }
}
