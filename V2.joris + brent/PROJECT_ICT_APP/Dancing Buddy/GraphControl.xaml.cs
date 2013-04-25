using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GraphControlDemo
{
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
            Loaded += (s,e) => DrawGraph();
        }


        public void DrawGraph()
        {
            //Source: http://stackoverflow.com/questions/4957485/how-can-i-draw-a-nice-graph-like-the-htc-stocks-app
            
            if (GraphValues != null && GraphValues.Count(i => true) > 0)
            {

                int maxGraphVal = GraphValues.Max(p => p);
                int minGraphVal = GraphValues.Min(p => p);
                int gridUnit = (int)GraphGrid.ActualWidth / GraphValues.Count(i => true);
                int graphHeight = (int)GraphGrid.ActualHeight;
                
                // Declare a Polyline for the spark line
                Polyline sparkLine = new Polyline();

                // Get style from the XAML user control resources
                sparkLine.Style = (Style)Resources["graphLine"];

                // Declare a polygon for the background
                Polygon backgroundPolygon = new Polygon();

                // Get its style
                // backgroundPolygon.Style = (Style)this.Resources["graphBackground"];

                // PointCollection for the graph
                PointCollection graphPointsCollection = new PointCollection();

                // The X value for each point just gets advanced by a uniform amount for each
                // Y value on the graph, in this case by an int called gridUnit, which was defined elsewhere
                int currentX = 0;

                // Get the range covering the min and max graph bounds
                decimal graphValRange = maxGraphVal - minGraphVal;

                // Traverse the numeric values in a list, create points and add them to the PointCollection
                foreach (var graphVal in GraphValues)
                {
                    // Calculate the Y2 value as a percentage
                    decimal graphY2Val = ((int)graphVal - minGraphVal) / graphValRange;

                    // Then apply that percentage to the overall graph height and that will be our Y2 value.
                    // NOTE: Since Y values are inverse, we subtract it from the graph height to render it correctly
                    double graphY2ValDouble = Convert.ToDouble(graphHeight - (graphY2Val * graphHeight));

                    // Create a point from the X and Y values
                    Point currentPoint = new Point(currentX, graphY2ValDouble);

                    // Add it to the collection
                    graphPointsCollection.Add(currentPoint);

                    // Advance the X value each time (as a multiple of the grid unit)
                    currentX += gridUnit;
                }

                // For the background we'll use all the same points but need to clone. Otherwise,
                // when some additional points are added they would also end up in the spark line
                PointCollection backgroundPointsCollection = new PointCollection() { };
                foreach (var p in graphPointsCollection)
                {
                    backgroundPointsCollection.Add(p);
                }

                // Now add additional points to collection to create background polygon.
                // These will allow the polygon to be drawn to bottom right
                // and back to bottom left, completing the polygon.
                Point bottomRightPoint = new Point(currentX - gridUnit, graphHeight);
                Point bottomLeftPoint = new Point(0, graphHeight);

                backgroundPointsCollection.Add(bottomRightPoint);
                backgroundPointsCollection.Add(bottomLeftPoint);

                // Now assign the points to the background polygon
                backgroundPolygon.Points = backgroundPointsCollection;

                // Add the background to the graph first so it doesn't cover the spark line
                GraphGrid.Children.Add(backgroundPolygon);

                // Finally, assign the graph points to the spark line
                sparkLine.Points = graphPointsCollection;

                // Add the spark line to the graph
                GraphGrid.Children.Clear();
                GraphGrid.Children.Add(sparkLine);
            }
            else
            {
                GraphGrid.Children.Clear();
                GraphGrid.Children.Add(new TextBlock() { Text = "No data", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        #region GraphValue DependencyProperty
        
        public ObservableCollection<int> GraphValues
        {
            get { return (ObservableCollection<int>)GetValue(GraphDataProperty); }
            set { SetValue(GraphDataProperty, value); DrawGraph(); }
        }


        public static readonly DependencyProperty GraphDataProperty = 
            DependencyProperty.Register("GraphValues",
                typeof(ObservableCollection<int>),
                typeof(GraphControl),
                new PropertyMetadata(null, OnGraphValuesChanged));

        private static void OnGraphValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Source http://stackoverflow.com/questions/2771407/wpf-binding-an-observablecollection-dependency-property-within-a-usercontrol
            GraphControl me = d as GraphControl;
            if (me != null)
            { 
                var old = e.OldValue as ObservableCollection<int>;
                if (old != null)
                    old.CollectionChanged -= me.OnGraphValueChanged;

                var n = e.NewValue as ObservableCollection<int>;
                if (n != null)
                    n.CollectionChanged +=  me.OnGraphValueChanged ;
            }
        }
        private  void OnGraphValueChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DrawGraph();
        }
        #endregion

    }

}