using System.Windows;
using TennisScoreApp.RefereePanel;

namespace TennisScoreApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RefereePanel.RefereePanel refereePanel = new RefereePanel.RefereePanel {DataContext = new RefereePanelViewModel()};
            Current.MainWindow = refereePanel;
            refereePanel.Show();
        }
    }
}
