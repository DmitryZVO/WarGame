using WarGame.Model;
using WarGame.Remote;
using WarGame.Resources;

namespace WarGame.Forms.Map;

public sealed partial class FormMap : Form
{
    public static GeoMap Map { get; private set; } = new();
    public static StaticObjects ObjectsStatic { get; private set; } = new();

    private Point _posFromDisplays;
    private readonly SharpDx _dx;

    private int _lastMouseX = 0;
    private int _lastMouseY = 0;
    private bool _lastMouseLeft;
    private bool _lastMouseRight;
    private bool _lastMapMove;

    private readonly ContextMenuStrip _contextMenuMapEdit = new();
    private readonly ContextMenuStrip _contextMenuVertexEdit = new();
    private readonly ContextMenuStrip _contextMenuStaticObjectEdit = new();

    private void UpdateLastMouseState()
    {
        _lastMouseX = MousePosition.X;
        _lastMouseY = MousePosition.Y;
        _lastMouseLeft = MouseButtons == MouseButtons.Left;
        _lastMouseRight = MouseButtons == MouseButtons.Right;
    }

    public FormMap(Point pos, int fps)
    {
        InitializeComponent();

        _posFromDisplays = pos;

        _dx = new SharpDxMap(pictureBoxMain, fps);

        //GlobalPos.Init();
        ObjectsStatic.Init(_dx);

        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        Shown += FormShown;
        Closed += FormOnClosing;
        pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
        pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;
        pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
        pictureBoxMain.MouseWheel += PictureBoxMain_MouseWheel;
        buttonEdit.Click += ButtonEdit_Click;

        // Создаем менюшки для карты
        var item = new ToolStripMenuItem("Добавить");
        item.DropDownItems.Add("Метка");
        item.DropDownItems.Add("Полигон");
        item.DropDownItems.Add("Населенный пункт");
        _contextMenuMapEdit.Items.Add(item);

        _contextMenuVertexEdit.Items.Add("Добавить вершину", null, (_, _) => { AddVertex(); });
        _contextMenuVertexEdit.Items.Add("Удалить вершину", null, (_, _) => { DeleteVertex(); });

        _contextMenuStaticObjectEdit.Items.Add("Удалить объект", null, (_, _) => { DeleteStaticObject(); });

        UpdateLastMouseState();
    }

    private static void DeleteStaticObject()
    {
        ObjectsStatic.Items.RemoveAll(x => x.Selected);
        Map.EditNeedSave = true;
    }

