using SharpDX.Mathematics.Interop;
using WarGame.Other;
using WarGame.Remote;

namespace WarGame.Core;

public class Tile
{
    public SharpDX.Direct2D1.Bitmap? Bitmap { get; set; }
    public DateTime TimeCreate { get; set; } = DateTime.MinValue;
    public DateTime TimeLastRequest { get; set; } = DateTime.MaxValue;
    public int Zoom { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Tile(int z, int x, int y)
    {
        Zoom = z;
        X = x;
        Y = y;
        TimeCreate = DateTime.Now;
        TimeLastRequest = DateTime.MaxValue;
    }
}

public class Tiles
{

    public SharpDX.Direct2D1.Bitmap TileNone = SharpDx.NoneBitmap;
    private List<Tile> _tiles = [];
    private SharpDx? _dx;

    public void SetTileNone(SharpDx dx, Bitmap tileNone)
    {
        _dx = dx;
        TileNone = _dx.CreateDxBitmap(tileNone)!;
    }

    public Tile GetTile(int z, int x, int y)
    {
        Tile ret = new Tile(z,x,y);
        ret.TimeCreate = DateTime.Now;
        ret.TimeLastRequest = DateTime.Now;
        ret.Bitmap = TileNone;

        bool find = false;
        lock (_tiles)
        {
            _tiles.RemoveAll(t => t.Zoom != Values.GlobalPos.Zoom);
            _tiles.RemoveAll(t => (DateTime.Now - t.TimeLastRequest).TotalSeconds > 1);
            var t = _tiles.Find(t => t.Zoom == z && t.X == x && t.Y == y);
            if (t != null)
            {
                ret = t;
                find = true;
            }
        }
        if (!find) LoadTileAsync(z, x, y);
        ret.TimeLastRequest = DateTime.Now;
        return ret;
    }

    private async void LoadTileAsync(int z, int x, int y, CancellationToken ct = default)
    {
        var t = new Tile(z, x, y);
        t.TimeCreate = DateTime.Now;
        t.TimeLastRequest = DateTime.Now;
        t.Bitmap = TileNone;

        lock (_tiles)
        {
            if (!_tiles.Exists(t => t.Zoom == z && t.X == x && t.Y == y)) _tiles.Add(t);
        }

        var mat = await Remote.Tiles.GetTileAsync(x, y, z, ct);
        if (mat == null) return;
        t.Bitmap = _dx?.CreateDxBitmap(mat);
    }
}

public class GeoMap
{
    private Tiles _tiles = new();
    public bool TestMode { get; set; }
    public int VisibleTilesCountX { get; set; } = 10; // Ширина сетки тайлов для отрисовки на экран
    public int VisibleTilesCountY { get; set; } = 6; // Высота сетки тайлов для отрисовки на экран

    public void Init(SharpDx dx)
    {
        if (dx.Rt == null) return;

        lock (dx.Rt!)
        {
            _tiles.SetTileNone(dx, Files.NoneBitmap);
        }
    }

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        lock (dx.Rt!)
        {
            if (_tiles.TileNone.Size.Width == Files.NoneBitmap.Width && Remote.Tiles.TileNone != null && !Remote.Tiles.TileNone.Equals(Files.NoneBitmap))
            {
                _tiles.SetTileNone(dx, Remote.Tiles.TileNone);
            }

            dx.Rt.Clear(new RawColor4(0.0f, 0.0f, 0.0f, 0.0f));

            //Отрисовка тайловой сетки
            var z = Values.GlobalPos.Zoom;
            var x0 = GeoMath.TileXForLon(z, Values.GlobalPos.LonX);
            var y0 = GeoMath.TileYForLat(z, Values.GlobalPos.LatY);
            var tileSize = (int)(GeoMath.TileSize + Values.GlobalPos.ZoomLocal * GeoMath.TileSize);
            var sx0 = GeoMath.LonXForTile(z, x0, y0);
            var sy0 = GeoMath.LatYForTile(z, x0, y0);
            var deltaSx = sx0 - Values.GlobalPos.LonX;
            var deltaSy = sy0 - Values.GlobalPos.LatY;
            var sx = deltaSx / GeoMath.GetLenXForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX) * tileSize;
            var sy = deltaSy / GeoMath.GetLenYForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX) * tileSize;
            for (var y = -Values.Map.VisibleTilesCountY / 2; y <= Values.Map.VisibleTilesCountY / 2; y++)
            {
                for (var x = -Values.Map.VisibleTilesCountX / 2; x <= Values.Map.VisibleTilesCountX / 2; x++)
                {
                    var r = new RawRectangleF(dx.BaseWidth / 2.0f + x * tileSize + (float)sx, dx.BaseHeight / 2.0f + y * tileSize + (float)sy, dx.BaseWidth / 2.0f + (x + 1) * tileSize + (float)sx, dx.BaseHeight / 2.0f + (y + 1) * tileSize + (float)sy);
                    var tile = _tiles.GetTile(z, x0 + x, y0 + y);
                    var alpha = (float)Math.Min((DateTime.Now - tile.TimeCreate).TotalSeconds / 0.5f, 1.0f);
                    dx.Rt.DrawBitmap(tile.Bitmap, r, alpha, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                }
            }

            dx.Rt.DrawLine(new RawVector2(dx.BaseWidth / 2.0f, dx.BaseHeight / 2.0f - 6.0f), new RawVector2(dx.BaseWidth / 2.0f, dx.BaseHeight / 2.0f + 6.0f), dx.Brushes.SysTextBrushYellow);
            dx.Rt.DrawLine(new RawVector2(dx.BaseWidth / 2.0f - 6.0f, dx.BaseHeight / 2.0f), new RawVector2(dx.BaseWidth / 2.0f + 6.0f, dx.BaseHeight / 2.0f), dx.Brushes.SysTextBrushYellow);
            var rect = new RawRectangleF(dx.BaseWidth * 0.870f, dx.BaseHeight * 0.003f, dx.BaseWidth * 0.999f, dx.BaseHeight * 0.013f);
            dx.Rt.FillRectangle(rect, dx.Brushes.RoiNone);
            dx.Rt.DrawText($"{Values.GlobalPos.LatY:0.00000000}, {Values.GlobalPos.LonX:0.00000000}, {Values.GlobalPos.Zoom+Values.GlobalPos.ZoomLocal:0.00}/{x0:0}/{y0:0}",
            dx.Brushes.SysText14, rect,dx.Brushes.SysTextBrushYellow);
        }
    }
}
