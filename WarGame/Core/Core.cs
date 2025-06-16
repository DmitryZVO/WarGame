namespace WarGame.Core;

public static class Values
{
    public static GeoPosition GlobalPos { get; private set; } = new();
    public static GeoMap Map { get; private set; } = new();
    public static ControlUser ControlUser { get; private set; } = new();
    public static ObjectsStatic ObjectsStatic { get; private set; } = new();

    public static void Init()
    {
        GlobalPos.Init();
        ObjectsStatic.Init();
    }
}
