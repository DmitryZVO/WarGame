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
            MessageBox.Show(@"������� ������ ��������� ���������! ������������ �� ��������....", @"������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }


        Core.Init();
        Application.Run(Core.FrmMap!);
        Core.DeInit();
    }
}