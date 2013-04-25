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

using System.Windows.Threading;

namespace Dancing_Buddy
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private Microphone microphone = Microphone.Default;     // Object representing the physical microphone on the device
        private byte[] buffer;                                  // Dynamic buffer to retrieve audio data from the microphone
        private MemoryStream stream = new MemoryStream();       // Stores the audio data for later playback
        private int sampleRate = 16000;
        private const double MinFreq = 10;
        private const double MaxFreq = 1300;
        private double freq;
        public double frequentie;
        private double[] doubleBuffer;

        public int SampleRate
        {
            get { return sampleRate; }
            set
            {
                sampleRate = value;
            }
        }
       
        public MainPage()
        {
            InitializeComponent();

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
            microphone.BufferReady += new EventHandler<EventArgs>(microphone_BufferReady);
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            try { FrameworkDispatcher.Update(); }
            catch (OutOfMemoryException ex)
            {
                Lstbox.Items.Insert(0, ex.Message);
            }

        }

        private void microphone_BufferReady(object sender, EventArgs e)
        {
            // Retrieve audio data
            microphone.GetData(buffer);
            // Store the audio data in a stream
            stream.Write(buffer, 0, buffer.Length);
            //Making shorts from doubles
            var shorts = buffer.Select(b => (short)b).ToArray();
           //start proccesing frequency out of data
            ProcessData(shorts);
        }

        protected void ProcessData(short[] data)
        {
            double[] x = new double[data.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = data[i] / 32768.0;
            }


            freq = sdkMicrophoneCS.FrequencyUtils.FindFundamentalFrequency(x, SampleRate, MinFreq, MaxFreq);
            frequentie = freq;
            Lstbox.Items.Insert(0, frequentie);

        }
        
        //moet nog aangepast worden. 
        private double[] filter(double[] frequentie)
        {
            double[] output;

            var filteredFrequency = from num in frequentie where ( (num > 500 || num < 50)) select num;
            output = filteredFrequency.ToArray();

            return output;
        }
        
        
        
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;

            btnPlay.Visibility = Visibility.Collapsed;
            btnPauze.Visibility = Visibility.Visible;
            
            // Get audio data in 1/10 of second chunks
            microphone.BufferDuration = TimeSpan.FromMilliseconds(100);
           
            // Allocate memory to hold the audio data
            buffer = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
            doubleBuffer = new double[buffer.Length];

            // Set the stream back to zero in case there is already something in it
            stream.SetLength(0);

            // Start recording
            microphone.Start();
        }

       
        
        private void btnPauze_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = true;

            btnPlay.Visibility = Visibility.Visible; 
            btnPauze.Visibility = Visibility.Collapsed;
            microphone.Stop();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStop.IsEnabled = false;

            btnPlay.Visibility = Visibility.Visible;
            btnPauze.Visibility = Visibility.Collapsed;

            if (microphone.State == MicrophoneState.Started)
            {
                
                // stop button to end recording
                microphone.Stop();
            }
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Switching page to HomePage
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            microphone.Stop();
        }

    }
}