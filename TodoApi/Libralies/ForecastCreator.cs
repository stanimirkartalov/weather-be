using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Libralies
{
    public class ForecastCreator
    {
        public ForecastCreator()
        {
        }

        public IList<Models.Reading> makeForecast(IList<Models.Reading> list)
        {
            //get direction
            int direction = getDirection(list);
            int coefficient = getCoefficient(list);
            //interpolate
            Models.Reading today = list.Last();
            Models.Reading tomorrow = interpolate(direction, coefficient, today);
            Models.Reading afterTomorrow = interpolate(direction, coefficient, tomorrow);

            IList<Models.Reading> result = new List<Models.Reading>();
            result.Add(today);
            result.Add(tomorrow);
            result.Add(afterTomorrow);

            return result;
        }

        protected int getDirection(IList<Models.Reading> list)
        {
            //Temperature
            //get last
            Models.Reading lastDay = list.Last();
            double? lastDayValue = lastDay.Temperature;
            list.Remove(lastDay);

            //get min
            double? minValue = list.Min(r => r.Temperature);

            //<=>
            if (Math.Round(Convert.ToDecimal(minValue)) < Math.Round(Convert.ToDecimal(lastDayValue))) return 1;
            if (Math.Round(Convert.ToDecimal(minValue)) > Math.Round(Convert.ToDecimal(lastDayValue))) return -1;
            return 0;

        }

        protected int getCoefficient(IList<Models.Reading> list)
        {
            //AthmosphericPreasure
            //get last
            Models.Reading lastDay = list.Last();
        double? lastDayValue = lastDay.AthmosphericPreasure;
        list.Remove(lastDay);

            //get min
            double? minValue = list.Min(r => r.AthmosphericPreasure);

            //<=>
            if (Math.Round(Convert.ToDecimal(minValue)) < Math.Round(Convert.ToDecimal(lastDayValue))) return 1;
            if (Math.Round(Convert.ToDecimal(minValue)) > Math.Round(Convert.ToDecimal(lastDayValue))) return -1;
            return 0;
            }

        protected Models.Reading interpolate(int direction, int coefficient, Models.Reading original)
        {

            return new Models.Reading()
            {
                Temperature = original.Temperature + direction + coefficient,
                AthmosphericPreasure = original.AthmosphericPreasure + 5 * coefficient
            };
        }
    }
}
