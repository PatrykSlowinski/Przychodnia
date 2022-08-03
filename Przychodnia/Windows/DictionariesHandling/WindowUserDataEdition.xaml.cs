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
using Przychodnia.Class.Login;
namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Interaction logic for WindowUserDataEdition.xaml
    /// </summary>
    public partial class WindowUserDataEdition : Window
    {
        private ClassUser user;
        private bool addNew;
        private bool isNotAssigned;

        public bool AddNew { get => addNew; set => addNew = value; }
        public bool IsNotAssigned { get => isNotAssigned; set => isNotAssigned = value; }

        public WindowUserDataEdition(ClassUser user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void InsertPermissions()
        {
            //Insert professions into combobox
            try
            {
                ComboBoxPermission.ItemsSource = ClassSQLConnections.PermissionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
                this.Close();
            }
           
        }

        private void PasswordBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RetypePassword.Visibility = Visibility.Visible;
            this.Height = 400;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            RetypePassword.Visibility = Visibility.Collapsed;


        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
           if( !ValidateData())
           {
                return;
           }

            SetUserData();
            this.DialogResult = true;
        }

        private void UserEditionData()
        {

            TextBoxLogin.Text = user.Login;
            PasswordBoxPassword.Password = user.Password;
            PasswordBoxRetypePassword.Password = user.Password;
            TextBoxEmail.Text = user.Email;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!AddNew)
            {
                UserEditionData();
                RetypePassword.Visibility = Visibility.Collapsed;
            }
            //Insert professions into combo box
            InsertPermissions();
            //Load data from object OfficeStaff
            LoadData();
        }
        private void LoadData()
        {
            //Load dat from OfficeStaff
            TextBoxLogin.Text = user.Login;
            TextBoxEmail.Text = user.Email;
            if (!(user.Permission is null))
            {
                foreach (ClassPermission permission in ComboBoxPermission.Items)
                {
                    if (permission.PermissionId == user.Permission.PermissionId)
                    {
                        ComboBoxPermission.SelectedItem = permission;
                    }
                }
                if (!isNotAssigned)
                {
                    if (user.Permission.PermissionId == 3)
                    {
                        ComboBoxPermission.IsEnabled = false;
                    }
                }
            }

        }

        private void SetUserData()
        {
            user.Login = TextBoxLogin.Text;
            user.Password = PasswordBoxPassword.Password;
            user.Email = TextBoxEmail.Text;
            user.Permission = new ClassPermission();
            user.Permission.PermissionId = ((ClassPermission)ComboBoxPermission.SelectedItem).PermissionId;

        }
       

        private bool ValidateData() 
        {
            if (TextBoxLogin.Text.Equals(""))
            {
                MessageBox.Show("Login can't be empty");
                return false;
            }
            if (!ClassHelpers.IsValidPassword(PasswordBoxPassword.Password))
            {
                MessageBox.Show("password must be eight to fifteen characters long, must contain at least one lowercase letter, uppercase letter, number and special chracter");
                return false;
            }
            if (PasswordBoxRetypePassword.Password != PasswordBoxPassword.Password)
            {
                MessageBox.Show("Passwords must be the same");
                return false;
            }
            if (!ClassHelpers.ValidateEmail(TextBoxEmail.Text))
            {
                MessageBox.Show("It's not correct email");
                return false;
            }
            if (ComboBoxPermission.SelectedIndex == -1)
            {
                MessageBox.Show("Select permission", "Permission was not selected");
                return false;
            }
            return true;

        }
    }
}
