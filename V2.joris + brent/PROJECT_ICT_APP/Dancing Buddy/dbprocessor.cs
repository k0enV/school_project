using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dancing_Buddy
{

    class dbprocessor
    {

        
        private Timer t = new Timer();

        public dbprocessor()
        {
        
        }

        public double volume (byte[] microphonesignaal)
        {
        long totalSquare = 0;
            for (int i = 0; i < microphonesignaal.Length; i += 2)
            {
                short sample = (short)(microphonesignaal[i] | (microphonesignaal[i + 1] << 8));
                totalSquare += sample * sample;

            }
            long meanSquare = 2 * totalSquare / microphonesignaal.Length;
            double rms = Math.Sqrt(meanSquare);
            double volume = rms / 32768.0;
            return volume;
            
        }

               
            
          
            
            public double db (byte[] microphonesignaal)
            {
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

          

            
        


        public double AVGDecibel(double[] decibalAVG)
        {
            double dB = 0;
            double result = 0;
            for (int i = 0; i < decibalAVG.Length; i++)
            {
                dB += decibalAVG[i];
            }
            dB /= decibalAVG.Length;
            result = Math.Round(dB);
           
            return result;
        }
    }
}
