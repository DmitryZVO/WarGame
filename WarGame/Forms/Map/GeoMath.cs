using OpenCvSharp;
using SharpDX.Mathematics.Interop;

namespace WarGame.Forms.Map;

public class GeoMath
{
    public static double TileSize { get; set; } = 256.0d;
    public static double LonXForTile(int z, int x, int _) // Долгота (x от нулевого меридиана, Longitude)
    {
        var tilesAll = Math.Pow(2, z) / 2.0d;
        return (x / tilesAll - 1.0d) * 180.0d;
    }
    public static double LatYForTile(int z, int _, int y) // Широта (y от экватора, Latitude)
    {
        var a = 6378137.0d;
        var c1 = 0.00335655146887969d;
        var c2 = 0.00000657187271079536d;
        var c3 = 0.00000001764564338702d;
        var c4 = 0.00000000005328478445d;

        var flatY = y * TileSize;
        var mercY = 20037508.342789d - flatY * Math.Pow(2, 23 - z) / 53.5865938d;
        var g = Math.PI / 2.0d - 2.0d * Math.Atan(1.0d / Math.Exp(mercY / a));
        var f = g + c1 * Math.Sin(2.0d * g) + c2 * Math.Sin(4.0d * g) + c3 * Math.Sin(6.0 * g) + c4 * Math.Sin(8.0 * g);

        return f * 180.0d / Math.PI;
    }
    public static int TileXForLon(int z, double lon) // Долгота (x от нулевого меридиана, Longitude)
    {
        var p = Math.Pow(2, z + 8) / 2.0d;
        return (int)Math.Round(p * (1.0d + lon / 180.0d) / TileSize, MidpointRounding.ToNegativeInfinity);
    }
    public static int TileYForLat(int z, double lat) // Широта (y от экватора, Latitude)
    {
        var p = Math.Pow(2, z + 8) / 2.0d;
        var beta = Math.PI * lat / 180.0d;
        var ex = 0.0818191908426d; //эксцентриситет земного эллипсоида. Если тайлы нужно получить для эллиптической проекции Меркатора, то ε = 0.0818191908426.Если для сферической проекции, то ε = 0.По умолчанию в Яндекс.Картах используется эллиптическая проекция Меркатора.
        var phi = (1.0d - ex*Math.Sin(beta)) / (1.0d + ex*Math.Sin(beta));
        var gama = Math.Tan(Math.PI / 4.0d + beta / 2.0d) * Math.Pow(phi, ex / 2.0d);

        return (int)Math.Round(p * (1.0d - Math.Log(gama) / Math.PI) / TileSize, MidpointRounding.ToNegativeInfinity);
    }

    public static double GetLenXForOneTile(int z, double lat, double lon)
    {
        var x0 = TileXForLon(z, lon);
        var y0 = TileYForLat(z, lat);
        return LonXForTile(z, x0 + 1, y0) - LonXForTile(z, x0, y0);
    }

    public static double GetLenYForOneTile(int z, double lat, double lon)
    {
        var x0 = TileXForLon(z, lon);
        var y0 = TileYForLat(z, lat);
        return LatYForTile(z, x0, y0 + 1) - LatYForTile(z, x0, y0);
    }

    public static System.Drawing.Point TileForLon(int z, double lat, double lon) // Широта (y от экватора, Latitude), Долгота (x от нулевого меридиана, Longitude)
    {
        return new System.Drawing.Point(TileXForLon(z, lon), TileYForLat(z, lat));
    }

    public static Rect2d GetTileCoord(int z, double lon, double lat)
    {
        var tx = TileXForLon(z, lon);
        var ty = TileYForLat(z, lat);
        var x0 = LonXForTile(z, tx, ty);
        var y0 = LatYForTile(z, tx, ty);
        var x1 = LonXForTile(z, tx + 1, ty + 1);
        var y1 = LatYForTile(z, tx + 1, ty + 1);
        return new Rect2d(x0, y0, x1 - x0, y1 - y0);
    }

    public static Rect2d GetTileCoord(int z, int tx, int ty)
    {
        var x0 = LonXForTile(z, tx, ty);
        var y0 = LatYForTile(z, tx, ty);
        var x1 = LonXForTile(z, tx + 1, ty + 1);
        var y1 = LatYForTile(z, tx + 1, ty + 1);
        return new Rect2d(x0, y0, x1 - x0, y1 - y0);
    }

    public static bool TileIsVisible(int z, int x, int y)
    {
        var tx = TileXForLon(z, FormMap.GlobalPos.LonX);
        var ty = TileYForLat(z, FormMap.GlobalPos.LatY);
        return x >= tx - FormMap.Map.VisibleTilesCountX / 2 && x <= tx - FormMap.Map.VisibleTilesCountX / 2 && y >= ty - FormMap.Map.VisibleTilesCountY / 2 && y <= ty - FormMap.Map.VisibleTilesCountY / 2;
    }

    public static bool TileIsVisible(int z, double lon, double lat)
    {
        var x = TileXForLon(z, lon);
        var y = TileYForLat(z, lat);
        var tx = TileXForLon(z, FormMap.GlobalPos.LonX);
        var ty = TileYForLat(z, FormMap.GlobalPos.LatY);
        return x >= tx - FormMap.Map.VisibleTilesCountX / 2 && x <= tx + FormMap.Map.VisibleTilesCountX / 2 && y >= ty - FormMap.Map.VisibleTilesCountY / 2 && y <= ty + FormMap.Map.VisibleTilesCountY / 2;
    }

    public static RawVector2 GetScreenSeek(double lonX, double latY)
    {
        var z = FormMap.GlobalPos.Zoom;
        var tileSize = (int)(TileSize + FormMap.GlobalPos.ZoomLocal * TileSize);
        var x0 = TileXForLon(z, FormMap.GlobalPos.LonX);
        var y0 = TileYForLat(z, FormMap.GlobalPos.LatY);
        //var sx0 = LonXForTile(z, x0, y0);
        //var sy0 = LatYForTile(z, x0, y0);

        var x1 = TileXForLon(z, lonX);
        var y1 = TileYForLat(z, latY);
        var sx1 = LonXForTile(z, x1, y1);
        var sy1 = LatYForTile(z, x1, y1);

        var deltaSx = lonX - sx1;
        var deltaSy = latY - sy1;
        var sx = deltaSx / GetLenXForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize + tileSize * (x0 - x1);
        var sy = deltaSy / GetLenYForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize + tileSize * (y0 - y1);
        return new RawVector2((float)sx, (float)sy);
    }
}
