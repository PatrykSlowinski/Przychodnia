using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace Przychodnia.Class.Login
{
    static class ClassHelpersWindows
    {
        //Method that centers window
        public static void CenteringWindow(Window window)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }
        //Method that displays MessageBox that is displayed if you want to cancel something
        public static bool MsgBoxYesNo(Window window, string text, string heading)
        {
            MessageBoxResult result = MessageBox.Show(text, heading, MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }

       
    }
}
