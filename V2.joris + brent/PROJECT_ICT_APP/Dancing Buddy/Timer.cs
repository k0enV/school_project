using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dancing_Buddy
{
    public class Timer
    {
        private bool start;
        public bool Start
        {
            get { return start; }
            set { start = value; }
        }
        private int hour;

        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }
        private int minutes;

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value <= 60 && value > 0)
                {
                    minutes = value;
                }
                else if (value == 0)
                {
                    minutes = value;
                }
                else
                    minutes = 60;
            }
        }

        private int seconds;

        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (value <= 59 && value > 0)
                {
                    seconds = value;
                }
                else if (value == 0)
                {
                    seconds = value;
                }
                else
                    seconds = 59;
            }
        }


        public Timer()
        {
        }


        public void StartTimer(int h, int m, int s)
        {
            Hour = h;
            Minutes = m;
            Seconds = s;
            Start = true;
        }

        public void StopTimer()
        {
            Start = false;
        }

        public void getTime(double value)
        {
            if (value != 0)
            {
                double TimeVal = 0;
                double time = 0;


                TimeVal = (94 - value) / 3;
                time = Math.Pow(2, TimeVal);

                Hour = (int)time;
                double minutes2 = time % 1 * 60;
                Minutes = (int)(minutes2);
                double seconds2 = minutes2 % 1 * 60;
                Seconds = (int)(seconds2);

                StartTimer(Hour, Minutes, Seconds);
            }
        }
    }
}
