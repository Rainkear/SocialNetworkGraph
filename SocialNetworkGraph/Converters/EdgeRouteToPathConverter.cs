using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SocialNetworkGraph.Converters
{
    public class EdgeRouteToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Point sourcePos = new Point()
            {
                X = (values[0] != DependencyProperty.UnsetValue ? (double)values[0] : 0.0),
                Y = (values[1] != DependencyProperty.UnsetValue ? (double)values[1] : 0.0)
            };

            Point targetPos = new Point()
            {
                X = (values[4] != DependencyProperty.UnsetValue ? (double)values[2] : 0.0),
                Y = (values[5] != DependencyProperty.UnsetValue ? (double)values[3] : 0.0)
            };
            
            Point[] routeInformation = (values[4] != DependencyProperty.UnsetValue ? (Point[])values[4] : null);
            bool hasRouteInfo = routeInformation != null && routeInformation.Length > 0;

            //prevent wrong edges drawing
            if (double.IsNaN(sourcePos.X) || double.IsNaN(sourcePos.Y))
                return null;

            PathSegment[] segments = new PathSegment[1 + (hasRouteInfo ? routeInformation.Length : 0)];
            if (hasRouteInfo)
                for (int i = 0; i < routeInformation.Length; i++)
                    segments[i] = new LineSegment(routeInformation[i], true);

            segments[segments.Length - 1] = new LineSegment(targetPos, true);

            PathFigureCollection pfc = new PathFigureCollection(2);
            pfc.Add(new PathFigure(sourcePos, segments, false));

            return pfc;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //no need
            throw new NotImplementedException();
        }
    }
}
