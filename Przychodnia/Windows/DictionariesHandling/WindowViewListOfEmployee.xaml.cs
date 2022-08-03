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
    /// Logika interakcji dla klasy WindowViewListOfEmployee.xaml
    /// </summary>
    public partial class WindowViewListOfEmployee : Page
    {
        public WindowViewListOfEmployee()
        {
            InitializeComponent();
        }
        private void LoadDataToDataGrid()
        {
            //Load data to DataGrid
            try
            {
                DataGridListOfEmployee.ItemsSource =ClassSQLConnections.EmployeeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }

       

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Open a new window adding a new office staff 
            ClassEmployee employee = new ClassEmployee();
            WindowEmployeeDataEdition windowEmployeeDataEdition = new WindowEmployeeDataEdition(employee);
            windowEmployeeDataEdition.AddNew = true;
            bool update =  (bool)windowEmployeeDataEdition.ShowDialog();
            if(update)
            {
                UpdateEmployee(employee, windowEmployeeDataEdition.AddNew);
                LoadDataToDataGrid();
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //Check if user selected doctor
            if (DataGridListOfEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the employee", "No row selected");
                return;
            }
            //Open a new window editing existing doctor 
            ClassEmployee employee = (ClassEmployee)DataGridListOfEmployee.SelectedItem;
            WindowEmployeeDataEdition windowEmployeeDataEdition = new WindowEmployeeDataEdition(employee);
            windowEmployeeDataEdition.AddNew = false;
            bool update = (bool)windowEmployeeDataEdition.ShowDialog();
            if (update)
            {
                UpdateEmployee(employee, windowEmployeeDataEdition.AddNew);
                LoadDataToDataGrid();
            }
        }

        
        //Removes doctor from Doctors table in DataBase
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            
            //Check if user selected doctor
            if (DataGridListOfEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the employee", "No row selected");
                return;
            }
            foreach (int index in ClassSQLConnections.EmployeesThatAreDoctors())
            {
                if (((ClassEmployee)DataGridListOfEmployee.SelectedItem).EmployeeId == index)
                {
                    MessageBox.Show("Doctor cannot be removed. Choose another empolyee", "Choose another employee");
                    return;
                }
            }
            try
            {
                ClassSQLConnections.DeleteEmployee(((ClassEmployee)DataGridListOfEmployee.SelectedItem).EmployeeId);
                LoadDataToDataGrid();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateEmployee(ClassEmployee employee, bool AddNew)
        {
            //Update  doctor info in data base
            try
            {
                //If user choose edit
                if (!AddNew)
                {
                    ClassSQLConnections.UpdateEmployee(employee);
                    return;
                }
                //If user choose add
                ClassSQLConnections.AddNewEmployee(employee);
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
    }
}
