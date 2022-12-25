using Bogus.DataSets;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MyAppDevextremeAspCoreProject.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        readonly ApplicationContext _appContext;
        private readonly ILogger<ScheduleController> _logger;
        public ScheduleController(ILogger<ScheduleController> logger, ApplicationContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        public async Task<object?> Get(DataSourceLoadOptions loadOptions, [FromQuery] string? guidEmployee, [FromQuery] string? guidFilial)
        {
            try
            {
                if (string.IsNullOrEmpty(guidEmployee) || string.IsNullOrEmpty(guidFilial))
                {
                    return null;
                }

#pragma warning disable CS8602,CS8600
                var dates = (loadOptions.Filter[0] as IList)[0];
                var start = DateTime.ParseExact(((IList)((IList)dates)[0])[2]?.ToString() ?? "06/25/2001", "M/d/yyyy", null);
                var end = DateTime.ParseExact(((IList)((IList)dates)[1])[2]?.ToString() ?? "06/06/2001", "M/d/yyyy", null);
#pragma warning restore CS8602, CS8600

                loadOptions.Filter = null;
                var data = _appContext.
                    FullScheduleViews.
                    Where(x => x.IdFilial == Guid.Parse(guidFilial) && x.IdEmployee == Guid.Parse(guidEmployee) && x.DateVisit >= start && x.DateVisit <= end);

                return await DataSourceLoader.LoadAsync(data, loadOptions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<object> FilialsSelectBoxByEmployee(DataSourceLoadOptions loadOptions, [FromQuery] string? guid)
        {
            try
            {
                if (string.IsNullOrEmpty(guid))
                {
                    return BadRequest("Guid is null");
                }

                var data = await _appContext.
                    EmployeeFilials.
                    Include(x => x.Filial).
                    Where(x => x.EmployeeId == Guid.Parse(guid)).ToListAsync();

                return DataSourceLoader.Load(data.Select(x => new { Id = x.FilialId, Name = x.Filial!.Name }), loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeTableIdByDate(DateTime date, Guid guidEmployee, Guid guidFilial)
        {
            try
            {
                var scheduleTime = await _appContext.
                    TimeTables.
                    Where(x => x.Date == date.Date && x.IdEmployee == guidEmployee && x.IdFilial == guidFilial).
                    FirstOrDefaultAsync();

                return Ok(scheduleTime?.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeTable([Required] DateTime? date, [Required] Guid? guidEmployee, [Required] Guid? guidFilial)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                var TimeTable = await _appContext.
                             TimeTables.
                             Where(x => x.Date == date!.Value.Date && x.IdEmployee == guidEmployee && x.IdFilial == guidFilial).
                             FirstOrDefaultAsync();

                if (TimeTable != null)
                {
                    return Ok(TimeTable.Id);
                }

                var newTimeTable = new TimeTable { Date = date!.Value, IdFilial = guidFilial!.Value, IdEmployee = guidEmployee!.Value };
                await _appContext.TimeTables.AddAsync(newTimeTable);
                await _appContext.SaveChangesAsync();

                return Ok(newTimeTable.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetShcedule(DataSourceLoadOptions loadOptions, [FromQuery] Guid? TimeTableId)
        {
            try
            {
                if (TimeTableId == null)
                {
                    return Ok("TimeTableId is null");
                }
                return Ok(await DataSourceLoader.LoadAsync(_appContext.ScheduleTimes.Where(x => x.IdTimeTable == TimeTableId.Value), loadOptions));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteShcedule(Guid key)
        {
            try
            {
                var scheduleTime = await _appContext
                    .ScheduleTimes
                    .FirstOrDefaultAsync(o => o.Id == key);

                if (scheduleTime == null)
                {
                    return BadRequest("Id отсутствует");
                }

                _appContext.ScheduleTimes.Remove(scheduleTime);

                await _appContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertShcedule(string values, [FromQuery] Guid? TimeTableId)
        {
            try
            {
                var scheduleTime = new ScheduleTime();
                JsonConvert.PopulateObject(values, scheduleTime);

                if (!TryValidateModel(scheduleTime))
                {
                    return BadRequest(ModelState.GetFullErrorMessage());
                }

                await _appContext.ScheduleTimes.AddAsync(scheduleTime);
                await _appContext.SaveChangesAsync();

                return Ok(scheduleTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
