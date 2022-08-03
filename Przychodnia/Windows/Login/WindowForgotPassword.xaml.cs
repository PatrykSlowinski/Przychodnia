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
    /// Logika interakcji dla klasy Window_ForgotPassword.xaml
    /// </summary>
    public partial class WindowForgotPassword : Page
    {
        WindowLogin Main;
        public WindowForgotPassword(WindowLogin Main)
        {
            InitializeComponent();
            this.Main = Main;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Main.OpenLogin();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //Getting data from the form
            string login = TextBoxLogin.Text;
            string email = TextBoxEmail.Text;

            bool correctInputData = false;
            try
            {
                correctInputData = new ClassSQLConnection().CheckLoginAndEmail(login, email);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nTry again later", "Error");
                return;
            }

            //Checking if login and email are incorrect
            if(!correctInputData) 
            {
                Main.UpdateMainContent(new WindowInputCode(Main));
                return;
            }

            try
            {
                Main.UpdateMainContent(new WindowInputCode(Main, login, ClassSend.EmailSender(login, email), email));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nTry again later", "Error");
                return;
            }
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonNext.IsEnabled = ClassHelpers.ValidateEmail(TextBoxEmail.Text);
        }
    }
}