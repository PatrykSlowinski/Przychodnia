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
using Przychodnia.Class.Calendar;
using System.Linq;
using Przychodnia.Windows.DictionariesHandling;


namespace Przychodnia
{
    /// <summary>
    /// Logika interakcji dla klasy WindowAdministrator.xaml
    /// </summary>
    public partial class WindowAdministrator : Window
    {
        public WindowAdministrator()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.FrameCalendar.Content = new Windows.DictionariesHandling.PageCalendar(this);
        }
        private void TabItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                WindowLogin windowLogin = new WindowLogin();
                windowLogin.Show();
                this.Close();
            }
            else
            {
                this.TabControlMain.SelectedIndex = 0;
                return;
            }
        }
        private void TabItem_MouseLeftButtonUp_Refresh(object sender, MouseButtonEventArgs e)
        {
            FrameDoctor.Content = new WindowViewListOfDoctors();
            FrameEmployee.Content = new WindowViewListOfEmployee();
            FrameUser.Content = new WindowUserList();
            FrameOffice.Content = new PageViewListOfOffice();
            FrameCalendar.Content = new PageCalendar(this);
            this.TabControlMain.SelectedIndex = 0;
            
        }

        private void FramePatient_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
