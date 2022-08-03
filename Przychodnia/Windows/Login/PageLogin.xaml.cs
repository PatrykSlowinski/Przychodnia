using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.SqlClient;
using Przychodnia.Class.Login;
using System.Data.Common;
using System.Timers;
using System.Windows.Threading;
using System.ComponentModel;
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Windows.Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        //variable counting log in failed attempts
        public int failedAttempts = 0;

        WindowLogin Main;
        public PageLogin(WindowLogin Main)
        {
            InitializeComponent();
            this.Main = Main;
        }

        private void ButtonIForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            Main.UpdateMainContent(new Windows.login.WindowForgotPassword(Main));
        }
        private void ButtonLogging_Click(object sender, RoutedEventArgs e)
        {
            //Getting data from the form
            string login = TextBoxLogin.Text;
            string password = PasswordBoxPassword.Password;
            bool correctData;
            try
            {
                correctData = new ClassSQLConnection().CheckLoginDetails(login, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nTry again later", "Error");
                return;
            }

            //Invalid data
            if (!correctData)
            {
                failedAttempts++;
                string message = "Incorrect password or login";
                if (failedAttempts == 2) message +="\nNext unsuccessful attempt will block the login for five minutes";
                MessageBox.Show(message, "Incorrect data");
                if (failedAttempts < 3) return;
                //Block app 
                ButtonLogging.IsEnabled = false;
                TextBlockNextTry.Visibility = Visibility.Visible;
                TextBoxLogin.IsEnabled = false;
                PasswordBoxPassword.IsEnabled = false;
                TextBoxLogin.Text = "";
                PasswordBoxPassword.Password = "";
                //Time counting
                ClassTimer ct = new ClassTimer(5, 0, this);
                ct.StartTimer();
                return;
            }
            try
            {
                ClassPermission permission = new ClassSQLConnection().GetUserType(login, password);
                if (permission.Permission == "Administrator")
                {
                    //Opening MainWindow
                    Main.OpenAdministratorWindow();
                    return;
                }
                Main.OpenMainWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            TextBoxLogin.Text = "";
            PasswordBoxPassword.Password = "";
        }
    }
}