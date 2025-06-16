using OpenCvSharp;

namespace WarGame.Core;

public class GeoPosition
{
    public double LonX { get; set; } // Стартовая точка системы (Lon, Lat)
    public double LatY { get; set; } // Стартовая точка системы (Lon, Lat)
    public int Zoom { get; set; } = 16; // Глобальный zoom
    public double ZoomLocal { get; set; } = 1.0d; // Локальный zoom

    public void Init()
    {
        // ЕЛЬКИНО
        LonX = 37.542351d;
        LatY = 54.151851d;
    }
}
