using System.ComponentModel;
using System.Text.Json;
using WarGame.Core;

namespace WarGame.Remote;

public class Server
{
    public string Url { get; set; } = "http://212.12.7.116:1111"; // Тестовый сервер офис
    public DateTime Time { get; private set; } = DateTime.MinValue;
    public bool Alive { get; private set; }

    public async void Init(CancellationToken ct = default)
    {
        var cycles = 999;
        while (!ct.IsCancellationRequested)
        {
            var check = await CheckAsync();
            if (check != null)
            {
                Time = new DateTime(check.Time);
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

    public async Task<ServerCheck?> CheckAsync(CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Url);
            using var answ = await web.GetAsync($"ServerCheck", ct);
            return !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<ServerCheck>(await answ.Content.ReadAsStringAsync(ct));
        }
        catch
        {
            //
        }
        return null;
    }

}
public class ServerCheck
{
    public long Time { get; set; }
}
