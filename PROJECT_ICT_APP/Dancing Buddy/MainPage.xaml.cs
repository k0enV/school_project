using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Dancing_Buddy
{
    public partial class Page1 : PhoneApplicationPage
    {
        
        public Page1()
        {
            InitializeComponent();
        }

        private void btnDancing_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DancingBuddy.xaml", UriKind.Relative));
        }

        private void btnDBwarner_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/dBWarner.xaml", UriKind.Relative));
        }

    }
}