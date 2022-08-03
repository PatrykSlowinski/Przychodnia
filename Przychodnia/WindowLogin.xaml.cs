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
using Przychodnia.Windows.Login;
using System.Windows.Media.Animation;
using System.Threading;

namespace Przychodnia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        PageLogin pageLogin;
        public WindowLogin()
        {
            InitializeComponent();
        }
        public void UpdateMainContent(Page page)
        {
            Main.Content = page;
        }
        public void OpenLogin()
        {
            Main.Content = pageLogin;
        }
        public void OpenAdministratorWindow()
        {
            WindowAdministrator windowAdministrator = new WindowAdministrator();
            windowAdministrator.Show();
            this.Close();
        }
        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pageLogin = new PageLogin(this);
            OpenLogin();
        }

        #region Animation
        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;
        private Duration _duration = new Duration(TimeSpan.FromSeconds(0.6));
        private double _oldHeight = 0;

        private void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _oldHeight = Main.ActualHeight;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = Main.ActualHeight;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                Main.BeginAnimation(HeightProperty, animation0);
            }
            _allowDirectNavigation = false;
        }

        private void SlideCompleted(object sender, EventArgs e)
        {
            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                        Main.Navigate(_navArgs.Content);
                    else
                        Main.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    Main.GoBack();
                    break;
                case NavigationMode.Forward:
                    Main.GoForward();
                    break;
                case NavigationMode.Refresh:
                    Main.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = _oldHeight;
                    animation0.Duration = _duration;
                    Main.BeginAnimation(HeightProperty, animation0);
                });
        }
        #endregion
    }
}
