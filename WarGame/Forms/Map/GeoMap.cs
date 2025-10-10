using SharpDX.Mathematics.Interop;
using WarGame.Forms;
using WarGame.Model;
using WarGame.Remote;

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

    public SharpDX.Direct2D1.Bitmap TileNone = SharpDx.NoneBitmap;
    private readonly List<Tile> _tiles = [];
    private SharpDx? _dx;

    public void SetTileNone(SharpDx dx, Bitmap tileNone)
    {
        _dx = dx;
        TileNone = _dx.CreateDxBitmap(tileNone)!;
    }

    public Tile GetTile(int z, int x, int y)
    {
        Tile ret = new(z, x, y)
        {
            TimeCreate = DateTime.Now,
            TimeLastRequest = DateTime.Now,
            Bitmap = TileNone
        };

        bool find = false;
        lock (_tiles)
        {
            var time = DateTime.Now;
            _tiles.FindAll(t => t.Zoom != FormMap.GlobalPos.Zoom).ForEach(t=>t.Bitmap?.Dispose());
            _tiles.RemoveAll(t => t.Zoom != FormMap.GlobalPos.Zoom);
            _tiles.FindAll(t => (time - t.TimeLastRequest).TotalSeconds > 1).ForEach(t => t.Bitmap?.Dispose());
            _tiles.RemoveAll(t => (time - t.TimeLastRequest).TotalSeconds > 1);
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
        var t = new Tile(z, x, y)
        {
            TimeCreate = DateTime.Now,
            TimeLastRequest = DateTime.Now,
            Bitmap = TileNone
        };

        lock (_tiles)
        {
            if (!_tiles.Exists(t => t.Zoom == z && t.X == x && t.Y == y)) _tiles.Add(t);
        }

        var mat = await Remote.Tiles.GetTileAsync(x, y, z, ct);
        if (mat == null) return;
        t.Bitmap = _dx?.CreateDxBitmap(mat);
        mat.Dispose();
        mat = null;
    }
}

public class GeoMap
{
    private readonly Tiles _tiles = new();
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
            var z = FormMap.GlobalPos.Zoom;
            var x0 = GeoMath.TileXForLon(z, FormMap.GlobalPos.LonX);
            var y0 = GeoMath.TileYForLat(z, FormMap.GlobalPos.LatY);
            var tileSize = (int)(GeoMath.TileSize + FormMap.GlobalPos.ZoomLocal * GeoMath.TileSize);
            var sx0 = GeoMath.LonXForTile(z, x0, y0);
            var sy0 = GeoMath.LatYForTile(z, x0, y0);
            var deltaSx = sx0 - FormMap.GlobalPos.LonX;
            var deltaSy = sy0 - FormMap.GlobalPos.LatY;
            var sx = deltaSx / GeoMath.GetLenXForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize;
            var sy = deltaSy / GeoMath.GetLenYForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize;
            for (var y = -FormMap.Map.VisibleTilesCountY / 2; y <= FormMap.Map.VisibleTilesCountY / 2; y++)
            {
                for (var x = -FormMap.Map.VisibleTilesCountX / 2; x <= FormMap.Map.VisibleTilesCountX / 2; x++)
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
            dx.Rt.DrawText($"{FormMap.GlobalPos.LatY:0.00000000}, {FormMap.GlobalPos.LonX:0.00000000}, {FormMap.GlobalPos.Zoom+ FormMap.GlobalPos.ZoomLocal:0.00}/{x0:0}/{y0:0}",
            dx.Brushes.SysText14, rect,dx.Brushes.SysTextBrushYellow);
        }
    }
}
