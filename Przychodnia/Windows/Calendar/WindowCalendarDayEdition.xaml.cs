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
namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Logika interakcji dla klasy WindowCalendarDayEdition.xaml
    /// </summary>
    public partial class WindowCalendarDayEdition : Window
    {
        #region Declare variables
        private DateTime? start;
        public DateTime? Start
        {
            get { return start; }
            set
            {
                start = value;

            }
        }
        private DateTime? end;
        public DateTime? End
        {
            get { return end; }
            set
            {
                end = value;

            }
        }
        ClassCalendarDay CalendarDay;
        int year;
        int month;
        List<ClassCalendarDay> list;
        public bool createNew=true;
        #endregion
        public WindowCalendarDayEdition(ClassCalendarDay CalendarDay, int year, int month, List<ClassCalendarDay> list)
        {
            InitializeComponent();
            this.CalendarDay = CalendarDay;
            this.year = year;
            this.month = month;
            this.list = list;
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Start = Convert.ToDateTime(CalendarDay.StartTime.ToString());
            End = Convert.ToDateTime(CalendarDay.EndTime.ToString());
            DatePick.SelectedDate = new DateTime(year, month, CalendarDay.Day);
            StartTimePicker.SelectedTime = Start;
            EndTimePicker.SelectedTime = End;
            if (createNew)
            {
                return;
            }
            DatePick.IsEnabled = false;
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (DatePick.SelectedDate.Value.Year != year)
            {
                MessageBox.Show("Invalid Year");
                return;
            }
            if (DatePick.SelectedDate.Value.Month != month)
            {
                MessageBox.Show("Invalid month");
                return;
            }
            if(Start.Value>=End.Value)
            {
                MessageBox.Show("Start time must be before end time");
                return;
            }
            if(createNew)
            {
                foreach (ClassCalendarDay item in list)
                {
                    if (item.Day == DatePick.SelectedDate.Value.Day)
                    {
                        MessageBox.Show("Selected day already in calendar");
                        return;
                    }
                }
            }
            CalendarDay.Day = DatePick.SelectedDate.Value.Day;
            CalendarDay.StartTime = new TimeSpan(Start.Value.Hour, Start.Value.Minute, Start.Value.Second);
            CalendarDay.EndTime = new TimeSpan(End.Value.Hour, End.Value.Minute, End.Value.Second);
            this.DialogResult = true;
            this.Close();
        }
    }
}
