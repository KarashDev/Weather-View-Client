using System;
using System.Linq;
using System.Windows.Controls;
using WeatherViewClientDB;

namespace WeatherViewClient
{
    public static class ComboboxDataLoader
    {
        public static void LoadComboboxData(ComboBox comboBox)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var dbCitiesNames = db.Cities.OrderBy(c => c.Name).ToList();

                comboBox.ItemsSource = dbCitiesNames;
                comboBox.DisplayMemberPath = "Name";
                comboBox.SelectedValuePath = "Id";
                comboBox.SelectedIndex = 0;
            }
        }

    }
}
