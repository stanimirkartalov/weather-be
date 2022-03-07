
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private TodoApi.Models.weatherContext myDbContext;

        public WeatherForecastController(TodoApi.Models.weatherContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public IList<TodoApi.Models.Reading> Get()
        {
            var readings = this.myDbContext.Readings;
            
            return (this.myDbContext.Readings.ToList());
        }

        [HttpGet]
        [Route("forecast")]
        public IList<TodoApi.Models.Reading> Forecast()
        {
            var readings = this.myDbContext.Readings;
            TodoApi.Libralies.ForecastCreator forecastCreator = new TodoApi.Libralies.ForecastCreator();
            return forecastCreator.makeForecast(readings.ToList());
        }

        [HttpPost]
        public async Task<TodoApi.Models.Reading> Post()
        {
            TodoApi.Services.WeatherStationService service = new TodoApi.Services.WeatherStationService();
            String str = await service.GetAsync("http://192.168.0.105/data");
            string[] values = str.Split("\r\n");
            //TodoApi.Models.Location location = this.myDbContext.Locations.FirstOrDefault(l => l.Name == "Plovdiv");
            TodoApi.Models.Reading reading = new TodoApi.Models.Reading
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
    }
}