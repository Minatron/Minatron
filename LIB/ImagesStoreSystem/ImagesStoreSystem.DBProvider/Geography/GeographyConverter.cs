using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Windows;
using Microsoft.SqlServer.Types;

namespace ImagesStoreSystem.DBProvider
{
    public static class GeographyConverter
    {
        static string PointToString(Point p)
        {
            return string.Format("{0} {1}", p.X, p.Y).Replace(',', '.');
        }

        public static SqlGeography PointToGeography(double longitude, double latitude, Srids srid = Srids.WGS84)
        {
            var p = new Point(longitude, latitude);
            return SqlGeography.STGeomFromText(new SqlChars(string.Format("POINT({0})", PointToString(p))), (int) srid);
        }

        /// <param name="points">Точки. x - это долгота, y - это широта</param>
        public static string PointsToString(ICollection<Point> points)
        {
            Point last, first;
            string inner;
            using (var enumerator = points.GetEnumerator())
            {
                enumerator.MoveNext();
                inner = PointToString(enumerator.Current);
                first = last = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    last = enumerator.Current;
                    inner += ", " + PointToString(last);
                }
            }

            //если полигон не замкнут, то сделать его таким
            if (last != first)
                inner += ", " + PointToString(first);

            return string.Format("POLYGON(({0}))", inner);
        }

        /// <param name="points">Точки. x - это долгота, y - это широта</param>
        public static SqlGeography PointsToGeography(ICollection<Point> points, Srids srid = Srids.WGS84)
        {
            if (points == null || points.Count == 0)
                return SqlGeography.Null;
            List<Point> list = new List<Point>(points);
            if (points.Count == 1)
                return PointToGeography(list[0].X, list[0].Y);
            if (points.Count == 2)
                throw new InvalidOperationException("Invalid number of points specified");

            try
            {
                return SqlGeography.STGeomFromText(new SqlChars(PointsToString(points)), (int)srid);
            }
            catch
            {
                list.Reverse();
                return SqlGeography.STGeomFromText(new SqlChars(PointsToString(list)), (int)srid);
            }
        }

        /// <param name="points">Точки. x - это долгота, y - это широта</param>
        public static SqlGeography PointsToGeography(params Point[] points)
        {
            return PointsToGeography(new List<Point>(points));
        }
    }
}
