using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace WarGame.Other;

public struct ConfigApp
{
    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
    };

    public bool TestMode { get; set; } = false;
    public string ServerUrl { get; set; } = "http://212.12.7.116:1111";
    public FormPos FormMap { get; set; } = new FormPos();
    public FormPos FormRls { get; set; } = new FormPos();
    public FormPos FormVideo { get; set; } = new FormPos();
    public FormPos FormTelem { get; set; } = new FormPos();

    public ConfigApp()
    {
    }

    public ConfigApp Load()
    {
        try
        {
            using var sr = new StreamReader(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "_global.ini", FileMode.Open));
            this = JsonSerializer.Deserialize<ConfigApp>(sr.ReadToEnd());
        }
        catch
        {
            Save();
        }
        return this;
    }

    public readonly bool Save()
    {
        try
        {
            using var sw = new StreamWriter(new FileStream(AppDomain.CurrentDomain.BaseDirectory + "_global.ini", FileMode.Create, FileAccess.Write));
            sw.WriteLine(JsonSerializer.Serialize(this,
                _options));
        }
        catch
        {
            return false;
        }
        return true;
    }
    public class FormPos
    {
        public bool Enable { get; set; } = true;
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Fps { get; set; } = 60;
    }
}
