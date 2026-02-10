using SharpDX.DirectInput;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using WarGame.Model;

namespace WarGame.Other;

public class RadiomasterTx12
{
    public bool Alive { get; private set; }
    private DateTime _lastUpdate = DateTime.MinValue; // Время последнего обновления данных
    private readonly CancellationToken _ct = new();
    private readonly DirectInput _dxInput = new(); // Контроллер джойстика
    private Joystick? _joystick; // Джйостик
    private const float StickTolerance = 0.00001f;

    public float[] Channels { get; private set; } = new float[16]; // Данные каналов нормированные от -1.0 до 1.0;

    public async void StartAsync()
    {
        while (!_ct.IsCancellationRequested)
        {
            await Task.Delay(50, _ct); // 20 Гц

            if (_joystick == null || _joystick.IsDisposed || (_joystick.Capabilities.Flags & DeviceFlags.Attached) != DeviceFlags.Attached)
            {
                _joystick?.Dispose();
                var guids = _dxInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AttachedOnly);
                if (guids.Count > 0)
                {
                    _joystick = new Joystick(_dxInput, guids.First().InstanceGuid);
                    _joystick.Properties.BufferSize = 128;
                    _joystick.Acquire();
                }
                else
                {
                    await Task.Delay(100, _ct);
                }
            }

            ReadJoystick(); // Читаем данные с джойстика
            await SendJoystickToServerAsync(); // Отправляем данные на сервер
            Alive = (DateTime.Now - _lastUpdate).TotalMilliseconds < 1000;
        }
    }

    private void ReadJoystick()
    {
        if (_joystick == null || _joystick?.IsDisposed == true || (_joystick?.Capabilities.Flags & DeviceFlags.Attached) != DeviceFlags.Attached)
        {
            ResetButtons();
            return;
        }

        _joystick?.Poll();

        var datas = _joystick?.GetBufferedData();
        if (datas == null) return;

        foreach (var vals in datas)
        {
            var value = NormalizeStickToFloat(vals.Value);
            if (vals.Offset == JoystickOffset.X && Math.Abs(Channels[2] - value) > StickTolerance) // Левый стик Y
            {
                Channels[2] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Y && Math.Abs(Channels[0] - value) > StickTolerance) // Правый стик Х
            {
                Channels[0] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Z && Math.Abs(Channels[1] - value) > StickTolerance) // Правый стик Y
            {
                Channels[1] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.RotationX && Math.Abs(Channels[3] - value) > StickTolerance) // Левый стик X
            {
                Channels[3] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.RotationY && Math.Abs(Channels[4] - value) > StickTolerance) // Левый дальний 3-х позиционный
            {
                Channels[4] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.RotationZ && Math.Abs(Channels[5] - value) > StickTolerance) // Правый дальний 3-х позиционный
            {
                Channels[5] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Sliders1 && Math.Abs(Channels[6] - value) > StickTolerance) // Левый ближний 3-х позиционный
            {
                Channels[6] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Sliders0 && Math.Abs(Channels[7] - value) > StickTolerance) // Правый ближний 3-х позиционный
            {
                Channels[7] = value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Buttons0) // Левая кнопка А
            {
                Channels[8] = vals.Value;
                continue;
            }
            if (vals.Offset == JoystickOffset.Buttons1) // Правая кнопка D
            {
                Channels[9] = vals.Value;
                continue;
            }
            //Console.WriteLine(vals.Offset);
        }
        _lastUpdate = DateTime.Now;
    }

    private void ResetButtons()
    {
        for (var i = 0; i < Channels.Length; i++)
        {
            Channels[i] = 0.0f;
        }
    }

    private async Task<bool> SendJoystickToServerAsync()
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            var ch = new JoyChannels {Channels = Core.Joystick.Channels};
            var jsonString = JsonSerializer.Serialize(ch);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = jsonString.Length;
            using var answ = await web.PostAsync($"SetClientJoyChannels?name={Core.ClientName}", content, _ct);
            return answ.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    private static float NormalizeStickToFloat(int value)
    {
        return (value - 32767f) / 32767f;
    }

    public async void StopAsync()
    {
        _ct.ThrowIfCancellationRequested();
        await Task.Delay(100);
    }
    public class JoyChannels
    {
        public float[] Channels { get; set; } = new float[16];
    }
}
