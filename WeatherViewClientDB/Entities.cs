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
        }
       
        [Table("История запросов")]
        public class RequestHistoryStorage
        {
            public int Id { get; set; }
           
            [Column("Дата")]
            public string Date { get; set; }
            
            [Column("Время")]
            public string Time { get; set; }
           
            [Column("Город")]
            public string City { get; set; }
           
            [Column("Погода")]
            public string Weather { get; set; }
        }
    }
}
