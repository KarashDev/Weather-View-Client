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

                //List<string> cities = new List<string>
                //{
                //    "Москва", "Санкт-Петербург",  "Рязань", "Екатеринбург", "Казань", "Челябинск", "Уфа",
                //    "Омск", "Волгоград", "Краснодар", "Барнаул", "Мурманск", "Владивосток", "Оренбург",
                //    "Пермь","Киров", "Иркутск", "Тюмень", "Саратов"
                //};

                //foreach (var cityName in cities)
                //{
                //    db.Cities.Add(new Entities.City {Name = cityName});
                //}

                static Entities.City CreateCity(string name, double latitude, double longtitude)
                {
                    return new Entities.City { Name = name, Latitude = latitude, Longitude = longtitude };
                }


                List<Entities.City> cities = new List<Entities.City>
                {
                    CreateCity("Москва", 55.45, 37.37),
                    CreateCity("Рязань", 54.36, 39.42),
                    CreateCity("Екатеринбург", 56.51, 60.36),
                    CreateCity("Казань", 55.47, 49.10),
                    CreateCity("Челябинск", 55.9, 61.26),
                    CreateCity("Уфа", 54.9, 56.4),
                    CreateCity("Омск", 54.59, 73.22),
                    CreateCity("Волгоград", 48.43, 44.29),
                    CreateCity("Краснодар", 45.2, 38.58),
                    CreateCity("Барнаул", 53.21, 83.45),
                    CreateCity("Мурманск", 68.5, 33.05),
                    CreateCity("Владивосток", 43.07, 131.54),
                    CreateCity("Оренбург", 51.46, 55.06),
                    CreateCity("Пермь", 58.0, 56.14),
                    CreateCity("Киров", 58.36, 49.39),
                    CreateCity("Иркутск", 58.0, 56.14),
                    CreateCity("Тюмень", 57.09, 65.32),
                    CreateCity("Саратов", 54.47, 32.3)
                };

                foreach (var city in cities)
                {
                    db.Cities.Add(city);
                }

                db.SaveChanges();

            }
        }
    }
}
