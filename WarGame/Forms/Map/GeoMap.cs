using SharpDX.Mathematics.Interop;
using WarGame.Model;

namespace WarGame.Forms.Map;

public class Tile(int z, int x, int y)
{
    public SharpDX.Direct2D1.Bitmap? Bitmap { get; set; }
    public DateTime TimeCreate { get; set; } = DateTime.Now;
    public DateTime TimeLastRequest { get; set; } = DateTime.MaxValue;
    public int Zoom { get; set; } = z;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
}

public class Tiles
{
    private readonly List<Tile> _tiles = [];

    public Tile GetTile(SharpDx dx, int z, int x, int y)
    {
        Tile ret = new(z, x, y)
        {
            TimeCreate = DateTime.Now,
            TimeLastRequest = DateTime.Now,
        };

        bool find = false;
        lock (_tiles)
        {
            var time = DateTime.Now;
            _tiles.FindAll(t => t.Zoom != Core.Config.Map.Zoom).ForEach(t=>t.Bitmap?.Dispose());
            _tiles.RemoveAll(t => t.Zoom != Core.Config.Map.Zoom);
            _tiles.FindAll(t => (time - t.TimeLastRequest).TotalSeconds > 1).ForEach(t => t.Bitmap?.Dispose());
            _tiles.RemoveAll(t => (time - t.TimeLastRequest).TotalSeconds > 1);
            var t = _tiles.Find(t => t.Zoom == z && t.X == x && t.Y == y);
            if (t != null)
            {
                ret = t;
                find = true;
            }
        }
        if (!find) LoadTileAsync(dx, z, x, y);
        ret.TimeLastRequest = DateTime.Now;
        return ret;
    }

    private async void LoadTileAsync(SharpDx dx, int z, int x, int y, CancellationToken ct = default)
    {
        var t = new Tile(z, x, y)
        {
            TimeCreate = DateTime.Now,
            TimeLastRequest = DateTime.Now,
        };

        lock (_tiles)
        {
            if (!_tiles.Exists(t => t.Zoom == z && t.X == x && t.Y == y)) _tiles.Add(t);
        }

        var mat = await Remote.Files.GetTileAsync(x, y, z, ct);
        if (mat == null) return;
        t.Bitmap = dx?.CreateDxBitmap(mat);
        mat.Dispose();
        mat = null;
    }
}

public class GeoMap
{
    private readonly Tiles _tiles = new();
    public bool EditMode { get; set; }
    public bool EditNeedSave { get; set; }
    public bool TestMode { get; set; }
    public int VisibleTilesCountX { get; set; } = 10; // Ширина сетки тайлов для отрисовки на экран
    public int VisibleTilesCountY { get; set; } = 6; // Высота сетки тайлов для отрисовки на экран

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        lock (dx.Rt!)
        {
            dx.Rt.Clear(new RawColor4(0.0f, 0.0f, 0.0f, 0.0f));

            //Отрисовка тайловой сетки
            var z = Core.Config.Map.Zoom;
            var x0 = GeoMath.TileXForLon(z, Core.Config.Map.LonX);
            var y0 = GeoMath.TileYForLat(z, Core.Config.Map.LatY);
            var tileSize = (int)(GeoMath.TileSize + Core.Config.Map.ZoomLocal * GeoMath.TileSize);
            var sx0 = GeoMath.LonXForTile(z, x0, y0);
            var sy0 = GeoMath.LatYForTile(z, x0, y0);
            var deltaSx = sx0 - Core.Config.Map.LonX;
            var deltaSy = sy0 - Core.Config.Map.LatY;
            var sx = deltaSx / GeoMath.GetLenXForOneTile(Core.Config.Map.Zoom, Core.Config.Map.LatY, Core.Config.Map.LonX) * tileSize;
            var sy = deltaSy / GeoMath.GetLenYForOneTile(Core.Config.Map.Zoom, Core.Config.Map.LatY, Core.Config.Map.LonX) * tileSize;
            for (var y = -FormMap.Map.VisibleTilesCountY / 2; y <= FormMap.Map.VisibleTilesCountY / 2; y++)
            {
                for (var x = -FormMap.Map.VisibleTilesCountX / 2; x <= FormMap.Map.VisibleTilesCountX / 2; x++)
                {
                    var r = new RawRectangleF(dx.BaseWidth / 2.0f + x * tileSize + (float)sx, dx.BaseHeight / 2.0f + y * tileSize + (float)sy, dx.BaseWidth / 2.0f + (x + 1) * tileSize + (float)sx, dx.BaseHeight / 2.0f + (y + 1) * tileSize + (float)sy);
                    var tile = _tiles.GetTile(dx, z, x0 + x, y0 + y);
                    var alpha = (float)Math.Min((DateTime.Now - tile.TimeCreate).TotalSeconds / 0.5f, 1.0f);
                    dx.Rt.DrawBitmap(tile.Bitmap ?? ((SharpDxMap)dx).BitmapNone, r, alpha, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                }
            }
            dx.Rt.DrawLine(new RawVector2(dx.BaseWidth / 2.0f, dx.BaseHeight / 2.0f - 6.0f), new RawVector2(dx.BaseWidth / 2.0f, dx.BaseHeight / 2.0f + 6.0f), dx.Brushes.SysTextBrushYellow);
            dx.Rt.DrawLine(new RawVector2(dx.BaseWidth / 2.0f - 6.0f, dx.BaseHeight / 2.0f), new RawVector2(dx.BaseWidth / 2.0f + 6.0f, dx.BaseHeight / 2.0f), dx.Brushes.SysTextBrushYellow);
            var rect = new RawRectangleF(dx.BaseWidth * 0.870f, dx.BaseHeight * 0.003f, dx.BaseWidth * 0.999f, dx.BaseHeight * 0.013f);
            dx.Rt.FillRectangle(rect, dx.Brushes.RoiNone);
            dx.Rt.DrawText($"{Core.Config.Map.LatY:0.000000}, {Core.Config.Map.LonX:0.000000}, {Core.Config.Map.Zoom+ Core.Config.Map.ZoomLocal:0.00}/{x0:0}/{y0:0}",
            dx.Brushes.SysText14, rect,dx.Brushes.SysTextBrushYellow);

            rect = new RawRectangleF(dx.BaseWidth * 0.870f, dx.BaseHeight * 0.013f, dx.BaseWidth * 0.999f, dx.BaseHeight * 0.023f);
            dx.Rt.FillRectangle(rect, dx.Brushes.RoiNone);
            var posMouse = GeoMath.ScreenPositionToGps(dx, Control.MousePosition.X, Control.MousePosition.Y);
            dx.Rt.DrawText($"{posMouse.Y:0.000000}, {posMouse.X:0.000000}", dx.Brushes.SysText14, rect, dx.Brushes.SysTextBrushYellow);
        }
    }
}
