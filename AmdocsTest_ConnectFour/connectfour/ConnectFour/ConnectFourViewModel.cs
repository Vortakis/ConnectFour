using ConnectFour;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConnectFour.ViewModel
{
    class ConnectFourViewModel : INotifyPropertyChanged
    {

        public GameManager gameManager = new GameManager();
        public GameUIData gameUIData = new GameUIData();


        private string[] cells = new string[42];
        private int[] scores = new int[2];
        private string[] addTokenVisibility = null;
        private string playerTurn;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddToken1Command { get; set; }
        public ICommand AddToken2Command { get; set; }
        public ICommand AddToken3Command { get; set; }
        public ICommand AddToken4Command { get; set; }
        public ICommand AddToken5Command { get; set; }
        public ICommand AddToken6Command { get; set; }
        public ICommand AddToken7Command { get; set; }

        public ICommand ResetCommand { get; set; }


        public int[] Scores {
            get => scores;
            set {
                scores = value;
                NotifyPropertyChanged("Scores");
            }
        }

        public string[] AddTokenVisibility {
            get => addTokenVisibility;
            set {
                addTokenVisibility = value;
                NotifyPropertyChanged("AddTokenVisibility");
            }
        }

        public string[] Cells {
            get => cells;
            set
            {
                cells = value;
                NotifyPropertyChanged("Cells");
            }
        }     

        public string PlayerTurn {
            get => playerTurn;
            set
            {
                playerTurn = value;
                NotifyPropertyChanged("PlayerTurn");
            }
        }


        public ConnectFourViewModel()
        {
            // Commands - Delegates Binding.
            AddToken1Command = new RelayCommand(Column1Clicked, o => true);
            AddToken2Command = new RelayCommand(Column2Clicked, o => true);
            AddToken3Command = new RelayCommand(Column3Clicked, o => true);
            AddToken4Command = new RelayCommand(Column4Clicked, o => true);
            AddToken5Command = new RelayCommand(Column5Clicked, o => true);
            AddToken6Command = new RelayCommand(Column6Clicked, o => true);
            AddToken7Command = new RelayCommand(Column7Clicked, o => true);
            ResetCommand = new RelayCommand(ResetClicked, o => true);


            Cells = gameUIData.cellImages;
            AddTokenVisibility = gameUIData.addTokenStates;
            PlayerTurn = gameUIData.playerTurnImage;
            Scores = gameUIData.GetScores(gameManager);

            gameManager.NewGame();
            gameUIData.Reset();
            ResetViewModel();
            Scores = new int[2];
        }

        public void ResetClicked(object o)
        {
            gameManager.NewGame();
            gameUIData.Reset();
            ResetViewModel();
            Scores = new int[2];
         
        }

        public void Column1Clicked(object o)
        {
            gameManager.AddToken(0);
            UpdateImages();
        }

        public void Column2Clicked(object o)
        {
            gameManager.AddToken(1);
            UpdateImages();
        }
        public void Column3Clicked(object o)
        {
            gameManager.AddToken(2);
            UpdateImages();
        }
        public void Column4Clicked(object o)
        {
            gameManager.AddToken(3);
            UpdateImages();
        }
        public void Column5Clicked(object o)
        {
            gameManager.AddToken(4);
            UpdateImages();
        }
        public void Column6Clicked(object o)
        {
            gameManager.AddToken(5);
            UpdateImages();
        }
        public void Column7Clicked(object o)
        {
            gameManager.AddToken(6);
            UpdateImages();   
        }

        private async void UpdateImages()
        {
            // Game is Finished!
            if (gameUIData.IsGameFinished(gameManager))
            {
                try
                {
                    if (gameManager.winner != CellType.EMPTY)
                        gameUIData.ChangeCellWinnerImages(gameManager);
                    else
                        gameUIData.ChangeCellImages(gameManager);

                    gameUIData.DisableColumnFullButton(gameManager);
                    gameUIData.ChangePlayerTurnImage(gameManager);

                    Cells = gameUIData.cellImages;
                    AddTokenVisibility = gameUIData.DisableAllColumnButtons();
                    PlayerTurn = gameUIData.playerTurnImage;
                    Scores = gameUIData.GetScores(gameManager);

                    await Task.Delay(500);

                    gameManager.Reset();
                    gameUIData.Reset();
                    ResetViewModel();

                    return;
                }
                catch (System.Exception ex) { }
            }
            // Update visuals and continue
            else
            {
                gameUIData.ChangeCellImages(gameManager);
                gameUIData.DisableColumnFullButton(gameManager);
                gameUIData.ChangePlayerTurnImage(gameManager);
            }

            Cells = gameUIData.cellImages;
            AddTokenVisibility = gameUIData.addTokenStates;
            PlayerTurn = gameUIData.playerTurnImage;
        }

        private void ResetViewModel()
        {
            Cells = gameUIData.cellImages;
            AddTokenVisibility = gameUIData.addTokenStates;
            PlayerTurn = gameUIData.playerTurnImage;
            gameUIData.ChangeCellImages(gameManager);
            gameUIData.DisableColumnFullButton(gameManager);
            gameUIData.ChangePlayerTurnImage(gameManager);
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
