namespace WarGame.Other;

public class GeoMap
{
    // Храм Василия Блаженного, МСК = 55.750935, 37.617178;
    public static double LonForTile(int z, int x, int y) // Долгота (x от нулевого меридиана, Longitude)
    {
        var tilesAll = Math.Pow(2, z) / 2.0d;
        return (x / tilesAll - 1.0d) * 180.0d;
    }
    public static double LatForTile(int z, int x, int y) // Широта (y от экватора, Latitude)
    {
        var a = 6378137.0d;
        var c1 = 0.00335655146887969d;
        var c2 = 0.00000657187271079536d;
        var c3 = 0.00000001764564338702d;
        var c4 = 0.00000000005328478445d;

        var flatY = y * 256.0d;
        var mercY = 20037508.342789d - flatY * Math.Pow(2, 23 - z) / 53.5865938d;
        var g = Math.PI / 2.0d - 2.0d * Math.Atan(1.0d / Math.Exp(mercY / a));
        var f = g + c1 * Math.Sin(2.0d * g) + c2 * Math.Sin(4.0d * g) + c3 * Math.Sin(6.0 * g) + c4 * Math.Sin(8.0 * g);

        return (f * 180.0d) / Math.PI;
    }
    public static int TileXForLon(int z, double lon) // Долгота (x от нулевого меридиана, Longitude)
    {
        var p = Math.Pow(2, z + 8) / 2.0d;
        return (int)Math.Round((p * (1.0d + lon / 180.0d)) / 256.0d, MidpointRounding.ToNegativeInfinity);
    }
    public static int TileYForLat(int z, double lat) // Широта (y от экватора, Latitude)
    {
        var p = Math.Pow(2, z + 8) / 2.0d;
        var beta = (Math.PI * lat) / 180.0d;
        var ex = 0.0818191908426d; //эксцентриситет земного эллипсоида. Если тайлы нужно получить для эллиптической проекции Меркатора, то ε = 0.0818191908426.Если для сферической проекции, то ε = 0.По умолчанию в Яндекс.Картах используется эллиптическая проекция Меркатора.
        var phi = (1.0d - ex*Math.Sin(beta)) / (1.0d + ex*Math.Sin(beta));
        var gama = Math.Tan(Math.PI / 4.0d + beta / 2.0d) * Math.Pow(phi, ex / 2.0d);

        return (int)Math.Round((p * (1.0d - Math.Log(gama) / Math.PI)) / 256.0d, MidpointRounding.ToNegativeInfinity);
    }

    public static Point TileForLon(int z, double lat, double lon) // Широта (y от экватора, Latitude), Долгота (x от нулевого меридиана, Longitude)
    {
        return new Point(TileXForLon(z, lon), TileYForLat(z, lat));
    }

}
