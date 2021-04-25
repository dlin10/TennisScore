using TennisScoreApp.ScoreScreen;
using TennisScoreCalculation;

namespace TennisScoreApp.RefereePanel
{
    public class RefereePanelViewModel : NotificationObject
    {
        private string _playerX;
        private string _playerY;
        private bool _started;
        private ScoreCalculator _scoreCalculator;

        public RefereePanelViewModel()
        {
            StartCommand = new DelegateCommand(Start, CanStart);
            WinPointCommand = new DelegateCommand<bool>(WinPoint, () => IsStarted);
        }

        public bool IsStarted
        {
            get => _started;
            set
            {
                _started = value;
                RaisePropertyChanged(nameof(IsStarted), nameof(NotIsStarted));
                StartCommand.RaiseCanExecuteChanged();
                WinPointCommand.RaiseCanExecuteChanged();
            }
        }

        public bool NotIsStarted => !IsStarted;

        public string PlayerX
        {
            get => _playerX;
            set
            {
                _playerX = value;
                StartCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(PlayerX));
            }
        }
        public string PlayerY
        {
            get => _playerY;
            set
            {
                _playerY = value;
                StartCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(nameof(PlayerY));
            }
        }

        public DelegateCommand StartCommand { get; }

        public DelegateCommand<bool> WinPointCommand { get; }

        public void WinPoint(bool isPlayerX)
        {
            _scoreCalculator.Increment(isPlayerX);
        }

        private bool CanStart()
        {
            return !IsStarted && !string.IsNullOrEmpty(PlayerX) && !string.IsNullOrEmpty(PlayerY);
        }

        private void Start()
        {
            IsStarted = true;
            _scoreCalculator = new ScoreCalculator(PlayerX, PlayerY);
            var scoreScreenViewModel = new ScoreScreenViewModel(_scoreCalculator);
            var scoreScreen = new ScoreScreen.ScoreScreen {DataContext = scoreScreenViewModel};
            scoreScreen.Show();
        }
    }
}
