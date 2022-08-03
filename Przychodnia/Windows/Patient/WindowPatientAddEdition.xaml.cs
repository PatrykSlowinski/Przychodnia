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

namespace Przychodnia.Windows.Patient
{
    /// <summary>
    /// Logika interakcji dla klasy WindowPatientAddEdition.xaml
    /// </summary>
    public partial class WindowPatientAddEdition : Window
    {
        #region Declare variables
        //Office staff data are inside variable "OfficeStaff"
        private ClassPatient patient;
        //Defines if update office staff or add new office staff
        private bool addNew;
        //List that contains users that are not defined in tbl: User, OfficeStaff, Administrator  
        public bool AddNew { get => addNew; set => addNew = value; }
        private bool readOnly = false;
        //List that contains users that are not defined in tbl: User, OfficeStaff, Administrator  
        public bool ReadOnly { get => readOnly; set => readOnly = value; }
        #endregion
        public WindowPatientAddEdition(ClassPatient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AddNew)
            {
                //Hide fields that are unused during editing employee
                HideUnusedFields();
            }
            if (this.ReadOnly)
            {
                StPanel.IsEnabled = false;
                ButtonSubmit.Visibility = Visibility.Hidden;
            }
            LoadData();
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfValidData())
            {
                return;
            }
            SetData();
            this.DialogResult = true;
        }

        #region Methods used in Window_Loaded
       
        //Hides fields that are unused
        private void HideUnusedFields()
        {
        }
        //Load data from Employee
        private void LoadData()
        {

            TextBoxName.Text = patient.Name;
            TextBoxSurname.Text = patient.Surname;
            TextBoxPhoneNumber.Text = patient.PhoneNumber;
            TextBoxPersonalIDNumber.Text = patient.PersonalIdentityNumber;
            TextBoxCity.Text = patient.City;
            TextBoxStreet.Text = patient.Street;
            TextBoxStreetNumber.Text = patient.Street_number;

            if (patient.DateOfBirth != DateTime.MinValue)
            {
                DatePickerDateOfBirth.SelectedDate = patient.DateOfBirth;
            }
            if (patient.IsActiv) CheckBoxActive.IsChecked = true;
            else CheckBoxActive.IsChecked = false;
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
            if (TextBoxCity.Text.Length == 0)
            {
                MessageBox.Show("Input city", "Invalid data");
                return false;
            }
            if (TextBoxStreet.Text.Length == 0)
            {
                MessageBox.Show("Input street", "Invalid data");
                return false;
            }
            if (TextBoxStreetNumber.Text.Length == 0)
            {
                MessageBox.Show("Input street number", "Invalid data");
                return false;
            }
            if (TextBoxPhoneNumber.Text.Length == 0)
            {
                MessageBox.Show("Input phone number", "Invalid phone number");
                return false;
            }
            if (DatePickerDateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Select date", "Select date");
                return false;
            }
            if (DatePickerDateOfBirth.SelectedDate >= DateTime.Now.Date)
            {
                MessageBox.Show("Select valid date", "Select date");
                return false;
            }

            //Additional check if add new office staff
            if (AddNew)
            {
                if (!Class.Login.ClassHelpers.IsValidNumberLenght(TextBoxPersonalIDNumber.Text, 11))
                {
                    MessageBox.Show("Pesel must be 11 digits long", "Invalid pesel");
                    return false;
                }
            }
            return true;
        }

        //sets data for new office class member
        private void SetData()
        {
            //Set data
            patient.Name = TextBoxName.Text;
            patient.Surname = TextBoxSurname.Text;
            patient.PhoneNumber = TextBoxPhoneNumber.Text;
            patient.DateOfBirth = (DateTime)DatePickerDateOfBirth.SelectedDate;
            patient.IsActiv = (bool)CheckBoxActive.IsChecked;
            patient.City = TextBoxCity.Text;
            patient.Street = TextBoxStreet.Text;
            patient.Street_number = TextBoxStreetNumber.Text;
            if (AddNew)
            {
                patient.PersonalIdentityNumber = TextBoxPersonalIDNumber.Text;
            }
        }
        #endregion

    }
}
