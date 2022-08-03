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
using Przychodnia.Windows.Visit;
using Przychodnia.Class.DictionariesHanding;
using System.Data;
using System.Data.Common;

namespace Przychodnia.Windows.Visit
{

    public partial class WindowViewListOfVisit : Page
    {
        public WindowViewListOfVisit()
        {
            InitializeComponent();
        }
        
        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToDataGridSearch();
        }
     
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Open a new window adding a new visit 
            ClassVisit visit = new ClassVisit();
            WindowVisitAdd windowVisitAdd = new WindowVisitAdd(visit);
            windowVisitAdd.ReadOnly = false;
            windowVisitAdd.AddNewVisit = true;
            bool update = (bool)windowVisitAdd.ShowDialog();
            if (update)
            {
                UpdateVisit(visit, windowVisitAdd.AddNewVisit);
                //LoadDataToDataGrid();
                LoadDataToDataGridSearch();
            }
        }
        private void UpdateVisit(ClassVisit visit, bool AddNewVisit)
        {
            try
            {
                if (!AddNewVisit)
                {
                    ClassSQLConnections.UpdateVisit(visit);
                    return;
                }
                ClassSQLConnections.AddVisit(visit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewOrEdit(sender);
        }
        private void ViewOrEdit(object sender)
        {
            Button btn = (Button)sender;
            if (btn is null && !string.IsNullOrEmpty(btn.Name))
            {
                return;
            }
            if (DataGridListOfVisit.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the visit", "No row selected");
                return;
            }
            //Open a new window with existing visit details
            ClassVisit visit = (ClassVisit)DataGridListOfVisit.SelectedItem;
            WindowVisitAdd windowVisitAdd = new WindowVisitAdd(visit);
            if (btn.Name.Equals(ButtonViewDetails.Name))
            {
                windowVisitAdd.ReadOnly = true;
            }
            else if (btn.Name.Equals(ButtonEdit.Name))
            {
                windowVisitAdd.ReadOnly = false;
            }
            windowVisitAdd.AddNewVisit = false;
            windowVisitAdd.ShowDialog();
            if ((bool)windowVisitAdd.DialogResult)
            {
                UpdateVisit(visit, windowVisitAdd.AddNewVisit);
                //LoadDataToDataGrid();
                LoadDataToDataGridSearch();
            }
            return;
        }
        private void ButtonViewDetails_Click(object sender, RoutedEventArgs e)
        {
            ViewOrEdit(sender);

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            if (DataGridListOfVisit.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the visit", "No row selected");
                return;
            }

            
            const string message = "Are you sure that you would like to cancel this appointment?";
            const string caption = "Cancel appointment";
            MessageBoxResult result = MessageBox.Show(message, caption,
                                         MessageBoxButton.YesNo);

            // If the no button was pressed ...
            if (result == MessageBoxResult.Yes)
            {
                // cancel the closure of the form.
                ClassSQLConnections.DeleteVisit(((ClassVisit)DataGridListOfVisit.SelectedItem).VisitId);
                //LoadDataToDataGrid();
                LoadDataToDataGridSearch();
            }
        }

        private void ButtonAppointmentConfirmation_Click(object sender, RoutedEventArgs e)
        {

            if (DataGridListOfVisit.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the visit", "No row selected");
                return;
            }
            string temp = ((ClassVisit)DataGridListOfVisit.SelectedItem).ToPay;
            if (temp == "Waiting") 
            {
                const string message = "Are you sure that you would like to confirm this appointment?";
                const string caption = "Appointment confirmation";
                MessageBoxResult result = MessageBox.Show(message, caption,
                                             MessageBoxButton.YesNo);

                // If the no button was pressed ...
                if (result == MessageBoxResult.Yes)
                {
                    // cancel the closure of the form.
                    ClassSQLConnections.ConfirmVisit(((ClassVisit)DataGridListOfVisit.SelectedItem).VisitId);
                    //LoadDataToDataGrid();
                    LoadDataToDataGridSearch();
                }
            }
            
        }

        private void DataGridListOfVisit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonClearOldAppointments_Click(object sender, RoutedEventArgs e)
        {
            
            //LoadDataToDataGrid();
            LoadDataToDataGridSearch();

            const string message = "Are you sure that you would like to delete old appointments?";
            const string caption = "Appointment deleting confirmation";
            MessageBoxResult result = MessageBox.Show(message, caption,
            MessageBoxButton.YesNo);

            // If the no button was pressed ...
            if (result == MessageBoxResult.Yes)
            {
                // cancel the closure of the form.
                ClassSQLConnections.DeleteOldVisit(DateTime.Today);
                //LoadDataToDataGrid();
                LoadDataToDataGridSearch();
            }
            
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDataToDataGridSearch();
        }
        void Selection_Change(Object sender, EventArgs e)
        {
            LoadDataToDataGridSearch();
        }
        private void LoadDataToDataGridSearch()
        {
            //Load data to data grid after searching
            try
            {
                string patient = TextBoxSearchPatient.Text;
                string doctor = TextBoxSearchDoctor.Text;
                string topic = TextBoxSearchTopic.Text;
                if (DatePickerStartDate.SelectedDate.HasValue & DatePickerEndDate.SelectedDate.HasValue)
                {
                    DateTime startDate = (DateTime)DatePickerStartDate.SelectedDate;
                    DateTime endDate = (DateTime)DatePickerEndDate.SelectedDate;
                    DataGridListOfVisit.ItemsSource = ClassSQLConnections.VisitListSearch(patient, doctor, topic, startDate, endDate);
                }else if (DatePickerStartDate.SelectedDate.HasValue & !DatePickerEndDate.SelectedDate.HasValue)
                {
                    DateTime startDate = (DateTime)DatePickerStartDate.SelectedDate;
                    DateTime endDate = new DateTime(3000, 6, 1, 7, 47, 0);
                    DataGridListOfVisit.ItemsSource = ClassSQLConnections.VisitListSearch(patient, doctor, topic, startDate, endDate);
                }else if (!DatePickerStartDate.SelectedDate.HasValue & DatePickerEndDate.SelectedDate.HasValue)
                {
                    DateTime startDate = new DateTime(1800, 6, 1, 7, 47, 0);
                    DateTime endDate = (DateTime)DatePickerEndDate.SelectedDate;
                    DataGridListOfVisit.ItemsSource = ClassSQLConnections.VisitListSearch(patient, doctor, topic, startDate, endDate);
                }
                else 
                {
                    DateTime startDate = new DateTime(1800, 6, 1, 7, 47, 0);
                    DateTime endDate = new DateTime(3000, 6, 1, 7, 47, 0);
                    DataGridListOfVisit.ItemsSource = ClassSQLConnections.VisitListSearch(patient, doctor, topic, startDate, endDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }
    }
}
