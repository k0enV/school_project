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


namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        Microphone mic = Microphone.Default;
        byte[] buffer;
        MemoryStream stream = new MemoryStream();


        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            mic.BufferDuration = TimeSpan.FromMilliseconds(500);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(33);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();

            mic.BufferReady += mic_BufferReady;          
        }

        void dt_Tick(object sender, EventArgs e)
        {
            try { FrameworkDispatcher.Update(); }
            catch { }
            lstbox.Items.Add(stream.GetBuffer());
        }

        void mic_BufferReady(object sender, EventArgs e)
        {
            mic.GetData(buffer);
            stream.Write(buffer, 0, buffer.Length);
            stream.SetLength(0);
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Allocate memory to hold the audio data
            buffer = new byte[mic.GetSampleSizeInBytes(mic.BufferDuration)];

            // Set the stream back to zero in case there is already something in it
            stream.SetLength(0);

            // Start recording
            mic.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            mic.Stop();
        }
    }
}