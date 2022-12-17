using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Models;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;

namespace MyAppDevextremeAspCoreProject.Controllers
{
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

                return await DataSourceLoader.LoadAsync(data, loadOptions); ;
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
                var data = _appContext.
                    EmployeeFilials.
                    Include(x => x.Filial).
                    Where(x => x.EmployeeId == Guid.Parse(guid));

                return await DataSourceLoader.LoadAsync(data.Select(x => new { Id = x.FilialId, Name = x.Filial!.Name }), loadOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Post(string values, [FromQuery] string? guidEmployee, [FromQuery] string? guidFilial)
        {
            try
            {
             
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            //var appointment = _data.Appointments.First(a => a.AppointmentId == key);
            //JsonConvert.PopulateObject(values, appointment);

            //if (!TryValidateModel(appointment))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            //_data.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            //var appointment = _data.Appointments.First(a => a.AppointmentId == key);
            //_data.Appointments.Remove(appointment);
            //_data.SaveChanges();
        }


    }
}
