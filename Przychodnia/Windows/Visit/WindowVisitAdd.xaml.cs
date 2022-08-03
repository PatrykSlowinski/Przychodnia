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
using Przychodnia.Class.Calendar;

namespace Przychodnia.Windows.Visit
{
    /// <summary>
    /// Logika interakcji dla klasy WindowVisitAdd.xaml
    /// </summary>
    public partial class WindowVisitAdd : Window
    {
        #region Declare variables
        //Doctor data are inside variable "doctor"
        private ClassVisit visit = new ClassVisit();
        //Variable disabling editing data if set on true
        private bool readOnly;
        private bool addNewVisit;
        private bool edit;
        public bool ReadOnly
        {
            get => readOnly;
            set => readOnly = value;
        }
        public bool AddNewVisit
        {
            get => addNewVisit;
            set => addNewVisit = value;
        }
        public bool Edit
        {
            get => edit;
            set => edit = value;
        }

        #endregion
        public WindowVisitAdd(ClassVisit visit)
        {
            InitializeComponent();
            this.visit = visit;
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (!AddNewVisit)
            {

                foreach (ClassPatient patient in ComboBoxPatient.Items)
                {
                    if (patient.PatientId == visit.PatientId)
                    {
                        ComboBoxPatient.SelectedItem = patient;
                    }
                }

                foreach (ClassDoctor doctor in ComboBoxDoctor.Items)
                {
                    if (doctor.Doctor_id == visit.DoctorId)
                    {
                        ComboBoxDoctor.SelectedItem = doctor;
                    }
                }

                foreach (ClassTerm term in ComboBoxTerm.Items)
                {
                    if (term.TermId == visit.TermId)
                    {
                        ComboBoxTerm.SelectedItem = term;
                    }
                }
                TextBoxTopic.Text = visit.Topic;
                TextBoxPersonalID.Text = visit.PersonalIdentityNumber;
                TextBoxDegree.Text = visit.Degree;
                ComboBoxTime.Items.Add(visit.StartTime.ToString());
                ComboBoxTime.Text = visit.StartTime.ToString();
                TextBoxSpecialization.Text = visit.Specialization;
                TextBoxTypeOfSpecialization.Text = visit.TypeOfSpecialization;

                if (ReadOnly)
                {
                    disableEdition();
                }
                else
                {
                    collapseEdition();
                }

            }
            else
            {
                collapseEdition();
            }

        }
        private void collapseEdition()
        {
            PersonalID.Visibility = Visibility.Collapsed;
            Degree.Visibility = Visibility.Collapsed;
            Specialization.Visibility = Visibility.Collapsed;
            TypeOfSpecialization.Visibility = Visibility.Collapsed;
            this.Height = this.Height - 180;
        }
        private void disableEdition()
        {

            //Disabling editing fields and hiding button
            ComboBoxDoctor.IsEnabled = false;
            ComboBoxPatient.IsEnabled = false;
            ComboBoxTerm.IsEnabled = false;
            ComboBoxTime.IsEnabled = false; 
            TextBoxTopic.IsEnabled = false;
            TextBoxPersonalID.IsEnabled = false;
            TextBoxDegree.IsEnabled = false;
            TextBoxSpecialization.IsEnabled = false;
            TextBoxTypeOfSpecialization.IsEnabled = false;
            ButtonSubmit.Visibility = Visibility.Collapsed;
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            LoadDoctor();
            LoadPatient();
        }
        private void LoadDoctor()
        {
            try
            {
                foreach (ClassDoctor doctor in ClassSQLConnections.DoctorList())
                {
                    ComboBoxDoctor.Items.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        private void LoadPatient()
        {
            try
            {
                foreach (ClassPatient patient in ClassSQLConnections.PatientList())
                {
                    ComboBoxPatient.Items.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxPatient.SelectedItem) is null) || ((ComboBoxDoctor.SelectedItem) is null) || ((ComboBoxTime.Text) is null) || ((ComboBoxTerm.SelectedItem) is null))
            {
                MessageBox.Show("Please fill all nessesary fields");
            }
            else
            {
                SetData();
                this.DialogResult = true;
            }

        }

        //Sets data for Visit
        private void SetData()
        {
            
                ClassPatient patient = (ClassPatient)ComboBoxPatient.SelectedItem;
                ClassDoctor doctor = (ClassDoctor)ComboBoxDoctor.SelectedItem;
                ClassTerm term = (ClassTerm)ComboBoxTerm.SelectedItem;

                visit.TermId = term.TermId;
                visit.PatientId = patient.PatientId;
                visit.StartTime = TimeSpan.Parse(ComboBoxTime.Text);
                visit.Topic = TextBoxTopic.Text;
                visit.ToPay = "Waiting";
        }
        private void ComboBoxDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ClassDoctor doctor = (ClassDoctor)ComboBoxDoctor.SelectedItem;
            List<ClassTerm> termsToAdd = new List<ClassTerm>();
            foreach (ClassTerm term in ClassSQLConnections.ListOfTerms())
            {
                if (doctor.Doctor_id == term.Doctor.Doctor_id)
                {
                    termsToAdd.Add(term);
                }
            }
            ComboBoxTime.Items.Clear();
            ComboBoxTerm.Items.Clear();
            foreach (ClassTerm termToAdd in termsToAdd)
            {
                ComboBoxTerm.Items.Add(termToAdd);
            }
            ComboBoxTerm.IsEnabled = true;
        }

        private void ComboBoxTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassTerm term = (ClassTerm)ComboBoxTerm.SelectedItem;
            List<TimeSpan> timeToAdd = new List<TimeSpan>();
            List<TimeSpan> existingTime = new List<TimeSpan>();

            bool check = true;

            if (!(ComboBoxTerm.SelectedItem is null)) {
                existingTime = ClassSQLConnections.NotFreeTime(term.TermId);
                TimeSpan endTime = term.EndTime;
                TimeSpan startTime = term.StartTime;
                TimeSpan time = term.StartTime;
                do
                {
                    //not tested
                    foreach(TimeSpan existTime in existingTime)
                    {
                        if (existTime == time)
                        {
                            check = false;
                        }
                    }
                    if (check == true)
                    {
                        timeToAdd.Add(time);
                    }
                    check = true;
                    time += TimeSpan.FromMinutes(20);
                }
                while (time < endTime);

                ComboBoxTime.Items.Clear();

                foreach (TimeSpan timeAdd in timeToAdd)
                {
                    ComboBoxTime.Items.Add(timeAdd);

                }
            }
        }
    }
}
