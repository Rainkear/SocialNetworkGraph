using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SocialNetworkGraph
{
    public class EdgeRouteToPathConverter : IMultiValueConverter
    {
        /// <summary>
        /// Find attach point on ource object 
        /// </summary>
        /// <param name="s">Source</param>
        /// <param name="sourceSize">Source size</param>
        /// <param name="t">Target</param>
        /// <returns>Attach point</returns>
        private Point CalculateAttachPoint(Point s, Size sourceSize, Point t)
        {
            double[] sides = new double[4];
            sides[0] = (s.X - sourceSize.Width / 2.0 - t.X) / (s.X - t.X);
            sides[1] = (s.Y - sourceSize.Height / 2.0 - t.Y) / (s.Y - t.Y);
            sides[2] = (s.X + sourceSize.Width / 2.0 - t.X) / (s.X - t.X);
            sides[3] = (s.Y + sourceSize.Height / 2.0 - t.Y) / (s.Y - t.Y);

            double fi = 0;
            for (int i = 0; i < 4; i++)
            {
                if (sides[i] <= 1)
                    fi = Math.Max(fi, sides[i]);
            }

            return t + fi * (s - t);
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Point sourcePos = new Point()
            {
                X = (values[0] != DependencyProperty.UnsetValue ? (double)values[0] : 0.0),
                Y = (values[1] != DependencyProperty.UnsetValue ? (double)values[1] : 0.0)
            };

            Size sourceSize = new Size()
            {
                Width = (values[2] != DependencyProperty.UnsetValue ? (double)values[2] : 0.0),
                Height = (values[3] != DependencyProperty.UnsetValue ? (double)values[3] : 0.0)
            };

            Point targetPos = new Point()
            {
                X = (values[4] != DependencyProperty.UnsetValue ? (double)values[4] : 0.0),
                Y = (values[5] != DependencyProperty.UnsetValue ? (double)values[5] : 0.0)
            };

            Size targetSize = new Size()
            {
                Width = (values[6] != DependencyProperty.UnsetValue ? (double)values[6] : 0.0),
                Height = (values[7] != DependencyProperty.UnsetValue ? (double)values[7] : 0.0)
            };

            //if edge not straight
            Point[] routeInformation = (values[8] != DependencyProperty.UnsetValue ? (Point[])values[8] : null);
            bool hasRouteInfo = routeInformation != null && routeInformation.Length > 0;

            Point p1 = CalculateAttachPoint(sourcePos, sourceSize, (hasRouteInfo ? routeInformation[0] : targetPos));
            Point p2 = CalculateAttachPoint(targetPos, targetSize, (hasRouteInfo ? routeInformation[routeInformation.Length - 1] : sourcePos));

            //prevent error edges drawing
            if (double.IsNaN(sourcePos.X) || double.IsNaN(sourcePos.Y))
                return null;

            PathSegment[] segments = new PathSegment[1 + (hasRouteInfo ? routeInformation.Length : 0)];
            if (hasRouteInfo)
                for (int i = 0; i < routeInformation.Length; i++)
                    segments[i] = new LineSegment(routeInformation[i], true);

            Point pLast = (hasRouteInfo ? routeInformation[routeInformation.Length - 1] : p1);

            segments[segments.Length - 1] = new LineSegment(p2, true);

            PathFigureCollection pfc = new PathFigureCollection(2);
            pfc.Add(new PathFigure(p1, segments, false));

            return pfc;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //no need
            throw new NotImplementedException();
        }
    }
}
