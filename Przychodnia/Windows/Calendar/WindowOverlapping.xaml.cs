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
using System.Linq;
namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Logika interakcji dla klasy WindowOverlapping.xaml
    /// </summary>
    public partial class WindowOverlapping : Window
    {
        List<ClassOverlappingCalendar> list;
        int calendarId;
        public WindowOverlapping(List<ClassOverlappingCalendar> list, int calendarId)
        {
            InitializeComponent();
            this.list = list;
            this.calendarId = calendarId;
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if(!(MessageBox.Show("Cancel changing offices?","Are you sure", MessageBoxButton.YesNo)==MessageBoxResult.Yes)) return;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridListOfDoctors.ItemsSource = list;
            MessageBox.Show("Change offices for showed terms and then press button \"Verify\" again","Veryfication failed");
        }

        private void DataGridListOfDoctors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClassOverlappingCalendar calendar;
            try
            {
                calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                WindowOverlappingChangeOffice changeOffice = new WindowOverlappingChangeOffice(calendar);
                changeOffice.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
            
            foreach (var item in list)
            {
                if (calendar.TermId != item.TermId) continue;
                item.NewOfficeId = calendar.NewOfficeId;
                item.NewOfficeNumber = calendar.NewOfficeNumber;
                break;
            }
            DataGridListOfDoctors.ItemsSource = null;
            DataGridListOfDoctors.ItemsSource = list;
        }
        private ClassOverlappingCalendar GetCalendarForSelectedCalendarInComboboxFromDataBase()
        {
            if (DataGridListOfDoctors.SelectedIndex == -1) throw new Exception("Day isn't selected");
            //Get overlapping from data base
            IEnumerable<ClassOverlappingCalendar> query =
               from elem in new ClassSqlCalendar().OverlappingTerms(calendarId)
               where elem.TermId == (((ClassOverlappingCalendar)DataGridListOfDoctors.SelectedItem)).TermId
               select elem;
            if (!query.Any()) throw new Exception("Unable to find selected day in database");
            return query.First();
        }
    }
}
