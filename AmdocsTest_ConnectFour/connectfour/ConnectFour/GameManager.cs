using System.Collections.Generic;
namespace ConnectFour
{
    public enum CellType
    {
        EMPTY,
        RED,
        BLACK
    }

    public class GameManager
    {
        public const int gridColumns = 7;
        public const int gridRows = 6;
        public const CellType startingPlayer = CellType.RED;

        public CellType[,] gridCells = new CellType[gridColumns, gridRows];
        public List<KeyValuePair<int, int>> placedTokens = new List<KeyValuePair<int, int>>();
        public List<int> fullColumns = new List<int>();
        public KeyValuePair<int, int>[] winningTokens = null;


        public CellType playerTurn;
        public CellType winner = CellType.EMPTY;
        public int[] scores = new int[2];
        public bool gameFinished = false;



        public GameManager()
        {
            playerTurn = startingPlayer;
        }


        public void SetWinnerId(CellType winnerType)
        {
            gameFinished = true;
            winner = winnerType;
            switch (winnerType)
            {
                case CellType.RED:
                    scores[0]++;
                    break;
                case CellType.BLACK:
                    scores[1]++;
                    break;
                case CellType.EMPTY:
                    break;
            }
        }


        public void AddToken(int column) {
            for (int i = gridRows - 1; i >= 0; i--) {
                if (gridCells[column, i] == CellType.EMPTY) {
                    gridCells[column, i] = playerTurn;
                    placedTokens.Add(new KeyValuePair<int, int>(column, i));

                    if (i == 0) {
                        fullColumns.Add(column);
                    }
                    break;
                }
            }

            CheckPlayerWin();

            if (placedTokens.Count == gridRows * gridColumns && !gameFinished) {
                SetWinnerId(CellType.EMPTY);
            } else {
                NextTurn();

            }
        }


        private void NextTurn() {
            switch (playerTurn) {
                case CellType.RED:
                    playerTurn = CellType.BLACK;
                    break;
                case CellType.BLACK:
                    playerTurn = CellType.RED;
                    break;
            }
        }

        private void CheckPlayerWin()
        {
            foreach (var placedToken in placedTokens)
            {
                CellType currentTokenColour = gridCells[placedToken.Key, placedToken.Value];


                // Horizontal Check
                if (placedToken.Key < gridColumns - 3)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (gridCells[placedToken.Key + i, placedToken.Value] == currentTokenColour)
                        {
                            if (i == 3)
                            {
                                SetWinnerId(currentTokenColour);
                                winningTokens = new KeyValuePair<int, int>[4];
                                for (int t = 0; t < 4; t++)
                                {
                                    winningTokens[t] = new KeyValuePair<int, int>(placedToken.Key + t, placedToken.Value);
                                }
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                // Vertical Check
                if (placedToken.Value < gridRows - 3)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (gridCells[placedToken.Key, placedToken.Value + i] == currentTokenColour)
                        {
                            if (i == 3)
                            {
                                SetWinnerId(currentTokenColour);

                                winningTokens = new KeyValuePair<int, int>[4];
                                for (int t = 0; t < 4; t++)
                                {
                                    winningTokens[t] = new KeyValuePair<int, int>(placedToken.Key, placedToken.Value + t);
                                }
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                // Diagonal Right Check
                if (placedToken.Key < gridColumns - 3 && placedToken.Value < gridRows - 3)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (gridCells[placedToken.Key + i, placedToken.Value + i] == currentTokenColour)
                        {
                            if (i == 3)
                            {
                                SetWinnerId(currentTokenColour);
                                winningTokens = new KeyValuePair<int, int>[4];
                                for (int t = 0; t < 4; t++)
                                {
                                    winningTokens[t] = new KeyValuePair<int, int>(placedToken.Key + t, placedToken.Value + t);
                                }                              
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                // Diagonal Left Check
                if (placedToken.Key > 2 && placedToken.Value < gridRows - 3)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        if (gridCells[placedToken.Key - i, placedToken.Value + i] == currentTokenColour)
                        {
                            if (i == 3)
                            {
                                SetWinnerId(currentTokenColour);
                                winningTokens = new KeyValuePair<int, int>[4];
                                for (int t = 0; t < 4; t++)
                                {
                                    winningTokens[t] = new KeyValuePair<int, int>(placedToken.Key - t, placedToken.Value + t);
                                }
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        

        //Resets the game
        public void Reset()
        {
            playerTurn = startingPlayer;
            winner = CellType.EMPTY;
            gridCells = new CellType[gridColumns, gridRows];
            winningTokens = new KeyValuePair<int, int>[4];
            fullColumns = new List<int>();
            placedTokens = new List<KeyValuePair<int, int>>();
            gameFinished = false;
        }

        public void NewGame()
        {
            playerTurn = startingPlayer;
            winner = CellType.EMPTY;
            gridCells = new CellType[gridColumns, gridRows];
            winningTokens = new KeyValuePair<int, int>[4];
            fullColumns = new List<int>();
            placedTokens = new List<KeyValuePair<int, int>>();
            gameFinished = false;


            scores = new int[2];
        }
    }
}