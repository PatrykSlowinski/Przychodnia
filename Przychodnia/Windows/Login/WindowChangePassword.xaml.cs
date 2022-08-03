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
using Przychodnia.Class.Login;



namespace Przychodnia.Windows.login
{
    /// <summary>
    /// Logika interakcji dla klasy WindowChangePassword.xaml
    /// </summary>
    public partial class WindowChangePassword : Page
    {
        //Auxiliary variables holding the necessary data
        private string login;
        private string email;
        WindowLogin Main;
        public WindowChangePassword(WindowLogin Main,string login, string email)
        {
            InitializeComponent(); 
            this.login = login;
            this.email = email;
            this.Main = Main;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Main.OpenLogin();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want change your password?", "Continue?", MessageBoxButton.YesNo)!=MessageBoxResult.Yes) return;
            try
            {
                //Change the password in the database
                new ClassSQLConnection().ChangePassword(login, email, PasswordBoxPassword.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"\nTry again later" ,"Error");
                return;
            }
            Main.OpenLogin();
        }

        private void PasswordBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Verify if both passwordboxes are the same
            Button_Save.IsEnabled = PasswordBoxPassword.Password.Equals(PasswordBox_RepeatPassword.Password) && ClassHelpers.IsValidPassword(PasswordBoxPassword.Password);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockLogin.Content = login;
        }
    }
}
