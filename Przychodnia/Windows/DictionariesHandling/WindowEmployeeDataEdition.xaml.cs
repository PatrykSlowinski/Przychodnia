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
    /// Logika interakcji dla klasy WindowEmployeeDataEdition.xaml
    /// </summary>
    public partial class WindowEmployeeDataEdition : Window
    {
        #region Declare variables
        //Office staff data are inside variable "OfficeStaff"
        private ClassEmployee employee;
        //Defines if update office staff or add new office staff
        private bool addNew;
        //List that contains users that are not defined in tbl: User, OfficeStaff, Administrator  
        public bool AddNew { get => addNew; set => addNew = value; }
        #endregion
        public WindowEmployeeDataEdition(ClassEmployee employee)
        {
            InitializeComponent();
            this.employee = employee;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AddNew)
            {
                //Hide fields that are unused during editing employee
                HideUnusedFields();
            }
                InsertUser();
                LoadData();
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckIfValidData())
            {
                return;
            }
            SetData();
            this.DialogResult = true;
          
        }

        #region Methods used in Window_Loaded
        private void InsertUser()
        {
            //Insert users into combobox
            try
            {
                //Load not defined user list to combobox
                foreach (ClassUser user in ClassSQLConnections.NotSpecifiedUsers())
                {
                    ComboBoxUser.Items.Add(user);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

        }
        //Hides fields that are unused
        private void HideUnusedFields()
        {
            //Hide unused fields
            Pesel.Visibility = Visibility.Collapsed;
            User.Visibility = Visibility.Collapsed;
            this.Height = this.Height - 70;
        }
        //Load data from Employee
        private void LoadData()
        {
            
            TextBoxName.Text = employee.Name;
            TextBoxSurname.Text = employee.Surname;
            TextBoxPhoneNumber.Text = employee.PhoneNumber;
            if (employee.DateOfBirth != DateTime.MinValue)
            {
                DatePickerDateOfBirth.SelectedDate = employee.DateOfBirth;
            }
        }
        #endregion

        #region Method used in ButtonSubmit_Click
       

        //Checks if data is valid 
        private bool CheckIfValidData()
        {
            if (TextBoxName.Text.Length == 0)
            {
                MessageBox.Show("Input name", "Invalid data");
                return false;
            }
            if (TextBoxSurname.Text.Length == 0)
            {
                MessageBox.Show("Input surname", "Invalid data");
                return false;
            }
            if (!Class.Login.ClassHelpers.IsValidNumberLenght(TextBoxPhoneNumber.Text, 9))
            {
                MessageBox.Show("Phone number must be 9 digits long", "Invalid phone number");
                return false;
            }
            if (DatePickerDateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Select date", "Select date");
                return false;
            }
            if(DatePickerDateOfBirth.SelectedDate >= DateTime.Now.Date)
            {
                MessageBox.Show("Select valid date", "Select date");
                return false;
            }

            //Additional check if add new office staff
            if (AddNew)
            {
                if (!Class.Login.ClassHelpers.IsValidNumberLenght(TextBoxPesel.Text, 11))
                {
                    MessageBox.Show("Pesel must be 11 digits long", "Invalid pesel");
                    return false;
                }
                if (ComboBoxUser.SelectedIndex == -1)
                {
                    MessageBox.Show("Select user", "User was not selected");
                    return false;
                }
            }
            return true;
        }
        
        //sets data for new office class member
        private void SetData()
        {
            //Set data
            employee.Name = TextBoxName.Text;
            employee.Surname = TextBoxSurname.Text;
            employee.PhoneNumber = TextBoxPhoneNumber.Text;
            employee.DateOfBirth = (DateTime)DatePickerDateOfBirth.SelectedDate;

            //Additional set if add new office staff
            if (AddNew)
            {
                employee.PersonalIdentityNumber = TextBoxPesel.Text;
                employee.User_id = ((ClassUser)ComboBoxUser.SelectedItem).User_id;
            }
        }
        #endregion

    }
}
