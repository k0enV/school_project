using Microsoft.Devices;
using sdkMicrophoneCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dancing_Buddy
{
    class dancing_processing
    {
        byte[] buffer;
        public short[] shorts;
        public double[] doubles;
        public double[] Filterdsamples;
        Filter filtersample = new Filter();
        private double freq;
        double[] fftfr;
        public int SampleRate = 16000;
        private const double MinFreq = 20;
        private const double MaxFreq = 1300;
        double Fundamentelfr;
        VibrateController vibrate = VibrateController.Default;
        FftAlgorithm fft = new FftAlgorithm();
        double [] gefilterdedata;
         
       
        public dancing_processing(byte[] Buffer)
        {
            buffer = Buffer;
            //Making shorts from bytes
            shorts = buffer.Select(b => (short)b).ToArray();
            doubles = buffer.Select(b => (double)b).ToArray();
            fftfr = Filterdata(FftAlgorithm.Calculate(doubles));
            tril();
            
            
            
        }

        public double [] Filterdata(double [] samples)
        {
            gefilterdedata = new double[samples.Length];
        for (int i = 0; i < gefilterdedata.Length; i++)
            {
                gefilterdedata[i] = filtersample.filter(samples[i]);
                
            }

            
        return gefilterdedata;
        }

        public void tril()
        { 
            double test= fundamentelfr();

            if (test < 65 && test > 15)
            {
                vibrate.Start(TimeSpan.FromMilliseconds(80));
            }
        
        }
       
        public double fundamentelfr()
        {
            Fundamentelfr = ProcessData(shorts);
            
            return Fundamentelfr;
        
        }


        public double volume(byte[] samples)
        {

            byte[] buffer = samples;
            long totalSquare = 0;
            // Volume bepaling

            for (int i = 0; i < buffer.Length; i += 2)
            {
                short sample = (short)(buffer[i] | (buffer[i + 1] << 8));
                totalSquare += sample * sample;

            }
            long meanSquare = 2 * totalSquare / buffer.Length;
            double rms = Math.Sqrt(meanSquare);
            double volume = rms / 32768.0;

            return volume;
        
        }

        //fundamentel freq
        protected double ProcessData(short[] data)
        {
            double[] x = new double[data.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = data[i] / 32768.0;
            }

            double[] spectr = FftAlgorithm.Calculate(x);
            freq = sdkMicrophoneCS.FrequencyUtils.FindFundamentalFrequency(x, SampleRate, MinFreq, MaxFreq);
            //frequentie = freq;
            //frequentie = frequentie * 2;
            //frequentie = (0.7692307692308 * (1300 - frequentie));
            freq *= 2;
            return freq;


        }
        
        
    }
}
