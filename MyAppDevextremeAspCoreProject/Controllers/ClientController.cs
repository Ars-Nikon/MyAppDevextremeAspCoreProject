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
    public class ClientController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<ClientController> _logger;
        public ClientController(ApplicationContext applicationContext, ILogger<ClientController> logger)
        {
            _appContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public object GetClients(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return DataSourceLoader.Load(_appContext.Clients, loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(Guid key, string values)
        {
            try
            {
                var client = await _appContext.
                    Clients.
                    FirstOrDefaultAsync(o => o.Id == key);

                if (client == null)
                {
                    return BadRequest("Id отсутствует");
                }

                if (!TryValidateModel(client))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                JsonConvert.PopulateObject(values, client);

                await _appContext.SaveChangesAsync();

                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertClient(string values)
        {
            try
            {
                var client = new Client();
                JsonConvert.PopulateObject(values, client);

                if (!TryValidateModel(client))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                await _appContext.Clients.AddAsync(client);
                await _appContext.SaveChangesAsync();

                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(Guid key)
        {
            try
            {
                var client = await _appContext
                    .Clients
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (client == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.Clients.Remove(client);

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
        public object GetClientsLookup(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return DataSourceLoader.Load(_appContext.Clients.Select(x => new { x.Id, x.Name }), loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }
    }
}
