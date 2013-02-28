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

namespace Dancing_Buddy
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;

            btnPlay.Visibility = Visibility.Collapsed;
            btnPauze.Visibility = Visibility.Visible;
        }

        private void btnPauze_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;

            btnPlay.Visibility = Visibility.Visible; 
            btnPauze.Visibility = Visibility.Collapsed;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = false;

            btnPlay.Visibility = Visibility.Visible;
            btnPauze.Visibility = Visibility.Collapsed;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Switching page to HomePage
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}