    private static void DeleteVertex()
    {
        var poly = ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (poly == null) return;
        if (poly.Coords.Count <= 3)
        {
            MessageBox.Show("Полигон не может содержать менее трех вершин!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        poly.Coords.RemoveAll(x => x.Selected);
        Map.EditNeedSave = true;
    }

    private static void AddVertex()
    {
        var poly = ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (poly == null) return;
        var vert = poly.Coords.FindIndex(x => x.Selected);
        if (vert <0) return;
        poly.Coords.ForEach(x => x.Selected = false);
        poly.Coords.ForEach(x => x.Lighting = false);
        poly.Coords.Insert(vert+1, new StaticObject.Coord() { Selected = true, Lighting = false, X = poly.Coords[vert].X, Y = poly.Coords[vert].Y });
        Map.EditNeedSave = true;
    }

    private async void ButtonEdit_Click(object? sender, EventArgs e)
    {
        if (!Map.EditMode) Map.EditNeedSave = false;
        if (Map.EditMode && Map.EditNeedSave)
        {
            var quest = MessageBox.Show("Сохранить изменения?", "СОХРАНИТЬ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (quest == DialogResult.Yes) // Сохраняем изменения
            {
                if (!await ObjectsStatic.ChangeAsync())
                {
                    MessageBox.Show("Сохранение не удалось!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (quest == DialogResult.No) // Не сохраняем изменения
            {
                ObjectsStatic.TimeStamp = 0;
            }
            else
            {
                return;
            }
            Map.EditNeedSave = false;
        }
        Map.EditMode = !Map.EditMode;
        buttonEdit.BackColor = Map.EditMode ? Color.LightGreen : Color.White;
    }

    private void PictureBoxMain_MouseWheel(object? sender, MouseEventArgs e)
    {
        var zoom = Core.Config.Map.ZoomLocal + Math.Sign(e.Delta) * 0.2d;
        var mouseScreenX = MousePosition.X;
        var mouseScreenY = MousePosition.Y;
        var mouseGps = GeoMath.ScreenPositionToGps(_dx, mouseScreenX, mouseScreenY);
        switch (Core.Config.Map.Zoom)
        {
            case 6:
                if (zoom < 0)
                {
                    Core.Config.Map.Zoom = 6;
                    Core.Config.Map.ZoomLocal = 0.0d;
                }
                else if (zoom >= Core.Config.Map.ZoomLocalStep0)
                {
                    Core.Config.Map.Zoom = 8;
                    Core.Config.Map.ZoomLocal = zoom - Core.Config.Map.ZoomLocalStep0;
                }
                else
                {
                    Core.Config.Map.ZoomLocal = zoom;
                }
                break;
            case 8:
                if (zoom < 0)
                {
                    Core.Config.Map.Zoom = 6;
                    Core.Config.Map.ZoomLocal = zoom + Core.Config.Map.ZoomLocalStep0;
                }
                else if (zoom >= Core.Config.Map.ZoomLocalStep1)
                {
                    Core.Config.Map.Zoom = 12;
                    Core.Config.Map.ZoomLocal = zoom - Core.Config.Map.ZoomLocalStep1;
                }
                else
                {
                    Core.Config.Map.ZoomLocal = zoom;
                }
                break;
            case 12:
                if (zoom < 0)
                {
                    Core.Config.Map.Zoom = 8;
                    Core.Config.Map.ZoomLocal = zoom + Core.Config.Map.ZoomLocalStep1;
                }
                else if (zoom >= Core.Config.Map.ZoomLocalStep1)
                {
                    Core.Config.Map.Zoom = 16;
                    Core.Config.Map.ZoomLocal = zoom - Core.Config.Map.ZoomLocalStep1;
                }
                else
                {
                    Core.Config.Map.ZoomLocal = zoom;
                }
                break;
            case 16:
                if (zoom < 0)
                {
                    Core.Config.Map.Zoom = 12;
                    Core.Config.Map.ZoomLocal = zoom + Core.Config.Map.ZoomLocalStep1;
                }
                else if (zoom >= Core.Config.Map.ZoomLocalStep1)
                {
                    Core.Config.Map.Zoom = 16;
                    Core.Config.Map.ZoomLocal = Core.Config.Map.ZoomLocalStep1;
                }
                else
                {
                    Core.Config.Map.ZoomLocal = zoom;
                }
                break;
            default:
                break;
        }
        var mouseNewPosScreen = GeoMath.GpsPositionToScreen(_dx, mouseGps.X, mouseGps.Y);
        var deltaX = mouseNewPosScreen.X - mouseScreenX;
        var deltaY = mouseNewPosScreen.Y - mouseScreenY;
        MoveMapOnDeltaScreen(deltaX, deltaY);
        UpdateLastMouseState();
    }

    private void PictureBoxMain_MouseUp(object? sender, MouseEventArgs e)
    {
        if (_lastMapMove) return; // Было движение карты, игнорируем любые нажатия

        if (e.Button == MouseButtons.Left)
        {
        }
        else if (e.Button == MouseButtons.Right)
        {
            var buttonOk = false;
            // Есть выделенный объект
            var obj = ObjectsStatic.Items.Find(x => x.Selected);
            if (!buttonOk && obj != null && obj.Lighting)
            {
                if (Map.EditMode)
                {
                    _contextMenuStaticObjectEdit.Show(new Point(_lastMouseX, _lastMouseY));
                }
                buttonOk = true;
            }
            obj = ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
            var vert = obj?.Coords.Find(x => x.Selected);
            //Есть выделенная вершина
            if (!buttonOk && vert != null && vert.Lighting)
            {
                if (Map.EditMode)
                {
                    _contextMenuVertexEdit.Show(new Point(_lastMouseX, _lastMouseY));
                }
                buttonOk = true;
            }
            //Нет выделенных объектов
            if (!buttonOk && !ObjectsStatic.Items.Any(x => x.Coords.Any(y => y.Lighting)) && !ObjectsStatic.Items.Any(x => x.Lighting))
            {
                if (Map.EditMode)
                {
                    _contextMenuMapEdit.Show(new Point(_lastMouseX, _lastMouseY));
                }
            }
        }
        UpdateLastMouseState();
    }

    private static void MoveMapOnDeltaScreen(double dx, double dy)
    {
        Core.Config.Map.LonX += (dx / GeoMath.TileSize) * GeoMath.GetLenXForOneTile(Core.Config.Map.Zoom + Core.Config.Map.ZoomLocal, Core.Config.Map.LatY, Core.Config.Map.LonX);
        Core.Config.Map.LatY += (dy / GeoMath.TileSize) * GeoMath.GetLenYForOneTile(Core.Config.Map.Zoom + Core.Config.Map.ZoomLocal, Core.Config.Map.LatY, Core.Config.Map.LonX);
    }

    private void PictureBoxMain_MouseMove(object? sender, MouseEventArgs e)
    {
        // Перемещение карты
        if (e.Button == MouseButtons.Right && e.Button != MouseButtons.Left && _lastMouseRight) 
        {
            _lastMapMove = true;
            var dX = _lastMouseX - e.X;
            var dY = _lastMouseY - e.Y;
            MoveMapOnDeltaScreen(dX, dY);
        }

        // Перемещение объектов
        if (e.Button == MouseButtons.Left && e.Button != MouseButtons.Right)
        {
            var obj = ObjectsStatic.Items.Find(x => x.Selected || x.Coords.Any(y => y.Selected));
            if (obj != null)
            {
                var gpsPosNew = GeoMath.ScreenPositionToGps(_dx, e.X, e.Y);
                if (obj.Selected)
                {
                    if (Map.EditMode)
                    {
                        obj.Coords[0].X = gpsPosNew.X;
                        obj.Coords[0].Y = gpsPosNew.Y;
                        Map.EditNeedSave = true;
                    }
                }
                else
                {
                    var vert = obj.Coords.Find(x => x.Selected);
                    if (vert != null)
                    {
                        if (Map.EditMode)
                        {
                            vert.X = gpsPosNew.X;
                            vert.Y = gpsPosNew.Y;
                            Map.EditNeedSave = true;
                        }
                    }
                }
            }
        }
        UpdateLastMouseState();
    }

    private void PictureBoxMain_MouseDown(object? sender, MouseEventArgs e)
    {
        _lastMapMove = false;
        // Левая кнопка мыши
        if (e.Button == MouseButtons.Left)
        {
            var selected = false;
            // Выделение и выбор объектов
            ObjectsStatic.Items.ForEach(x => x.Selected = false);
            if (!selected && ObjectsStatic.Items.Any(x => x.Lighting))
            {
                ObjectsStatic.Items.Find(x => x.Lighting)!.Selected = true;
                selected = true;
            }

            ObjectsStatic.Items.ForEach(x => x.Coords.ForEach(y => y.Selected = false));
            if (!selected && ObjectsStatic.Items.Any(x => x.Coords.Any(y => y.Lighting)))
            {
                ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Lighting == true))!.Coords.Find(x => x.Lighting)!.Selected = true;
                selected = true;
            }
        }
        // Правая кнопка мыши
        else if (e.Button == MouseButtons.Right)
        {
        }

        UpdateLastMouseState();
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
    }

    private void FormShown(object? sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual; // Ручная позиция окна
        Location = _posFromDisplays; // Поместить в нужный монитор

        _ = _dx.StartAsync(default);
        buttonEdit.BackColor = Color.White;
    }
}
