using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;

using sdkMicrophoneCS;
using Microsoft.Devices;

namespace Dancing_Buddy
{
    public partial class MainPage : PhoneApplicationPage
    {
      
       
        private Chartvisual Visualdata = new Chartvisual();
        Microphonedata data = new Microphonedata(1);
      
       
        public MainPage()
        {
            InitializeComponent();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += new EventHandler(dt_Tick);
            //Visualdata.Data.Add(new TestDataItem() { cat1 = DateTime.Now.Ticks.ToString(), val1 = volume });
            //Visualdata.Data.RemoveAt(0);
            dt.Start();
            
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            try { FrameworkDispatcher.Update(); }
            catch (OutOfMemoryException ex)
            {
                //Lstbox.Items.Insert(0, ex.Message);
            }

        }

        
       

       
        
        
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            this.DataContext = this;

            btnPlay.Visibility = Visibility.Collapsed;
            btnPauze.Visibility = Visibility.Visible;
            btnStop.IsEnabled = true;

            // Start recording
          data.microphone.Start();
        }

       
        
        private void btnPauze_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;

            btnPlay.Visibility = Visibility.Visible; 
            btnPauze.Visibility = Visibility.Collapsed;
            //data.microphone.Stop();
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = false;

            btnPlay.Visibility = Visibility.Visible;
            btnPauze.Visibility = Visibility.Collapsed;

            //data.microphone.Stop();

            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Switching page to HomePage
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //data.microphone.Stop();
            
        }

        

    }
}