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
    /// Logika interakcji dla klasy WindowOverlappingChangeOffice.xaml
    /// </summary>
    public partial class WindowOverlappingChangeOffice : Window
    {
        ClassOverlappingCalendar calendar;
        public WindowOverlappingChangeOffice(ClassOverlappingCalendar calendar)
        {
            InitializeComponent();
            this.calendar = calendar;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxOffice.ItemsSource = new ClassSqlCalendar().ListOfAvailableOfficeForSelectedDay(calendar.CalendarDayId);
                if (ComboBoxOffice.Items.Count == 0) throw new Exception("No available office for selected day \nContact with doctor needed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                this.Close();
            }
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxOffice.SelectedIndex==-1)
            {
                MessageBox.Show("Select office");
                return;
            }
            calendar.NewOfficeNumber = ((ClassOffice)ComboBoxOffice.SelectedItem).OfficeNumber;
            calendar.NewOfficeId = ((ClassOffice)ComboBoxOffice.SelectedItem).OfficeId;
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
