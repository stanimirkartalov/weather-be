using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApi.Models;

namespace WeatherApi.Libralies
{
    public class ForecastCreator
    {
        public ForecastCreator()
        {
        }

        public IList<Reading> makeForecast(IList<Reading> list)
        {
            //get direction
            int direction = getDirection(list);
            int coefficient = getCoefficient(list);
            //interpolate
            Reading today = list.Last();
            Reading tomorrow = interpolate(direction, coefficient, today);
            Reading afterTomorrow = interpolate(direction, coefficient, tomorrow);

            IList<Reading> result = new List<Reading>
            {
                today,
                tomorrow,
                afterTomorrow
            };

            return result;
        }

        protected int getDirection(IList<Models.Reading> list)
        {
            //Temperature
            //get last
            Reading lastDay = list.Last();
            double? lastDayValue = lastDay.Temperature;
            list.Remove(lastDay);

            //get min
            double? minValue = list.Min(r => r.Temperature);

            //<=>
            if (Math.Round(Convert.ToDecimal(minValue)) < Math.Round(Convert.ToDecimal(lastDayValue))) return 1;
            if (Math.Round(Convert.ToDecimal(minValue)) > Math.Round(Convert.ToDecimal(lastDayValue))) return -1;
            return 0;

        }

        protected int getCoefficient(IList<Reading> list)
        {
            //AthmosphericPreasure
            //get last
            Reading lastDay = list.Last();
        double? lastDayValue = lastDay.AthmosphericPreasure;
        list.Remove(lastDay);

            //get min
            double? minValue = list.Min(r => r.AthmosphericPreasure);

            //<=>
            if (Math.Round(Convert.ToDecimal(minValue)) < Math.Round(Convert.ToDecimal(lastDayValue))) return 1;
            if (Math.Round(Convert.ToDecimal(minValue)) > Math.Round(Convert.ToDecimal(lastDayValue))) return -1;
            return 0;
            }

        protected Reading interpolate(int direction, int coefficient, Reading original)
        {

            return new Reading()
            {
                Temperature = original.Temperature + direction + coefficient,
                AthmosphericPreasure = original.AthmosphericPreasure + 5 * coefficient
            };
        }
    }
}
