using System.Text.Json;
using WarGame.Model;

namespace WarGame.Remote;

public class Server
{
    public long Time { get; private set; }
    public long TimeStampStaticObjects { get; private set; }
    public bool Alive { get; private set; }

    public async void Init(CancellationToken ct = default)
    {
        var cycles = 999;
        while (!ct.IsCancellationRequested)
        {
            var check = await CheckAsync(ct);
            if (check != null)
            {
                Time = check.Time;
                TimeStampStaticObjects = check.TimeStampStaticObjects;
                cycles = 0;
            }
            else
            {
                cycles++;
            }
            Alive = cycles < 2;

            await Task.Delay(1000, ct);
        }
    }

    public static async Task<ServerCheck?> CheckAsync(CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"ServerCheck", ct);
            return !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<ServerCheck>(await answ.Content.ReadAsStringAsync(ct));
        }
        catch
        {
            return null;
        }
    }

}
public class ServerCheck
{
    public long Time { get; set; }
    public long TimeStampStaticObjects { get; set; }
}
