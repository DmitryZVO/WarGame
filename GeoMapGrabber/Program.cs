namespace GeoMapGrabber;

internal static class Program
{
    public static FormMain Fm = new();
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        using var mutex = new Mutex(true, "TULA_CBS_GRABBER", out var createdNew);
        if (!createdNew)
        {
            MessageBox.Show(@"Запущен другой экземпляр программы! Продолженние не возможно....", @"ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(Fm);
    }
}