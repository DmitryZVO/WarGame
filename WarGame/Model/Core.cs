using WarGame.Forms.Map;
using WarGame.Forms.Rls;
using WarGame.Forms.Video;
using WarGame.Forms.Telem;
using WarGame.Other;
using WarGame.Remote;

namespace WarGame.Model;

public static class Core
{
    // Рабочие формы (экраны)
    public static FormMap? FrmMap { get; private set; }
    public static FormRls? FrmRls { get; private set; }
    public static FormVideo? FrmVideo { get; private set; }
    public static FormTelem? FrmTelem { get; private set; }

    // Глобальные объекты
    public static ConfigApp Config { get; private set; } = new();
    public static Files Files { get; private set; } = new();
    public static Server Server { get; private set; } = new();

    public static void Init()
    {
        Config = Config.Load();

        FrmMap = new(new Point(Config.FormMap.PosX, Config.FormMap.PosY), Config.FormMap.Fps);
        FrmRls = new(new Point(Config.FormRls.PosX, Config.FormRls.PosY), Config.FormRls.Fps);
        FrmVideo = new(new Point(Config.FormVideo.PosX, Config.FormVideo.PosY), Config.FormVideo.Fps);
        FrmTelem = new(new Point(Config.FormTelem.PosX, Config.FormTelem.PosY), Config.FormTelem.Fps);

        if (Config.FormMap.Enable) FrmMap.Show();
        if (Config.FormRls.Enable) FrmRls.Show();
        if (Config.FormVideo.Enable) FrmVideo.Show();
        if (Config.FormTelem.Enable) FrmTelem.Show();

        Server.Init();
    }

    public static void DeInit()
    {
        Config.Save();
    }
}
