using WarGame.Core;

namespace WarGame.Remote;

public class Files
{
    public static Bitmap NoneBitmap { get; private set; } = new Bitmap(1, 1);

    public static async Task<Bitmap> GetFileAsync(string type, string name, CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Values.ServerDataUrl);
            using var answ = await web.GetAsync($"GetFile?type={type}&name={name}", ct);
            return !answ.IsSuccessStatusCode ? NoneBitmap : new Bitmap(new MemoryStream(Convert.FromBase64String(await answ.Content.ReadAsStringAsync(ct))));
        }
        catch
        {
            //
        }
        return NoneBitmap;
    }

    public async void Load()
    {
        Tiles.TileNone = await GetFileAsync("Sprites", "TileNone.png");
    }
}
