using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WeatherViewClientDB
{
    public static class DataBaseCreator
    {
        static Entities.City CreateCity(string name, double latitude, double longtitude)
        {
            return new Entities.City { Name = name, Latitude = latitude, Longitude = longtitude };
        }

        static Entities.RequestHistoryData CreateRequestHistory(DateTime dateTime, string city, string actualWeather)
        {
            return new Entities.RequestHistoryData
            {
                DateTime = dateTime,
                City = city,
                ActualWeather = actualWeather
            };
        }

        static DateTime CreateDateTimeForInsertInRequest(string actualDateTime, string formatDateTime)
        {
            return DateTime.ParseExact(actualDateTime, formatDateTime, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static void CreateDataBase()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

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


                List<Entities.RequestHistoryData> requestHistoryDatas = new List<Entities.RequestHistoryData>(9)
                {
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("03.01.2021 13:55:03", "dd.MM.yyyy HH:mm:ss"), "Оренбург", "15 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("13.01.2021 08:53:01", "dd.MM.yyyy HH:mm:ss"), "Мурманск", "-5 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("08.02.2021 13:47:03", "dd.MM.yyyy HH:mm:ss"), "Краснодар", "10 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("21.02.2021 18:04:45", "dd.MM.yyyy HH:mm:ss"), "Москва", "10 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("04.03.2021 21:01:05", "dd.MM.yyyy HH:mm:ss"), "Барнаул", "15 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("06.03.2021 03:21:08", "dd.MM.yyyy HH:mm:ss"), "Киров", "20 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("22.04.2021 02:33:21", "dd.MM.yyyy HH:mm:ss"), "Тюмень", "27 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("02.05.2021 22:13:17", "dd.MM.yyyy HH:mm:ss"), "Саратов", "30 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("07.05.2021 17:03:22", "dd.MM.yyyy HH:mm:ss"), "Киров", "15 °C"),
                    CreateRequestHistory(CreateDateTimeForInsertInRequest("18.05.2021 15:53:45", "dd.MM.yyyy HH:mm:ss"), "Омск", "22 °C")
                };

                foreach (var requestData in requestHistoryDatas)
                {
                    db.RequestHistoryDatas.Add(requestData);
                }

                db.SaveChanges();
            }
        }
    }
}
