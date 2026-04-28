using System;

namespace SpatialSearch;

public static class Haversin
{
    private const double EarthRadiusKm = 6371.0;

    public static double HaversinDistance(Point p1, Point p2)
    {
        double dLat = ToRadians(p2.Latitude - p1.Latitude); 
        double dLon = ToRadians(p2.Longitude - p1.Longitude);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(p1.Latitude)) * Math.Cos(ToRadians(p2.Latitude)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadiusKm * c;
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}