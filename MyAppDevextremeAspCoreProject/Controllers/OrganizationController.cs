using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;
using System;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class OrganizationController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<OrganizationController> _logger;
        public OrganizationController(ApplicationContext applicationContext, ILogger<OrganizationController> logger)
        {
            _appContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<object> GetOrganizations(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return await DataSourceLoader.LoadAsync(_appContext.Organizations, loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization(Guid key, string values)
        {
            try
            {
                var organization = await _appContext.
                    Organizations.
                    FirstOrDefaultAsync(o => o.Id == key);

                if (organization == null)
                {
                    return BadRequest("Id отсутствует");
                }

                if (!TryValidateModel(organization))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                JsonConvert.PopulateObject(values, organization);

                await _appContext.SaveChangesAsync();

                return Ok(organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertOrganization(string values)
        {
            try
            {
                var organization = new Organization();
                JsonConvert.PopulateObject(values, organization);

                if (!TryValidateModel(organization))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                await _appContext.Organizations.AddAsync(organization);
                await _appContext.SaveChangesAsync();

                return Ok(organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrganization(Guid key)
        {
            try
            {
                var organization = await _appContext
                    .Organizations
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (organization == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.Organizations.Remove(organization);

                await _appContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<object> GetOrganizationsLookup(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return await DataSourceLoader.LoadAsync(_appContext.Organizations, loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }
    }
}