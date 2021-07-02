using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WeatherViewClientDB;

namespace WeatherViewClient
{
    class JsonWeatherMainTable
    {
        public JsonWeatherInnerTable fact { get; set; }
    }

    class JsonWeatherInnerTable
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float wind_speed { get; set; }
    }
    public class StringWrapper
    {
        public string Value { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            DataBaseCreator.CreateDataBase();

            using (ApplicationContext db = new ApplicationContext())
            {
                var dbCitiesNames = db.Cities.OrderBy(c => c.Name).ToList();

                CBoxChooseCity.ItemsSource = dbCitiesNames;
                CBoxChooseCity.DisplayMemberPath = "Name";
                CBoxChooseCity.SelectedValuePath = "Id";
                CBoxChooseCity.SelectedIndex = 0;

            }
        }

        private void BtnShowWeather_Click(object sender, RoutedEventArgs e)
        {

            string weatherApiUrl;

            using (ApplicationContext db = new ApplicationContext())
            {
                Entities.City chosenCity = db.Cities.FirstOrDefault(c => c.Name == CBoxChooseCity.Text);
                var cityLatitude = chosenCity.Latitude; 
                var cityLongitude = chosenCity.Longitude;

                // Среда при интерполяции конвертировала числа в строку, заменяя у них точку на запятую,
                // из-за этого запрос не выполнялся, выкидывая ошибку 400. Стало работать только с InvariantCulture.
                weatherApiUrl = String.Format(CultureInfo.InvariantCulture, "https://api.weather.yandex.ru/v2/informers?lat={0}&lon={1}", cityLatitude, cityLongitude);
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(weatherApiUrl);

            httpWebRequest.Headers["X-Yandex-API-Key"] = "131eef81-c0fa-4c58-8b8e-d9e20983bbc6";

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string jsonResponse;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                jsonResponse = streamReader.ReadToEnd();
            }

            JsonWeatherMainTable weatherResponse = JsonConvert.DeserializeObject<JsonWeatherMainTable>(jsonResponse);

            var s1 = $"Температура фактическая : {weatherResponse.fact.temp} °C";
            var s2 = $"Ощущается как : {weatherResponse.fact.feels_like} °C";
            var s3 = $"Скорость ветра : {weatherResponse.fact.wind_speed} м/c";

            //List<string> listForDataGrid = new List<string>(3) { s1, s2, s3 };
            List<StringWrapper> listForDataGrid = new List<StringWrapper>(3) 
            {  
                new StringWrapper{Value = s1},
                new StringWrapper{Value = s2},
                new StringWrapper{Value = s3}
            };

            //ObservableCollection<string> collection = new ObservableCollection<string>() { s1, s2, s3 };
            var x = listForDataGrid.Where(d => d.Value.Length > 0).ToList();
            DataGridActWeather.ItemsSource = x;









        }

        private void BtnOpenHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
