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
    /// Logika interakcji dla klasy WindowDoctorDataEdition.xaml
    /// </summary>
    public partial class WindowDoctorDataEdition : Window
    {
        #region Declare variables
        //Doctor data are inside variable "doctor"
        private ClassDoctor doctor = new ClassDoctor();
        //Variable disabling editing data if set on true
        private bool readOnly;
        private bool addNewDoctor;
        public bool ReadOnly { get => readOnly; set => readOnly = value; }
        public bool AddNewDoctor { get => addNewDoctor; set => addNewDoctor = value; }
        #endregion
        public WindowDoctorDataEdition(ClassDoctor doctor)
        {
            InitializeComponent();
            this.doctor = doctor;
            
        }

        List<ClassTypeOfSpecialization> lista = ClassSQLConnections.TypeOfSpecializationList();

        //Loads Window
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AddNewDoctor)
            {
                ComboBoxEmployee.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                Office.Visibility = Visibility.Collapsed;
                this.Height = this.Height - 50;

 
                CheckBoxActive.IsChecked = doctor.Active;
                if (!(doctor.Degree is null))
                {
                    foreach (ClassDegreeOfDoctor type in ComboBoxDegreeOfDoctor.Items)
                    {
                        if (type.DegreeOfDoctorId == doctor.Degree.DegreeOfDoctorId)
                        {
                            ComboBoxDegreeOfDoctor.SelectedItem = type;
                        }
                    }
                }
                if (!(doctor.Specialization is null))
                {
                    foreach (ClassSpecialization type in ComboBoxSpecialization.Items)
                    {
                        if (type.SpecializationId == doctor.Specialization.SpecializationId)
                        {
                            ComboBoxSpecialization.SelectedItem = type;
                        }
                    }
                }
                if (!(doctor.TypeOfSpecialization is null))
                {
                    for(int i =0;i< ComboBoxTypeOfSpecialization.Items.Count;i++)
                    {
                        ClassTypeOfSpecialization typ = (ClassTypeOfSpecialization)ComboBoxTypeOfSpecialization.Items[i];
                        if (typ.Equals(doctor))
                        {
                            ComboBoxTypeOfSpecialization.SelectedIndex = i;   
                        }
                    }
                }
                if (ReadOnly)
                {
                    disableEdition();
                }
               
            }
        }

        //When window is initialized
        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadUser();
            
            ComboBoxTypeOfSpecialization.IsEnabled = false;

            try
            {
                ComboBoxSpecialization.ItemsSource = ClassSQLConnections.SpecializationList();
                ComboBoxDegreeOfDoctor.ItemsSource = ClassSQLConnections.DegreeOfDoctorList();
                ComboBoxOffice.ItemsSource = ClassSQLConnections.NotSpecifiedOffice();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
                this.Close();
            }
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            SetData();
            this.DialogResult = true;
        }

        private void ComboBoxSpecialization_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassSpecialization specialization = (ClassSpecialization)ComboBoxSpecialization.SelectedItem;
            List<ClassTypeOfSpecialization> typeOfSpecializationsToAdd = new List<ClassTypeOfSpecialization>();
            foreach (ClassTypeOfSpecialization typeOfSpecialization in ClassSQLConnections.TypeOfSpecializationList())
            {
                if (specialization.SpecializationId == typeOfSpecialization.SpecializationId)
                {
                    typeOfSpecializationsToAdd.Add(typeOfSpecialization);
                }
            }

            ComboBoxTypeOfSpecialization.Items.Clear();

            foreach (ClassTypeOfSpecialization typeOfSpecializationToAdd in typeOfSpecializationsToAdd)
            {
                ComboBoxTypeOfSpecialization.Items.Add(typeOfSpecializationToAdd);
            }
            ComboBoxTypeOfSpecialization.IsEnabled = true;
        }

        #region Methods for Loading
        private void LoadUser()
        {
            try
            {
                foreach (ClassEmployee user in ClassSQLConnections.NotSpecifiedEmployee())
                {
                    ComboBoxEmployee.Items.Add(user);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        private void disableEdition()
        {

            //Disabling editing fields and hiding button
            ComboBoxDegreeOfDoctor.IsEnabled = false;           
            ComboBoxEmployee.IsEnabled = false;
            ComboBoxOffice.IsEnabled = false;          
            ComboBoxSpecialization.IsEnabled = false;          
            ComboBoxTypeOfSpecialization.IsEnabled = false;
            CheckBoxActive.IsEnabled = false;
            ButtonSubmit.Visibility = Visibility.Collapsed;
            this.Height = this.Height - 45;
        }
        #endregion
        #region Method for submit
                
        
        //Checks if data is valid
        private bool ValidateData()
        {
            if (ComboBoxDegreeOfDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Select degree", "Degree was not selected");
                return false;
            }
            if (AddNewDoctor)
            {
                if (ComboBoxEmployee.SelectedIndex == -1)
                {
                    MessageBox.Show("Select employee", "Employee was not selected");
                    return false;
                }
                if (ComboBoxOffice.SelectedIndex == -1)
                {
                    MessageBox.Show("Select office", "Office was not selected");
                    return false;
                }
            }
            if (ComboBoxSpecialization.SelectedIndex == -1)
            {
                MessageBox.Show("Select specialization", "Specialization was not selected");
                return false;
            }
            if (ComboBoxTypeOfSpecialization.SelectedIndex == -1)
            {
                MessageBox.Show("Select type of specialization", "Type of specialization was not selected");
                return false;
            }
            return true;
        }

        //Sets data for doctor
        private void SetData()
        {
            ClassDegreeOfDoctor degree = (ClassDegreeOfDoctor)ComboBoxDegreeOfDoctor.SelectedItem;
            ClassSpecialization specialization = (ClassSpecialization)ComboBoxSpecialization.SelectedItem;
            ClassTypeOfSpecialization typeOfSpecialization = (ClassTypeOfSpecialization)ComboBoxTypeOfSpecialization.SelectedItem;
            ClassEmployee employee = (ClassEmployee)ComboBoxEmployee.SelectedItem;
            if (AddNewDoctor)
            {
                ClassOffice off = (ClassOffice)ComboBoxOffice.SelectedItem;
                doctor.Degree = new ClassDegreeOfDoctor();
                doctor.Specialization = new ClassSpecialization();
                doctor.TypeOfSpecialization = new ClassTypeOfSpecialization();
                doctor.OfficeNumber = off.OfficeNumber;
                doctor.EmployeeId = employee.EmployeeId;
            }
            doctor.Active = (bool)CheckBoxActive.IsChecked;
            doctor.Degree.DegreeOfDoctorId =degree.DegreeOfDoctorId;
            doctor.Specialization.SpecializationId = specialization.SpecializationId;
            doctor.TypeOfSpecialization.TypeOfSpecializationId = typeOfSpecialization.TypeOfSpecializationId;
        }
        #endregion

       
    }
}
