namespace WarGame;

internal static class Program
{
    public static FormMain Fm = new();

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

        Core.Values.Init();
        Application.Run(Fm);
    }
}