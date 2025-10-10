using OpenCvSharp;
using WarGame.Model;

namespace WarGame.Remote;

public class Tiles
{
    public static Bitmap? TileNone { get; set; }
    public static async Task<Mat?> GetTileAsync(int x, int y, int z, CancellationToken ct = default)
    {
        Mat? ret = null;
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetTile?x={x:0}&y={y:0}&z={z:0}", ct);
            ret = !answ.IsSuccessStatusCode ? null : Cv2.ImDecode(Convert.FromBase64String(await answ.Content.ReadAsStringAsync(ct)), ImreadModes.Unchanged);
        }
        catch
        {
            //
        }
        return ret;
    }
}
