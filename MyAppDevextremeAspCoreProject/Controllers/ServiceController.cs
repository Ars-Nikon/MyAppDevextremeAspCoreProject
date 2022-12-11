using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class ServiceController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<ServiceController> _logger;
        public ServiceController(ApplicationContext applicationContext, ILogger<ServiceController> logger)
        {
            _appContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public object GetServices(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return DataSourceLoader.Load(_appContext.Services, loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(Guid key, string values)
        {
            try
            {
                var service = await _appContext.
                    Services.
                    FirstOrDefaultAsync(o => o.Id == key);

                if (service == null)
                {
                    return BadRequest("Id отсутствует");
                }

                if (!TryValidateModel(service))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                JsonConvert.PopulateObject(values, service);

                await _appContext.SaveChangesAsync();

                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertService(string values)
        {
            try
            {
                var service = new Service();
                JsonConvert.PopulateObject(values, service);

                if (!TryValidateModel(service))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                await _appContext.Services.AddAsync(service);
                await _appContext.SaveChangesAsync();

                return Ok(service);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteService(Guid key)
        {
            try
            {
                var service = await _appContext
                    .Services
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (service == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.Services.Remove(service);

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
        public object GetServicesLookup(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return DataSourceLoader.Load(_appContext.Services.Select(x => new { x.Id, x.Name }), loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }
    }
}
