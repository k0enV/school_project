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
using System.Windows.Threading;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showColumnChart();
        }

        
    
        int f2 = 50;
        int f3 = 70;
        int f4 = 60;
        int f5 = 30;

        public int F1 { get; set; }
    
     
        private void showColumnChart()
        {
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
          
            valueList.Add(new KeyValuePair<string, int>("1kHz", F1));
            valueList.Add(new KeyValuePair<string, int>("2kHz", f2));
            valueList.Add(new KeyValuePair<string, int>("3kHz", f3));
            valueList.Add(new KeyValuePair<string, int>("4kHz", f4));
            valueList.Add(new KeyValuePair<string, int>("5kHz", f5));
           


            //Setting data for bar chart
            columnChart.DataContext = valueList;
          
        }

    

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            

            F1 = Convert.ToInt32(slider.Value);
            showColumnChart();

            
            textb.Text = slider.Value.ToString();

        }

    }
}
