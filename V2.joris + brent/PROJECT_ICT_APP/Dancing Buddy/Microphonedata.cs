using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace Dancing_Buddy
{
    class Microphonedata
    {

        public Microphone microphone = Microphone.Default;     // Object representing the physical microphone on the device
        public byte[] buffer;                                  // Dynamic buffer to retrieve audio data from the microphone
        private MemoryStream stream = new MemoryStream();       // Stores the audio data for later playback
        public int dancordb;
        dancing_processing proces;
        dbprocessor dB_process;
       
       
        
        public Microphonedata(int keuze)
        {
            dancordb = keuze;
            startmicro();
           
        }



        public void startmicro()
        {
            microphone.BufferReady += new EventHandler<EventArgs>(microphone_BufferReady);
            // Allocate memory to hold the audio data
            microphone.BufferDuration = TimeSpan.FromMilliseconds(100);
            buffer = new byte[microphone.GetSampleSizeInBytes(microphone.BufferDuration)];
            // Set the stream back to zero in case there is already something in it
            stream.SetLength(0);
            
            
        
        }

        private void microphone_BufferReady(object sender, EventArgs e)
        {
            microphone.GetData(buffer);
            stream.Write(buffer, 0, buffer.Length);


            if (dancordb == 1)
            {
                proces = new dancing_processing(buffer);
                
            }

            else if (dancordb == 2)
            {
               // dB_process = new dbprocessor(buffer);
            }
            

        }
    }
}
