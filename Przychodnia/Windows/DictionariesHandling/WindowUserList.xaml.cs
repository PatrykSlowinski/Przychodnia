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
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Windows.DictionariesHandling
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
  
    public partial class WindowUserList : Page
    {
        public WindowUserList()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

            ClassUser user = new ClassUser();
            WindowUserDataEdition windowUserDataEdition = new WindowUserDataEdition(user);
            windowUserDataEdition.AddNew = true;
            windowUserDataEdition.ShowDialog();
           if(windowUserDataEdition.DialogResult==true)
           {
                UpdateUser(user, windowUserDataEdition.AddNew);
                LoadDataToDataGridListOfUser();
           }
        }
       
        private void LoadDataToDataGridListOfUser()
        {
            //Load data to DataGrid
            try
            {
                DataGridListOfUser.ItemsSource = ClassSQLConnections.UserList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridListOfUser.SelectedIndex==-1)
            {
                MessageBox.Show("Select the row corresponding to the user", "No row selected");
                return;
            }
            ClassUser user = (ClassUser)DataGridListOfUser.SelectedItem;
            WindowUserDataEdition windowUserDataEdition = new WindowUserDataEdition(user);
            windowUserDataEdition.AddNew = false;
            windowUserDataEdition.IsNotAssigned = CheckIfSelectedUserIsNotAssigned(user); 
            windowUserDataEdition.ShowDialog();
            if(windowUserDataEdition.DialogResult==true)
            {
                UpdateUser(user, windowUserDataEdition.AddNew);
                LoadDataToDataGridListOfUser();
            }

        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridListOfUser.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the user", "No row selected");
                return;
            }


            try
            {
                if(!CheckIfSelectedUserIsNotAssigned(((ClassUser)DataGridListOfUser.SelectedItem)))
                {
                    MessageBox.Show("To remove user he can't be assigned to any employee");
                    return;
                }

                ClassSQLConnections.DeleteUser(((ClassUser)DataGridListOfUser.SelectedItem).User_id);
                LoadDataToDataGridListOfUser();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CheckIfSelectedUserIsNotAssigned(ClassUser user)
        {
                foreach (ClassUser notAssignedUser in ClassSQLConnections.NotSpecifiedUsers())
                {

                    if (user.User_id == notAssignedUser.User_id)
                    {
                        return true;
                    }
                }

            return false;
        }
        private void LoadDataToDataGridListOfPermission()
        {
            //Load data to DataGrid
            try
            {
               
                DataGridListOfPermission.ItemsSource = ClassSQLConnections.PermissionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxPermission.Text.Equals(""))
            {
                MessageBox.Show("To add new permission textbox must not be empty");
                return;
            }
            ClassPermission permission = new ClassPermission(TextBoxPermission.Text);
            ClassSQLConnections.AddPermission(permission);
            LoadDataToDataGridListOfPermission();

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataGridListOfPermission.SelectedIndex == -1)
            {
                MessageBox.Show("Select the row corresponding to the permission", "No row selected");
                return;
            }

            int permissionId = ((ClassPermission)DataGridListOfPermission.SelectedItem).PermissionId;
            try
            {
                if (!CheckIfPermissionIsNotAssigned(permissionId))
                {
                    MessageBox.Show("To remove permission it can't be assigned to any user");
                    return;
                }

                ClassSQLConnections.DeletePermission(permissionId);
                LoadDataToDataGridListOfPermission();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private bool CheckIfPermissionIsNotAssigned(int permissionId)
        {
            foreach (int index in ClassSQLConnections.NotAssignedPermission())
            {
                if (permissionId == index)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateUser(ClassUser user, bool AddNew)
        {
            try
            {
                if (!AddNew)
                {
                    ClassSQLConnections.UpdateUser(user);
                    return;
                }
                ClassSQLConnections.AddUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Try again later", "Error");
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToDataGridListOfUser();
            TextBoxPermission.Text = "";
            //Load data to DataGrid
            LoadDataToDataGridListOfPermission();
        }
    }
}
