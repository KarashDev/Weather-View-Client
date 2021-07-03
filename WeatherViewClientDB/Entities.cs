using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeatherViewClientDB
{
    public class Entities
    {
        [Table("Города")]
        public class City
        {
            public int Id { get; set; }
            
            [Column("Наименование")]
            public string Name { get; set; }

            [Column("Широта")]
            public double Latitude { get; set; }
            
            [Column("Долгота")]
            public double Longitude { get; set; }
        }

        [Table("История запросов")]
        public class RequestHistoryData
        {
            public int Id { get; set; }
           
            [Column("Дата_время")]
            public DateTime DateTime { get; set; }
            
            [Column("Город")]
            public string City { get; set; }
           
            [Column("Погода")]
            public string ActualWeather { get; set; }
        }
    }
}
