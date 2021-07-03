using System;
using Microsoft.EntityFrameworkCore;
using static WeatherViewClientDB.Entities;

namespace WeatherViewClientDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<RequestHistoryData> RequestHistoryDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=WeatherViewClientDB;Integrated Security=True");
        }
    }
}
