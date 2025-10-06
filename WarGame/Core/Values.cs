using WarGame.Remote;

namespace WarGame.Core;

public static class Values
{
    public static string ServerDataUrl { get; } = "http://212.12.7.116:1111"; // Тестовый сервер офис

    public static GeoPosition GlobalPos { get; private set; } = new();
    public static GeoMap Map { get; private set; } = new();
    public static ControlUser ControlUser { get; private set; } = new();
    public static ObjectsStatic ObjectsStatic { get; private set; } = new();
    public static Files Files { get; private set; } = new();

    public static void Init()
    {
        Files.Load();
        GlobalPos.Init();
        ObjectsStatic.Init();
    }
}
