namespace ConnectFour {
    public class GameUIData {

        public const string emptyCellPath = "/Resources/EmptyCell.png";
        public const string redTokenPath = "/Resources/RedToken.png";
        public const string blackTokenPath = "/Resources/BlackToken.png";
        public const string redTokenWinPath = "/Resources/RedTokenWin.png";
        public const string blackTokenWinPath = "/Resources/BlackTokenWin.png";


        public string[] cellImages = new string[42];
        public string[] addTokenStates = new string[7] { "Visible", "Visible", "Visible", "Visible", "Visible", "Visible", "Visible" };
        public string playerTurnImage = "";


        public GameUIData() {
            Reset();
        }

        public void Reset() {
            cellImages = new string[42];
            addTokenStates = new string[7] { "Visible", "Visible", "Visible", "Visible", "Visible", "Visible", "Visible" };
            playerTurnImage = redTokenPath;
        }

        public int[] GetScores(GameManager gm) {
            return new int[2]
            {
                gm.scores[0], gm.scores[1]
            };
        }

        public bool IsGameFinished(GameManager gm) {
            return gm.gameFinished;
        }
 

        public void DisableColumnFullButton(GameManager gm) {
            foreach (var columnId in gm.fullColumns) {
                addTokenStates[columnId] = "Hidden";
            }
        }

        public string[] DisableAllColumnButtons() {
            return new string[7]
             {
                 "Hidden","Hidden","Hidden","Hidden","Hidden","Hidden","Hidden"
             };
        }

        public void ChangePlayerTurnImage(GameManager gm) {
            switch (gm.playerTurn) {
                case CellType.RED:
                    playerTurnImage = redTokenPath;
                    break;
                case CellType.BLACK:
                    playerTurnImage = blackTokenPath;
                    break;
                default:
                    break;
            }

        }

        public void ChangeCellImages(GameManager gm) {
            int column, row;
            int index = 0;
            for (column = 0; column < 7; column++) {
                for (row = 0; row < 6; row++) {

                    switch (gm.gridCells[column, row]) {
                        case CellType.EMPTY:
                            cellImages[index] = emptyCellPath;
                            break;
                        case CellType.RED:
                            cellImages[index] = redTokenPath;
                            break;
                        case CellType.BLACK:
                            cellImages[index] = blackTokenPath;
                            break;
                    }
                    index++;
                }
            }
        }

        public void ChangeCellWinnerImages(GameManager gm) {
            int column, row;
            int index = 0;
            for (column = 0; column < 7; column++) {
                for (row = 0; row < 6; row++) {
                    bool alreadyAttached = false;

                    for (int inc = 0; inc < 4; inc++) {
                        if (column == gm.winningTokens[inc].Key && row == gm.winningTokens[inc].Value) {
                            switch (gm.gridCells[column, row]) {
                                case CellType.RED:
                                    cellImages[index] = redTokenWinPath;
                                    break;
                                case CellType.BLACK:
                                    cellImages[index] = blackTokenWinPath;
                                    break;
                            }
                            alreadyAttached = true;
                        }
                    }

                    if (!alreadyAttached) {
                        switch (gm.gridCells[column, row]) {
                            case CellType.EMPTY:
                                cellImages[index] = emptyCellPath;
                                break;
                            case CellType.RED:
                                cellImages[index] = redTokenPath;
                                break;
                            case CellType.BLACK:
                                cellImages[index] = blackTokenPath;
                                break;
                        }
                    }
                    index++;
                }
            }
        }

    }
}