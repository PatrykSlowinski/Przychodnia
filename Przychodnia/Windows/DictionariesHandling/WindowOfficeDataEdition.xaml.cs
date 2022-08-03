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
using System.Text.RegularExpressions;
using Przychodnia.Class.DictionariesHanding;
using Przychodnia.Class.Calendar;
using System.Linq;

namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Interaction logic for WindowOfficeDataEdition.xaml
    /// </summary>
    public partial class WindowOfficeDataEdition : Window
    {
        private ClassOffice office;
        private bool addNew;

        public bool AddNew { get => addNew; set => addNew = value; }

        public WindowOfficeDataEdition(ClassOffice office)
        {
            InitializeComponent();
            this.office = office;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (checkOffice())
                return;
            SetData();
            this.DialogResult = true;
            
        }

        private void SetData()
        {
            //Set data
            office.OfficeNumber = int.Parse(TextBoxOffice.Text);
            office.OfficeDescription = TextBoxOfficeDescription.Text;
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxOffice.Text = office.OfficeNumber.ToString();
            TextBoxOfficeDescription.Text = office.OfficeDescription;
        }
        private bool checkOffice()
        {
            foreach (int officeNumber in ClassSQLConnections.OfficeNumbersList())
            {
                if (TextBoxOffice.Text.Equals(officeNumber.ToString()))
                {
                    MessageBox.Show("This office number exists");
                    return true;
                }
            }
            return false;
        }
    }
}
