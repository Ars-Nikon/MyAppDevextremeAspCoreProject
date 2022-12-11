using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    public class EmployeeController : Controller
    {
        readonly ApplicationContext _appContext;
        readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ApplicationContext applicationContext, ILogger<EmployeeController> logger)
        {
            _appContext = applicationContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object GetEmployees(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var dataSourceLoader = DataSourceLoader.Load(_appContext.Employees.Include(x => x.EmployeeFilials).ThenInclude(x => x.Filial), loadOptions);
                if (loadOptions.Group != null)
                {
                    return dataSourceLoader;
                }
                var data = dataSourceLoader.data.Cast<Employee>().ToList();
                data.ForEach(x =>
                {
                    x.GuidFilials = x.EmployeeFilials.Select(x => x.FilialId).ToList();
                    x.FilialsName = string.Join(',', x.EmployeeFilials.Select(x => x?.Filial?.Name));
                    x.EmployeeFilials = null!;
                });
                dataSourceLoader.data = data;
                return dataSourceLoader;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ex.Message;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Guid key, string values)
        {
            try
            {
                var employee = await _appContext.
                    Employees.
                    Include(x => x.EmployeeFilials).
                    FirstOrDefaultAsync(o => o.Id == key);

                if (employee == null)
                {
                    return BadRequest("Id отсутствует");
                }

                if (!TryValidateModel(employee))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                JsonConvert.PopulateObject(values, employee);
                employee.EmployeeFilials = employee.GuidFilials.Select(x => new EmployeeFilial { FilialId = x }).ToList();

                await _appContext.SaveChangesAsync();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertEmployee(string values)
        {
            try
            {
                var employee = new Employee();
                JsonConvert.PopulateObject(values, employee);

                if (!TryValidateModel(employee))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }
                employee.EmployeeFilials = employee.GuidFilials.Select(x => new EmployeeFilial { FilialId = x }).ToList();

                await _appContext.Employees.AddAsync(employee);
                await _appContext.SaveChangesAsync();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid key)
        {
            try
            {
                var employee = await _appContext
                    .Employees
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (employee == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.Employees.Remove(employee);

                await _appContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
