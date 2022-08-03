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
using Przychodnia.Class.Calendar;
using System.Linq;

namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Interaction logic for WindowViewListOfOffice.xaml
    /// </summary>
    public partial class PageViewListOfOffice : Page
    {
        public PageViewListOfOffice()
        {
            InitializeComponent();

        }
        private void LoadDataToDataGrid()
        {
            //Load data to DataGrid
            try
            {              
                DataGridListOfOffice.ItemsSource = ClassSQLConnections.OfficeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassOffice office = new ClassOffice();
            WindowOfficeDataEdition windowOfficeDataEdition = new WindowOfficeDataEdition(office);
            windowOfficeDataEdition.AddNew = true;
            bool update = (bool)windowOfficeDataEdition.ShowDialog();
            if (update)
            {
                UpdateOffice(office, windowOfficeDataEdition.AddNew);
                LoadDataToDataGrid();
            }
        }

        

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridListOfOffice.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the office", "No row selected");
                return;
            }
            //Open a new window editing existing doctor 
            ClassOffice office = (ClassOffice)DataGridListOfOffice.SelectedItem;
            WindowOfficeDataEdition windowOfficeDataEdition = new WindowOfficeDataEdition(office);
            windowOfficeDataEdition.AddNew = false;
            bool update = (bool)windowOfficeDataEdition.ShowDialog();
            if (update)
            {
                UpdateOffice(office, windowOfficeDataEdition.AddNew);
                LoadDataToDataGrid();
            }
        }
        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            //Check if user selected doctor
            if (DataGridListOfOffice.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the office", "No row selected");
                return;
            }
            try
            {
                if (ConditionDeleteOffice())
                {
                ClassSQLConnections.DeleteOffice(((ClassOffice)DataGridListOfOffice.SelectedItem).OfficeId);
                }
                LoadDataToDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool ConditionDeleteOffice()
        {
            IEnumerable<ClassTerm> query =
               from elem in new ClassSqlCalendar().ListOfTerms()
               where elem.Office.OfficeId == ((ClassOffice)DataGridListOfOffice.SelectedItem).OfficeId
               select elem;
            if (query.Any())
            {
                string data = "This office cannot be removed because at this days meetings are being hosted in said of it: \n";
                foreach (ClassTerm queryResult in query)
                {
                    data += queryResult.Date.ToShortDateString() + "\n";
                }
                MessageBox.Show(data);
                return false;
            }

            IEnumerable<ClassDoctor> query2 =
                from elem in ClassSQLConnections.DoctorList()
                where elem.OfficeNumber == ((ClassOffice)DataGridListOfOffice.SelectedItem).OfficeId
                select elem;
            if (query2.Any())
            {
                MessageBox.Show("This Office is currently occupied by " + query2.First().Name + " " + query2.First().Surname);
                return false;
            }
            return true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToDataGrid();
        }

        private void UpdateOffice(ClassOffice office, bool AddNew)
        {
            //Update  doctor info in data base
            try
            {
                //If user choose edit
                if (!AddNew)
                {
                    ClassSQLConnections.UpdateOffice(office);
                    return;
                }
                //If user choose add
                ClassSQLConnections.AddNewOffice(office);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }
    }
}