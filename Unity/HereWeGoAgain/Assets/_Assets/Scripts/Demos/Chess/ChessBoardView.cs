using System;
using UnityEngine;

namespace _Assets.Scripts.Demos.Chess
{
    public class ChessBoardView : MonoBehaviour
    {
        [SerializeField] private float cellSize;
        [SerializeField] private int boardSizeX, boardSizeZ;
        [SerializeField] private ChessPieceView chessPieceViewPrefab;
        [SerializeField] private ChessBoardCellView chessBoardCellPrefab;
        private ChessBoard _board;
        
        public void MovePiece(int fromX, int fromZ, int toX, int toZ)
        {
            var piece = _board.GetCell(fromX, fromZ).ChessPieceView;
            _board.SetPiece(piece, toX, toZ);
        }

        private void Awake()
        {
            _board = new ChessBoard(boardSizeX, boardSizeZ);
            var piece = Instantiate(chessPieceViewPrefab);
            Visualize();
            _board.SetPiece(piece, boardSizeX / 2, boardSizeZ / 2);
        }

        private void Visualize()
        {
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int z = 0; z < boardSizeZ; z++)
                {
                    var cellInstance = Instantiate(chessBoardCellPrefab);
                    cellInstance.transform.localScale = Vector3.one * cellSize;
                    cellInstance.transform.position = new Vector3(x, 0, z);
                    cellInstance.transform.SetParent(transform, false);
                    _board.SetCell(cellInstance, x, z);
                }
            }
        }
    }
}