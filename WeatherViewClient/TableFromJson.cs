using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherViewClient
{
    public class TableFromJson
    {
        public class JsonWeatherMainTable
        {
            public JsonWeatherInnerTable fact { get; set; }
        }

        public class JsonWeatherInnerTable
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
            public float wind_speed { get; set; }
        }
    }
}
