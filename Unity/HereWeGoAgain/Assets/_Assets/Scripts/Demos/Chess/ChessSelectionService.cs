using System;
using UnityEngine;

namespace _Assets.Scripts.Demos.Chess
{
    public class ChessSelectionService : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private ChessBoardView chessBoardView;
        [SerializeField] private LayerMask pieceLayer;
        private ChessPieceView _selectedPieceView;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, float.MaxValue, layerMask: ~pieceLayer))
                {
                    if (hit.transform.TryGetComponent(out ChessBoardCellView cell))
                    {
                        if (_selectedPieceView == null)
                        {
                            Debug.Log("Select");
                            _selectedPieceView = cell.ChessPieceView;
                        }
                        else
                        {
                            Debug.Log("Move");
                            var positionX = cell.PositionX;
                            var positionZ = cell.PositionZ;
                            chessBoardView.MovePiece(_selectedPieceView.PositionX, _selectedPieceView.PositionZ, positionX, positionZ);
                            _selectedPieceView = null;
                        }
                    }
                }
            }
        }
    }
}