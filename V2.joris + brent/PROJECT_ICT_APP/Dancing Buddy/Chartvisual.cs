using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Dancing_Buddy
{


    class Chartvisual
    {

        private ObservableCollection<TestDataItem> _data = new ObservableCollection<TestDataItem>()
        {
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4",val1=0},
            new TestDataItem() { cat1 = "cat4",val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat1", val1=0},
            new TestDataItem() { cat1 = "cat2", val1=0},
            new TestDataItem() { cat1 = "cat3", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat1", val1=0},
            new TestDataItem() { cat1 = "cat2", val1=0},
            new TestDataItem() { cat1 = "cat3", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},
            new TestDataItem() { cat1 = "cat4", val1=0},

        };
        public ObservableCollection<TestDataItem> Data { get { return _data; } }

        //Visualdata.Data.Add(new TestDataItem() { cat1 = DateTime.Now.Ticks.ToString(), val1 = volume });
        //        Visualdata.Data.RemoveAt(0);

        public Chartvisual()
        { 
        
        }



    }
    public class TestDataItem
    {
        public string cat1 { get; set; }
        public double val1 { get; set; }

    }
}
