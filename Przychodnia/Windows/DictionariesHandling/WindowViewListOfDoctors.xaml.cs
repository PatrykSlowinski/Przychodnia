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
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Logika interakcji dla klasy WindowViewListOfDoctors.xaml
    /// </summary>
    public partial class WindowViewListOfDoctors : Page
    {
        public WindowViewListOfDoctors()
        {
            InitializeComponent();
        }

        private void LoadDataToDataGrid()
        {
            //Load data to DataGrid
            try
            {
                DataGridListOfDoctors.ItemsSource = ClassSQLConnections.DoctorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Open a new window adding a new doctor 
            ClassDoctor doc = new ClassDoctor();
            WindowDoctorDataEdition windowDoctorDataEdition = new WindowDoctorDataEdition(doc);
            windowDoctorDataEdition.ReadOnly = false;
            windowDoctorDataEdition.AddNewDoctor = true;
            bool update = (bool)windowDoctorDataEdition.ShowDialog();
            if(update)
            {
                UpdateDoctor(doc,windowDoctorDataEdition.AddNewDoctor);
                LoadDataToDataGrid();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewOrEdit(sender);
        }

        private void ButtonViewDetails_Click(object sender, RoutedEventArgs e)
        {
            ViewOrEdit(sender);
        }
        private void UpdateDoctor(ClassDoctor doctor, bool AddNewDoctor)
        {
            try
            {
                if (!AddNewDoctor)
                {
                    ClassSQLConnections.UpdateDoctor(doctor);
                    return;
                }
                ClassSQLConnections.AddDoctor(doctor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }
        private void ViewOrEdit(object sender)
        {
            Button btn = (Button)sender;
            if (btn is null && !string.IsNullOrEmpty(btn.Name))
            {
                return;
            }
            if (DataGridListOfDoctors.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the doctor", "No row selected");
                return;
            }
            //Open a new window with existing doctor details
            ClassDoctor doc = (ClassDoctor)DataGridListOfDoctors.SelectedItem;
            WindowDoctorDataEdition windowDoctorDataEdition = new WindowDoctorDataEdition(doc);
            if (btn.Name.Equals(ButtonViewDetails.Name))
            {
                windowDoctorDataEdition.ReadOnly = true;
            }
            else if (btn.Name.Equals(ButtonEdit.Name))
            {
                windowDoctorDataEdition.ReadOnly = false;
            }
            windowDoctorDataEdition.AddNewDoctor = false;
            windowDoctorDataEdition.ShowDialog();
            if ((bool)windowDoctorDataEdition.DialogResult)
            {
                UpdateDoctor(doc, windowDoctorDataEdition.AddNewDoctor);
                LoadDataToDataGrid();
            }
            return;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToDataGrid();
        }
    }
}
