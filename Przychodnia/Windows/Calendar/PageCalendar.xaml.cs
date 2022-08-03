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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using Przychodnia.Class.Calendar;
namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Logika interakcji dla klasy PageCalendar.xaml
    /// </summary>
    public partial class PageCalendar : Page
    {
        ClassSqlCalendar sql = new ClassSqlCalendar();
        WindowAdministrator windowAdministrator;
        public PageCalendar(WindowAdministrator windowAdministrator)
        {
            InitializeComponent();
            this.windowAdministrator = windowAdministrator;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxPickDate.ItemsSource = sql.CalendarList();
                if (ComboBoxPickDate.Items.Count == 0) return;
                ComboBoxPickDate.SelectedIndex = ComboBoxPickDate.Items.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboBoxPickDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadData();
        }

        #region Calendar handling
        //Generate calendar
        private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            try
            {
                IEnumerable<ClassCalendar> query =
                from elem in sql.CalendarList()
                where elem.Year == year && elem.Month==month
                select elem;
                if (query.Any()) throw new Exception("Calendar already created");

                ClassGenerateCalendar.GenerateCalendar(DateTime.Now.Year, DateTime.Now.Month + 1, sql);
                MessageBox.Show("Succesfully generated calendar");

                ComboBoxPickDate.ItemsSource = sql.CalendarList();
                if (ComboBoxPickDate.Items.Count == 0) return;
                ComboBoxPickDate.SelectedIndex = ComboBoxPickDate.Items.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        //Share calendar
        private void ButtonShare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassCalendar calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                if (calendar.Status.StatusId != sql.SelectStatusId(EnumStatus.New)) throw new Exception("Calendar status must be new");
                ClassGenerateCalendar.ShareCalendar(calendar,sql);
                MessageBox.Show("Succesfully shared calendar");

                ComboBoxPickDate.ItemsSource = sql.CalendarList();
                if (ComboBoxPickDate.Items.Count == 0) return;
                ComboBoxPickDate.SelectedIndex = ComboBoxPickDate.Items.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        //Verify calendar
        private void ButtonVerified_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassCalendar calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                if (!(calendar.Status.StatusId == sql.SelectStatusId(EnumStatus.DuringVerification))) throw new Exception("Calendar status must be during verification");
                OverlappingCalendar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        //Remove calendar
        private void ButtonRemoveCalendar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassCalendar callendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                if (callendar.Status.StatusId != sql.SelectStatusId(EnumStatus.New)) throw new Exception("To remove calendar status must be new");
                sql.DeleteCalendarAndApropriateCalendarDays(callendar.CalendarId);
                MessageBox.Show("Calendar succesfully deleted");
                ComboBoxPickDate.ItemsSource = sql.CalendarList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 

        }
        #endregion

        #region Edition of calendar days
        //Add or edit calendar day
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassCalendar calendar;
            ClassCalendarDay day;
            try
            {
                calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                if (calendar.Status.StatusId != sql.SelectStatusId(EnumStatus.New)) throw new Exception("Calendar status must be new");
                day = GetCalendarDayOrGenerateIt(sender,calendar);
                WindowCalendarDayEdition window = new WindowCalendarDayEdition(day, calendar.Year, calendar.Month, sql.ListOfCalendarDays(calendar.CalendarId));
                if (!(sender is Button)) window.createNew = false;                
                bool update = (bool)window.ShowDialog();
                if (!update) throw new Exception("Operation canceled");
                if(sender is Button)
                {
                    sql.CreateCalendarDay(day.Day, calendar.CalendarId, day.StartTime, day.EndTime);
                    MessageBox.Show("Calendar day succesfully added");
                    ReloadData();
                    return;
                }
                sql.UpdateCalendarDay(day.Day, day.CalendarDayId, day.StartTime, day.EndTime);
                MessageBox.Show("Calendar day succesfully edited");
                ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }
        //Remove calendar day
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassCalendar calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                if (calendar.Status.StatusId != sql.SelectStatusId(EnumStatus.New)) throw new Exception("Calendar status must be new");
                ClassCalendarDay  day = GetCalendarDayForSelectedCalendarDayInDataGridFromDataBase(calendar.CalendarId);
                if (!(MessageBox.Show("Are you sure you want to delete this day?", "Continue?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)) return;
                sql.DeleteCalendarDay(day.CalendarDayId);
                ReloadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Terms for doctor
        private void DataGridListOfCalendarDoctor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ClassCalendar calendar= GetCalendarForSelectedCalendarInComboboxFromDataBase();
                ClassCalendarDoctor calendarDoctor = GetCalendarDoctorForSelectedCalendarDoctorInDataGridFromDataBase(calendar.CalendarId);
                WindowDoctorTerms windowDoctorTerms = new WindowDoctorTerms(sql.TermLisTSelectedDoctor(calendar.CalendarId, calendarDoctor.Doctor.Doctor_id), calendar.Year, calendar.Month);
                windowDoctorTerms.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion

        #region Methods
        private void ReloadData()
        {
            if (ComboBoxPickDate.SelectedIndex == -1)
            {
                DataGridListOfDoctors.ItemsSource = null;
                DataGridListOfCalendarDoctor.ItemsSource = null;
                Status_Label.Text = "...";
                return;
            }
            try
            {
                ClassCalendar calendar = GetCalendarForSelectedCalendarInComboboxFromDataBase();
                DataGridListOfDoctors.ItemsSource = sql.ListOfCalendarDays(calendar.CalendarId);
                DataGridListOfCalendarDoctor.ItemsSource = sql.ListOfCalendarDoctor(calendar.CalendarId);
                LoadStatusToLabel(calendar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadStatusToLabel(ClassCalendar calendar)
        {
            IEnumerable<ClassStatus> query =
              from elem in sql.StatusList()
              where elem.StatusId == calendar.Status.StatusId
              select elem;
            if (!query.Any()) throw new Exception("Unable to find apropriate status in database");
            Status_Label.Text =  query.First().Status;

            //Check if update status to Waitng to Veryfy
            if (Status_Label.Text == ClassStatus.StatusString(EnumStatus.SharedForDoctors))
            {
                sql.UpdateCalendar(calendar.CalendarId);
                List<ClassCalendar> list = sql.CalendarList();
                if(calendar.Status.StatusId != GetCalendarForSelectedCalendarInComboboxFromDataBase().Status.StatusId)
                {
                    ComboBoxPickDate.ItemsSource = sql.CalendarList();
                    if (ComboBoxPickDate.Items.Count == 0) return;
                    ComboBoxPickDate.SelectedIndex = ComboBoxPickDate.Items.Count - 1;
                }
            }
        }

        private void OverlappingCalendar()
        {
            ClassCalendar calendar= GetCalendarForSelectedCalendarInComboboxFromDataBase();
            List<ClassOverlappingCalendar> list = sql.OverlappingTerms(calendar.CalendarId);
            if (list.Count != 0)
            {
                //Need changes in offices
                WindowOverlapping windowOverlapping = new WindowOverlapping(list, calendar.CalendarId);
                bool update = (bool)windowOverlapping.ShowDialog();
                if(!update) return;
                foreach (var item in list)
                {
                    if (item.NewOfficeNumber == 0) continue;
                    sql.UpdateOffice(item.TermId, item.NewOfficeId);
                }
                return;
            }
            //Calendar verified
            sql.UpdateCalendarStatus(4, calendar.CalendarId);
            MessageBox.Show("Succesfully veryfied", "Succes");
            ComboBoxPickDate.ItemsSource = sql.CalendarList();
            if (ComboBoxPickDate.Items.Count == 0) return;
            ComboBoxPickDate.SelectedIndex = ComboBoxPickDate.Items.Count - 1;
            return;
        }

        private ClassCalendarDay GetCalendarDayOrGenerateIt(object sender, ClassCalendar calendar)
        {
            if (!(sender is Button))
            {
                return GetCalendarDayForSelectedCalendarDayInDataGridFromDataBase(calendar.CalendarId);
            }
            ClassCalendarDay day = new ClassCalendarDay();
            day.Calendar = new ClassCalendar();
            day.Calendar.CalendarId = calendar.CalendarId;
            day.StartTime = new TimeSpan(7, 0, 0);
            day.EndTime = new TimeSpan(20, 0, 0);
            day.Day = 1;
            int year = ((ClassCalendar)ComboBoxPickDate.SelectedItem).Year;
            int month = ((ClassCalendar)ComboBoxPickDate.SelectedItem).Month;
            return day;
        }

        private ClassCalendar GetCalendarForSelectedCalendarInComboboxFromDataBase()
        {
            if (ComboBoxPickDate.SelectedIndex == -1) throw new Exception("Calendar isn't selected");

            //Get calendar from data base
            IEnumerable<ClassCalendar> query =
               from elem in sql.CalendarList()
               where elem.CalendarId == ((ClassCalendar)ComboBoxPickDate.SelectedItem).CalendarId
               select elem;
            if (!query.Any()) throw new Exception("Unable to find selected calendar in database");
            return query.First();
        }

        private ClassCalendarDay GetCalendarDayForSelectedCalendarDayInDataGridFromDataBase(int calendarId)
        {
            if (DataGridListOfDoctors.SelectedIndex == -1) throw new Exception("Data grid row isn't selected");
            //Get calendar from data base
            IEnumerable<ClassCalendarDay> query =
               from elem in sql.ListOfCalendarDays( calendarId)
               where elem.CalendarDayId == ((ClassCalendarDay)DataGridListOfDoctors.SelectedItem).CalendarDayId
               select elem;
            if (!query.Any()) throw new Exception("Unable to find selected calendar day in database");
            return query.First();
        }

        private ClassCalendarDoctor GetCalendarDoctorForSelectedCalendarDoctorInDataGridFromDataBase(int calendarId)
        {
            if (DataGridListOfCalendarDoctor.SelectedIndex == -1) throw new Exception("Data grid row isn't selected");
            //Get calendar from data base
            IEnumerable<ClassCalendarDoctor> query =
               from elem in sql.ListOfCalendarDoctor(calendarId)
               where elem.CalendarDoctorId == ((ClassCalendarDoctor)DataGridListOfCalendarDoctor.SelectedItem).CalendarDoctorId
               select elem;
            if (!query.Any()) throw new Exception("Unable to find selected calendar doctor in database");
            return query.First();
        }
        #endregion
    }
}
