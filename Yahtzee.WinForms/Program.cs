using Yahtzee.Core;
namespace Yahtzee.WinForms;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Yahtzee", "games.db");
        var model = new GameModel();
        var controller = new GameController(model, dbPath);
        var form = new MainForm();
        form.Bind(model, controller);
        Application.Run(form);
    }
}
