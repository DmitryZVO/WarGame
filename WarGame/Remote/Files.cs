using OpenCvSharp;
using WarGame.Model;

namespace WarGame.Remote;

public class Files
{
    public static async Task<Bitmap?> GetSpriteAsync(string type, string name, CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetFile?type={type}&name={name}", ct);
            return !answ.IsSuccessStatusCode ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(await answ.Content.ReadAsStringAsync(ct))));
        }
        catch
        {
            return null;
        }
    }

    public static async Task<Mat?> GetTileAsync(int x, int y, int z, CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetTile?x={x:0}&y={y:0}&z={z:0}", ct);
            return !answ.IsSuccessStatusCode ? null : Cv2.ImDecode(Convert.FromBase64String(await answ.Content.ReadAsStringAsync(ct)), ImreadModes.Unchanged);
        }
        catch
        {
            return null;
        }
    }
}
