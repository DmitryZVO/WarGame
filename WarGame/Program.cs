using WarGame.Model;

namespace WarGame;

internal static class Program
{
    public static string ConfigName { get; set; } = "_global.ini";

    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            if (args.Length > 0)
            {
                ConfigName = args[0];
            }
        }
        catch
        {
            //
        }

        ApplicationConfiguration.Initialize();

        using var mutex = new Mutex(true, "WAR_GAME", out var createdNew);
        if (!createdNew)
        {
            MessageBox.Show(@"Запущен другой экземпляр программы! Продолженние не возможно....", @"ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Core.Init();
        Application.Run(Core.FrmMain);
        Core.DeInit();
    }
}