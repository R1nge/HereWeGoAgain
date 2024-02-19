namespace _Assets.Scripts.Demos.Chess
{
    public class ChessBoard
    {
        public ChessBoardCellView[,] Board { get; set; }

        public ChessBoard(int boardSizeX, int boardSizeZ)
        {
            Board = new ChessBoardCellView[boardSizeX, boardSizeZ];
        }
        
        public ChessBoardCellView GetCell(int x, int z)
        {
            return Board[x, z];
        }

        public void SetCell(ChessBoardCellView chessBoardCellView, int x, int z)
        {
            chessBoardCellView.PositionX = x;
            chessBoardCellView.PositionZ = z;
            Board[x, z] = chessBoardCellView;
        }

        public void SetPiece(ChessPieceView chessPieceView, int x, int z)
        {
            var cell = Board[x, z];
            cell.ChessPieceView = chessPieceView;
            chessPieceView.transform.position = cell.transform.position;
            chessPieceView.transform.parent = cell.transform;
            chessPieceView.PositionX = x;
            chessPieceView.PositionZ = z;
        }
    }
}