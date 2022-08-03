using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace Przychodnia.Class.Login
{
    class ClassTimer
    {
        private string text= "Next try available in: 0";
        private int decreamentMinutes; //minutes of timer when it starts
        private int decreamentSeconds;  //seconds of timer when it starts
        private DispatcherTimer dt = new DispatcherTimer();
        Windows.Login.PageLogin logging;

        public ClassTimer(int minutes, int seconds, Windows.Login.PageLogin mw)
        {
            decreamentMinutes = minutes;
            decreamentSeconds = seconds;
            logging = mw;
        }
        //event that happens every tick
        private void TickEvent(object sender, EventArgs e)
        {
            //check if seconds are under 10 if yes returns modified string
            if (decreamentSeconds < 10)
            {
                logging.TextBlockNextTry.Text = text + decreamentMinutes.ToString() + ":0" + decreamentSeconds.ToString();
            }
            else
            {
               logging.TextBlockNextTry.Text = text + decreamentMinutes.ToString() + ":" + decreamentSeconds.ToString();
            }
           
            if (decreamentSeconds == 0)
            {
                if (decreamentMinutes <= 0)
                { 
                   //stops the timer and reset attempts
                   logging.TextBlockNextTry.Visibility = Visibility.Hidden;
                   logging.ButtonLogging.IsEnabled = true;
                   dt.Stop();
                   logging.failedAttempts = 0;
                   logging.TextBlockNextTry.Text = text;
                   logging.PasswordBoxPassword.IsEnabled = true;
                   logging.TextBoxLogin.IsEnabled = true;
                }
  
                decreamentSeconds = 59;
                decreamentMinutes--;
            }
            else
            {
                decreamentSeconds--;
            }
        }
        //Method that starts timer and set its interval
        public void StartTimer()
        {

            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += new EventHandler(TickEvent);
            dt.Start();
            TickEvent(dt, new EventArgs());
        }


    }
}
