using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using WeatherViewClientDB;

namespace WeatherViewClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DataBaseCreator.CreateDataBase();

            ComboboxDataLoader.LoadComboboxData(CBoxChooseCity);
        }

        private void BtnShowWeather_Click(object sender, RoutedEventArgs e)
        {
            string weatherApiUrl;
            string jsonResponseFromServer;
            TableFromJson.JsonWeatherMainTable weatherResponse;

            using (ApplicationContext db = new ApplicationContext())
            {
                Entities.City chosenCity = db.Cities.FirstOrDefault(c => c.Name == CBoxChooseCity.Text);
                var cityLatitude = chosenCity.Latitude;
                var cityLongitude = chosenCity.Longitude;

                weatherApiUrl = String.Format(CultureInfo.InvariantCulture, "https://api.weather.yandex.ru/v2/informers?lat={0}&lon={1}", cityLatitude, cityLongitude);

                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(weatherApiUrl);

                    httpWebRequest.Headers["X-Yandex-API-Key"] = "131eef81-c0fa-4c58-8b8e-d9e20983bbc6";

                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        jsonResponseFromServer = streamReader.ReadToEnd();
                    }
                    
                    weatherResponse = JsonConvert.DeserializeObject<TableFromJson.JsonWeatherMainTable>(jsonResponseFromServer);

                    
                    TextBlockActWeather.Text = $"Температура фактическая : {weatherResponse.fact.temp} °C";
                    TextBlockFeelWeather.Text = $"Ощущается как : {weatherResponse.fact.feels_like} °C";
                    TextBlockWindSpeed.Text = $"Скорость ветра : {weatherResponse.fact.wind_speed} м/c";

                   
                    var stringForSaveInHistory = $"{weatherResponse.fact.temp} °C";
                    Entities.RequestHistoryData requestHistoryData = new Entities.RequestHistoryData()
                    {
                        DateTime = DateTime.Now,
                        City = CBoxChooseCity.Text,
                        ActualWeather = stringForSaveInHistory
                    };
                    db.Add(requestHistoryData);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось установить соединение с поставщиком информации. Имейте ввиду, что действует лимит запросов: 50 в сутки", "Ошибка подключения");
                }
            }
        }

        private void BtnOpenHistory_Click(object sender, RoutedEventArgs e)
        {
            RequestHistoryWindow requestHistoryWindow = new RequestHistoryWindow();
            requestHistoryWindow.Show();
        }
    }
}
