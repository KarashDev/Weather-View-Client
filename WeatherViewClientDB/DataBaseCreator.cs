using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WeatherViewClientDB
{
    public static class DataBaseCreator
    {
        public static void CreateDataBase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<string> cities = new List<string>
                {
                    "Москва", "Санкт-Петербург",  "Рязань", "Екатеринбург", "Казань", "Челябинск", "Уфа",
                    "Омск", "Волгоград", "Краснодар", "Барнаул", "Мурманск", "Владивосток", "Оренбург",
                    "Пермь","Киров", "Иркутск", "Тюмень", "Саратов"
                };

                foreach (var cityName in cities)
                {
                    db.Cities.Add(new Entities.City {Name = cityName});
                }
               
                db.SaveChanges();

            }
        }
    }
}
