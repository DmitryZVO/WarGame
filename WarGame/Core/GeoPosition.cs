using OpenCvSharp;

namespace WarGame.Core;

public class GeoPosition
{
    public double LonX { get; set; } // Стартовая точка системы (Lon, Lat)
    public double LatY { get; set; } // Стартовая точка системы (Lon, Lat)
    public int Zoom { get; set; } = 12; // Глобальный zoom
    public double ZoomLocal { get; set; } = 1.0d; // Локальный zoom
    public double ZoomLocalStep0 = 2.0d; // Максимальный зум для уровня 0 (земля)
    public double ZoomLocalStep1 = 4.0d; // Максимальный зум для уровня 1 (города)

    public void Init()
    {
        // ЕЛЬКИНО
        LonX = 37.542351d;
        LatY = 54.151851d;
    }
}
