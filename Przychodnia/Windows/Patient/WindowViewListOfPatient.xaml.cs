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
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Windows.Patient
{
    /// <summary>
    /// Logika interakcji dla klasy WindowViewListOfPatient.xaml
    /// </summary>
    public partial class WindowViewListOfPatient : Page
    {
        public WindowViewListOfPatient()
        {
            InitializeComponent();
        }

        private void LoadDataToDataGrid()
        {
            //Load data to DataGrid
            try
            {
                DataGridListOfPatient.ItemsSource = ClassSQLConnections.PatientList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToDataGrid();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            

            //Open a new window adding a new patient 
            ClassPatient patient = new ClassPatient();
            WindowPatientAddEdition windowPatientAddEdition = new WindowPatientAddEdition(patient);
            windowPatientAddEdition.AddNew = true;
            bool update = (bool)windowPatientAddEdition.ShowDialog();
            if (update)
            {
                UpdatePatient(patient, windowPatientAddEdition.AddNew);
                LoadDataToDataGrid();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //Check if user selected patient
            if (DataGridListOfPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the patient", "No row selected");
                return;
            }
            ClassPatient patient = (ClassPatient)DataGridListOfPatient.SelectedItem;
            WindowPatientAddEdition windowPatientAddEdition = new WindowPatientAddEdition(patient);
            windowPatientAddEdition.AddNew = false;
            windowPatientAddEdition.TextBoxPersonalIDNumber.IsEnabled = false;
            bool update = (bool)windowPatientAddEdition.ShowDialog();
            if (update)
            {
                UpdatePatient(patient, windowPatientAddEdition.AddNew);
                LoadDataToDataGrid();
            }
        }

        private void ButtonViewDetails_Click(object sender, RoutedEventArgs e)
        {
            //Check if user selected patient
            if (DataGridListOfPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the patient", "No row selected");
                return;
            }
            ClassPatient patient = (ClassPatient)DataGridListOfPatient.SelectedItem;
            WindowPatientAddEdition windowPatientAddEdition = new WindowPatientAddEdition(patient);
            windowPatientAddEdition.ReadOnly = true;
            windowPatientAddEdition.ShowDialog();

        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            //Check if user selected doctor
            if (DataGridListOfPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the patient", "No row selected");
                return;
            }
            try
            {
                ClassSQLConnections.DeletePatient(((ClassPatient)DataGridListOfPatient.SelectedItem).PatientId);
                LoadDataToDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdatePatient(ClassPatient patient, bool addNew)
        {
            //Update  doctor info in data base
            try
            {
                //If user choose edit
                if (!addNew)
                {
                    ClassSQLConnections.UpdatePatient(patient);
                    return;
                }
                //If user choose add
                ClassSQLConnections.AddNewPatient(patient);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }


    }
}
