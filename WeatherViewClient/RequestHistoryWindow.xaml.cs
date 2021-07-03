using GSF.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WeatherViewClientDB;

namespace WeatherViewClient
{
    /// <summary>
    /// Interaction logic for RequestHistoryWindow.xaml
    /// </summary>
    public partial class RequestHistoryWindow : Window
    {
        public RequestHistoryWindow()
        {
            InitializeComponent();

            ComboboxDataLoader.LoadComboboxData(CBoxChooseCity);
        }

        private void BtnShowHistory_Click(object sender, RoutedEventArgs e)
        {
            DateTime? pickerDateFrom = DatePickerFrom.SelectedDate;
            DateTime? pickerDateTill = DatePickerTill.SelectedDate;

            using (ApplicationContext db = new ApplicationContext())
            {
                Entities.City chosenCity = db.Cities.FirstOrDefault(c => c.Name == CBoxChooseCity.Text);
                if (pickerDateFrom != null && pickerDateTill != null)
                {
                    var dateMatchingRequests = db.RequestHistoryDatas.Where(r => r.DateTime >= pickerDateFrom && r.DateTime <= pickerDateTill && r.City == chosenCity.Name);

                    // Для того чтобы вывести в разных столбцах и Дату и Время в DataGrid, необходимо разделить
                    // поле DateTime на 2 поля (со временем и с датой). Для реализации этой задачи я перекидываю данные с 
                    // оригинальной сущности RequestHistoryData в созданный класс с разделенными полями даты и времени
                    // + в этом классе я сразу же сделал поля русскими для вывода столбцов таблицы на русском языке
                    var finalDataForGrid = dateMatchingRequests.Select(reqHistory => new TableForDataGrid
                    {
                        Дата = reqHistory.DateTime.ToString("dd.MM.yyyy"),
                        Время = reqHistory.DateTime.ToString("HH:mm:ss"),
                        Город = reqHistory.City,
                        Погода = reqHistory.ActualWeather
                    });

                    if (finalDataForGrid.Any())
                    {
                        DataGridShowHistory.ItemsSource = finalDataForGrid.ToList();
                    }
                    else MessageBox.Show("По указанным данным записей не найдено", "Данные не обнаружены");
                }
                else MessageBox.Show("Должны быть указаны обе даты", "Ошибка заполнения");
            }
        }
    }
}
