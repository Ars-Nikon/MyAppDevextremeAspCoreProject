using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class FilialController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<FilialController> _logger;
        public FilialController(ApplicationContext applicationContext, ILogger<FilialController> logger)
        {
            _appContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<object> GetFilials(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return await DataSourceLoader.LoadAsync(_appContext.Filials.Include(x => x.Organization), loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFilial(Guid key, string values)
        {
            try
            {
                var filial = await _appContext.
                    Filials.
                    FirstOrDefaultAsync(o => o.Id == key);

                if (filial == null)
                {
                    return BadRequest("Id отсутствует");
                }

                if (!TryValidateModel(filial))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                JsonConvert.PopulateObject(values, filial);

                await _appContext.SaveChangesAsync();

                return Ok(filial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertFilial(string values)
        {
            try
            {
                var filial = new Filial();
                JsonConvert.PopulateObject(values, filial);

                if (!TryValidateModel(filial))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                await _appContext.Filials.AddAsync(filial);
                await _appContext.SaveChangesAsync();

                return Ok(filial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFilial(Guid key)
        {
            try
            {
                var filial = await _appContext
                    .Filials
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (filial == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.Filials.Remove(filial);

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
        public async Task<object> GetFilialLookup(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return await DataSourceLoader.LoadAsync(_appContext.Filials, loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }
    }
}
