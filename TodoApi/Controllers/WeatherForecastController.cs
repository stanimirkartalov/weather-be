
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;
using WeatherApi.Models.Request;
using WeatherApi.Services;

namespace EFCoreMySQL.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private weatherContext myDbContext;

        public WeatherForecastController(weatherContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public IList<Reading> Get()
        {
            var readings = this.myDbContext.Readings;
            
            return (this.myDbContext.Readings.ToList());
        }

        [HttpGet]
        [Route("forecast")]
        public IList<Reading> Forecast()
        {
            var readings = this.myDbContext.Readings;
            WeatherApi.Libralies.ForecastCreator forecastCreator = new WeatherApi.Libralies.ForecastCreator();
            return forecastCreator.makeForecast(readings.ToList());
        }

        [HttpPost]
        public async Task<Reading> Post()
        {
            WeatherStationService service = new WeatherStationService();
            Settings settings = this.myDbContext.Settings.Find((uint)1);
            if (settings == null)
            {
                throw new Exception("Settings not set - send an IP");
            }
            String str = await service.GetAsync(settings.Ip + "/data");
            string[] values = str.Split("\r\n");
            Reading reading = new Reading
            {
                Humidity = Convert.ToDouble(values[4]),
                Lightness = Convert.ToDouble(values[0]),
                AthmosphericPreasure = Convert.ToDouble(values[1]),
                Temperature = Convert.ToDouble(values[2]),
                LocationId = 1
            };
            this.myDbContext.Readings.Add(reading);
            this.myDbContext.SaveChanges();
            return reading;
        }

        [HttpPost]
        [Route("settings")]
        public Settings SetSettings([FromBody] SetSettings s)
        {
            bool exists = this.myDbContext.Settings.Any(x => x.Id == 1);

            Settings settings = new()
            {
                Id = 1,
                Ip = s.Ip
            };

            if (exists)
            {
                this.myDbContext.Settings.Update(settings);
            }
            else
            {
                this.myDbContext.Settings.Add(settings);
            }

            this.myDbContext.SaveChanges();
            return settings;
        }
    }
}