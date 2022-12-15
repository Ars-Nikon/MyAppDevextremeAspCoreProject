﻿using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppDevextremeAspCoreProject.Contexts;
using MyAppDevextremeAspCoreProject.Utilities;
using Newtonsoft.Json;

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
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                loadOptions.Filter = null;
                return await DataSourceLoader.LoadAsync(_appContext.FullScheduleViews, loadOptions);
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
                return ex.Message;
            }
        }



        [HttpPost]
        public IActionResult Post(string values)
        {
            //var newAppointment = new Appointment();
            //JsonConvert.PopulateObject(values, newAppointment);

            //if (!TryValidateModel(newAppointment))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            //_data.Appointments.Add(newAppointment);
            //_data.SaveChanges();

            return Ok();
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
