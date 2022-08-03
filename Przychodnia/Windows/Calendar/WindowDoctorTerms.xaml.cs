using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Przychodnia.Class.Calendar;
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Logika interakcji dla klasy WindowDoctorTerms.xaml
    /// </summary>
    public partial class WindowDoctorTerms : Window
    {
        List<ClassTerm> list;
        int year;
        int month;
        public WindowDoctorTerms(List<ClassTerm> list, int year, int month)
        {
            InitializeComponent();
            this.list = list;
            this.year = year;
            this.month = month;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (list.Count == 0)
            {
                MessageBox.Show("This doctor doesn't have terms for selected calendar");
                this.Close();
                return;
            }
            TextBlockDoctor.Text = list[0].Doctor.Name +" "+ list[0].Doctor.Surname;
            Calendar.DisplayDateStart = new DateTime(year, month, 1);
            Calendar.DisplayDateEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            //List for all days in month
            List<DateTime> blackoutList = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                blackoutList.Add(new DateTime(year, month, i));
            }
            //Remove from list days with term
            foreach (var item in list)
            {
               blackoutList.Remove(new DateTime(year, month, item.Date.Day));
            }
            //Blackout dates from list
            foreach (var item in blackoutList)
            {
                CalendarDateRange dateRange = new CalendarDateRange(item);
                Calendar.BlackoutDates.Add(dateRange);
            }
            //Select first date in calendar
            Calendar.SelectedDate = new DateTime(year, month, list[0].Date.Day);
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get time for selected date 
            foreach (var item in list)
            {
                if (Calendar.SelectedDate.Value.Day != item.Date.Day) continue;
                TextBlockTime.Text = item.GetTime;
                return;
            }
            TextBlockTime.Text = "";
        }
    }
}
