using WarGame.Model;

namespace WarGame;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var mutex = new Mutex(true, "WAR_GAME", out var createdNew);
        if (!createdNew)
        {
            MessageBox.Show(@"Запущен другой экземпляр программы! Продолженние не возможно....", @"ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }


        Core.Init();
        Application.Run(Core.FrmMap!);
        Core.DeInit();
    }
}