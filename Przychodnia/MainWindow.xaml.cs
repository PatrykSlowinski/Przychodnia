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
using Przychodnia.Windows.DictionariesHandling;
using Przychodnia.Windows.login;

namespace Przychodnia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //anciliarry variable which is defined to a value that is false after pressing “logout”
        //is required to block the pop-up window, which asks whether to close the window
        //bool askUserAboutClosing =true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void UpdateMenuContent(Page page)
        {

        }
        public void UpdateMainContent(Page page)
        {
        }
        public void OpenLogin()
        {
        }
    }
}
