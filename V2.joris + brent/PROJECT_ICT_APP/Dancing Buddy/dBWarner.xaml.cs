using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Windows.Threading;
using System.Windows.Media;
using System.ComponentModel;

namespace Dancing_Buddy
{
    public partial class dBWarner : PhoneApplicationPage
    {
        private int decibel_AVG_Length = 20;
        private List<double> Decibel_list = new List<double>();
        public double AVG_Decibel;
        double result;
        public double[] decibelAVG; // to calculate average dB level

        private int hearing_loss = 80;
        private int pain_Threshold = 60;
        private Microphone microphone = Microphone.Default;
        private byte[] microphonesignaal;
        private double dB;

        private Timer t = new Timer();
        dbprocessor dbprocessor = new dbprocessor();

        public dBWarner()
        {
            DispatcherTimer T = new DispatcherTimer();
            T.Interval = TimeSpan.FromSeconds(1);
            T.Tick += new EventHandler(T_Tick);
            T.Start();
            InitializeComponent();
            microphone.BufferDuration = TimeSpan.FromMilliseconds(100);
            microphone.BufferReady += new EventHandler<EventArgs>(microphone_BufferReady);

            microphonesignaal = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (t.Start == true)
            {
                TxbTimer.Text = "Hour " + t.Hour + " Minute " + t.Minutes + " Second " + t.Seconds;
                if (t.Seconds <= 0 && t.Minutes <= 0 && t.Hour <= 0)
                {
                    t.Seconds = 0;
                    t.Minutes = 0;
                    t.Hour = 0;
                    t.Start = false;
                    MessageBox.Show("done");
                }
                else if (t.Seconds <= 0 && t.Minutes <= 0)
                {
                    if (t.Hour != 0)
                    {
                        t.Hour--;
                        t.Minutes = 59;
                        t.Seconds = 0;
                    }
                }
                else if (t.Seconds <= 0)
                {
                    if (t.Minutes != 0)
                    {
                        t.Minutes--;
                        t.Seconds = 59;
                    }
                }
                t.Seconds--;
            }

        }

        private void dt_Tick(object sender, EventArgs e)
        {
            try
            {
                FrameworkDispatcher.Update();
            }

            catch (OutOfMemoryException ex)
            {

            }

        }
        private void microphone_BufferReady(object sender, EventArgs e)
        {
          dB =  calculateDecibel();
          processDecibel(dB);
        }

        private void processDecibel(double decibel)
        {

            if (decibel >= prgBar.Maximum / 4 * 3)
            {
                prgBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 1, 1));
            }
            else
                if (decibel >= prgBar.Maximum / 2)
                {
                    prgBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 112, 1));
                }
                else
                    prgBar.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 35, 255, 1));

            prgBar.Value = decibel;
            //lstdB.Items.Insert(0, decibel);
            Decibel_list.Insert(0, decibel);
            if (Decibel_list.Count >= decibel_AVG_Length)
            {
                decibelAVG = Decibel_list.Cast<double>().ToArray(); //= lstdB.Items.Cast<double>().ToArray();
                AVG_Decibel = _AVGDecibel(decibelAVG);
                Decibel_list.Clear(); //lstdB.Items.Clear();
            }
        }

        private double calculateDecibel()
        {
            microphone.GetData(microphonesignaal);
            decibelAVG = new double[decibel_AVG_Length];
            long totalSquare = 0;
            for (int i = 0; i < microphonesignaal.Length; i += 2)
            {
                short sample = (short)(microphonesignaal[i] | (microphonesignaal[i + 1] << 8));
                totalSquare += sample * sample;

            }
            long meanSquare = 2 * totalSquare / microphonesignaal.Length;
            double rms = Math.Sqrt(meanSquare);
            double volume = rms / 32768.0;
            lstbx.Items.Insert(0, volume);
            prgBar.Maximum = 110;

            double sum = 0;
            for (var i = 0; i < microphonesignaal.Length; i = i + 2)
            {
                double sample = BitConverter.ToInt16(microphonesignaal, i) / 32768.0;
                sum += (sample * sample);
            }
            double RMS = Math.Sqrt(sum / microphonesignaal.Length);
            double decibel = 92.8 + 20 * Math.Log10(RMS);
            return decibel;
        }

        private double _AVGDecibel(double[] decibalAVG)
        {
            double dB = 0;
            double result = 0;
            double betweenresult = 0;
            for (int i = 0; i < decibalAVG.Length; i++)
            {
                dB += decibalAVG[i];
            }
            dB /= decibalAVG.Length;
            result = Math.Round(dB);
            betweenresult = dB;
            if (betweenresult >= 60)
            {
                t.getTime(betweenresult);
            }
            else
            {
                TxbTimer.Text = "";
                t.Start = false;
            }

            TxbdB.Text = result.ToString();

            /* if (betweenresult >= 70)
             {
                 TxbdB.Text = result.ToString();
                 //TxbWarning.Text = "  You can only hear 2 hour ,\n if you stay to listen this music\n decibel level ";
                 //t.StartTimer(2, 0, 0);
             }
             else if (betweenresult >= 80)
             {
                 TxbdB.Text = result.ToString();
                 //TxbWarning.Text = " You can only hear 1 hour ,\n if you stay to listen this music\n decibel level ";
                 // t.StartTimer(1, 0, 0);
             }
             else
             {
                 TxbdB.Text = result.ToString();
                 TxbWarning.Text = "";
                 //t.StopTimer();
             }*/
            return result;
        }


        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = Visibility.Collapsed;
            btnPauze.Visibility = Visibility.Visible;
            microphone.Start();
        }

        private void btnPauze_Click(object sender, RoutedEventArgs e)
        {
            btnPlay.Visibility = Visibility.Visible;
            btnPauze.Visibility = Visibility.Collapsed;
            microphone.Stop();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            microphone.Stop();
        }
    }
}

