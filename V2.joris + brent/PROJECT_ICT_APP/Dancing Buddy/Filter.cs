using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dancing_Buddy
{
    class Filter
    {

        private double[] xv = new double[19];
        private double[] xvold = new double[19];
        private double[] yv = new double[19];
        private double[] yvold = new double[19];
      

        //default Filter
        public Filter()
        {


        }
        public double filter(double sample)
        {
            for (int i = 1; i < xvold.Length; i++)
            {
                xv[i - 1] = xvold[i];
            }
            xv[18] = sample / 0.284734755;// versterking in center


            for (int i = 1; i < yvold.Length; i++)
            {
                yv[i - 1] = yvold[i];
            }

            yv[18]= (xv[18]-xv[0]
                +9*(xv[2]-xv[16])
                +36*(xv[14]-xv[4])
                + 84 * (xv[6] - xv[12]) 
                + 126 * (xv[10] - xv[8])
                + ( -0.8930831716 * yv[0]) 
                + ( 16.1752645690 * yv[1])
                + (-138.3445830100 * yv[2]) 
                + (742.4335410100 * yv[3])
                + (-2801.4985683000 * yv[4]) 
                + (7893.2315751000 * yv[5])
                + (-17209.1019980000 * yv[6]) 
                + (29686.3996450000 * yv[7])
                + (-41075.3426170000 * yv[8]) 
                + (45926.6219240000 * yv[9])
                + (-41594.6696030000 * yv[10])
                + (30441.8117580000 * yv[11])
                + (-17870.1275650000 * yv[12]) 
                + (8300.0509752000 * yv[13])
                + (-2983.1341392000 * yv[14]) 
                + (800.5646556400 * yv[15])
                + (-151.0627634800 * yv[16]) 
                + ( 17.8855822170 * yv[17]));
            xvold=xv;
            yvold=yv;
            return yv[18];
        }
       
        
    
    }
}
