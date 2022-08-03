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
    public partial class WindowInputCode : Page
    {
        //Auxiliary variables holding the necessary data
        private string login;
        private string email;
        private string authentificationCode;

        WindowLogin Main;
        public WindowInputCode(WindowLogin Main ,string login, string authenticationCode, string email)
        {
            InitializeComponent();
            this.login = login;
            this.email = email;
            this.authentificationCode = authenticationCode;
            this.Main = Main;
        }
        public WindowInputCode(WindowLogin Main)
        {
            InitializeComponent();
            this.Main = Main;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //If user give wrong email or login in previous window
            if(authentificationCode == null || !authentificationCode.Equals(TextBoxKod.Text))
            {
                MessageBox.Show("Invalid code");
                return;
            }
            Main.UpdateMainContent(new WindowChangePassword(Main,login, email));
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Main.OpenLogin();
        }
    }
}

