namespace SpatialSearch;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    
    public Point(List<string> entries)
    {
        throw new NotImplementedException();
    }
    
    public Point(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public override string ToString()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}