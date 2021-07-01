using System;
using System.Collections.Generic;
using System.Linq;
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

        }
    }
}
