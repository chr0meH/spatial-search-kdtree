
using System.Globalization;

namespace SpatialSearch;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    public readonly string Type;
    public readonly string Name;
    public Point(List<string> entries)
    {
        Latitude = double.Parse(entries[0].Replace(',', '.'), CultureInfo.InvariantCulture);
        Longitude = double.Parse(entries[1].Replace(',', '.'), CultureInfo.InvariantCulture);
        if (entries.Count >= 5)
        {
            Type = entries[2];
            Name = entries[4];
        }
        else
        {
            Type = "";
            Name = "";
        }
    }
    
    public Point(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        Type = "";
        Name = "";
    }
    
    public override string ToString()
    {
        return $"{Latitude};{Longitude};{Type};{Name}";
        
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return this.Latitude == other.Latitude && this.Longitude == other.Longitude;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Math.Round(Latitude, 7), Math.Round(Longitude, 7));
    }
}