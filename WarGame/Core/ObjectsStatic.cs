using WarGame.Other;

namespace WarGame.Core;

public class ObjectsStatic
{
    public List<ObjectStatic> Items { get; set; } = new();
    public void Init()
    {
        Items.Add(new ObjectStatic()
        {
            Name = "Тула",
            Visible = true,
            Type = 0,
            LonX = 37.617348d,
            LatY = 54.193122d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Москва", //55.755864, 37.617698
            Visible = true,
            Type = 0,
            LonX = 37.617698d,
            LatY = 55.755864d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Керчь", // 45.356219, 36.467378
            Visible = true,
            Type = 0,
            LonX = 36.467378d,
            LatY = 45.356219d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Феодосия", // 45.026654, 35.383911
            Visible = true,
            Type = 0,
            LonX = 35.383911d,
            LatY = 45.026654d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Севастополь", // 44.616020, 33.524471
            Visible = true,
            Type = 0,
            LonX = 33.524471d,
            LatY = 44.616020d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Черноморское", // 45.509381, 32.703536
            Visible = true,
            Type = 0,
            LonX = 32.703536d,
            LatY = 45.509381d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Новороссийск", // 44.723771, 37.768813
            Visible = true,
            Type = 0,
            LonX = 37.768813d,
            LatY = 44.723771d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Джубга", // 44.314735, 38.701623
            Visible = true,
            Type = 0,
            LonX = 38.701623d,
            LatY = 44.314735d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Сочи", // 43.585472, 39.723098
            Visible = true,
            Type = 0,
            LonX = 39.723098d,
            LatY = 43.585472d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Санкт-Петербург", // 59.938784, 30.314997
            Visible = true,
            Type = 0,
            LonX = 30.314997d,
            LatY = 59.938784d,
        });
        Items.Add(new ObjectStatic()
        {
            Name = "Одесса", // 59.938784, 30.314997
            Visible = true,
            Type = 0,
            LonX = 30.731689d,
            LatY = 46.484213d,
        });
    }

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        lock (dx.Rt!)
        {
            Items.ForEach(i => i.Draw(dx));
        }
    }
}

public class ObjectStatic
{
    public int Type { get; set; } = 0; // Город
    public double LonX { get; set; }
    public double LatY { get; set; }
    public bool Visible { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        if (!GeoMath.TileIsVisible(Values.GlobalPos.Zoom, LonX, LatY)) return;
        var tileSize = (int)(GeoMath.TileSize + Values.GlobalPos.ZoomLocal * GeoMath.TileSize);
        var tileObjectX = GeoMath.TileXForLon(Values.GlobalPos.Zoom, LonX);
        var tileObjectY = GeoMath.TileYForLat(Values.GlobalPos.Zoom, LatY);
        var tileCenterX = GeoMath.TileXForLon(Values.GlobalPos.Zoom, Values.GlobalPos.LonX);
        var tileCenterY = GeoMath.TileYForLat(Values.GlobalPos.Zoom, Values.GlobalPos.LatY);
        var posX = (tileObjectX - tileCenterX) * tileSize;
        var posY = (tileObjectY - tileCenterY) * tileSize;
        var tileCenter = GeoMath.GetTileCoord(Values.GlobalPos.Zoom, Values.GlobalPos.LonX, Values.GlobalPos.LatY);
        var deltaSx = tileCenter.Left - Values.GlobalPos.LonX;
        var deltaSy = tileCenter.Top - Values.GlobalPos.LatY;
        var sx = deltaSx / GeoMath.GetLenXForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX) * tileSize;
        var sy = deltaSy / GeoMath.GetLenYForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX) * tileSize;

        var tileObject = GeoMath.GetTileCoord(Values.GlobalPos.Zoom, LonX, LatY);
        var objectSx = LonX - tileObject.Left;
        var objectSy = LatY - tileObject.Top;
        var ox = objectSx / GeoMath.GetLenXForOneTile(Values.GlobalPos.Zoom, LatY, LonX) * tileSize;
        var oy = objectSy / GeoMath.GetLenYForOneTile(Values.GlobalPos.Zoom, LatY, LonX) * tileSize;

        var pos = new SharpDX.Mathematics.Interop.RawVector2(dx.BaseWidth / 2.0f + posX + (float)sx + (float)ox, dx.BaseHeight / 2.0f + posY + (float)sy + (float)oy);
        var radius = 5.0f;
        dx.Rt.FillEllipse(new SharpDX.Direct2D1.Ellipse(pos, radius, radius), dx.Brushes.SysTextBrushYellow);
        dx.Rt.DrawEllipse(new SharpDX.Direct2D1.Ellipse(pos, radius, radius), dx.Brushes.SysTextBrushWhite, 2.0f);
        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new SharpDX.Mathematics.Interop.RawRectangleF(pos.X + 2 * radius, pos.Y - 2 * radius, pos.X + 100 * radius, pos.Y - 4 * radius), dx.Brushes.SysTextBrushYellow);
    }
}
