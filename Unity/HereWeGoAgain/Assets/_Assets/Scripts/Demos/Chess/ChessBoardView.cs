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
        [SerializeField] private Transform tilesParent;
        [SerializeField] private float speed;
        [SerializeField] private Transform pivot;
        [SerializeField] private float maxScale = 1.4f;
        [SerializeField] private float minScale = .2f;
        private ChessBoard _board;

        public void MovePiece(int fromX, int fromZ, int toX, int toZ)
        {
            //Probably not the best design
            //Since the cell's chess piece can be accessed
            var piece = _board.GetCell(fromX, fromZ).ChessPieceView;
            _board.SetPiece(piece, toX, toZ);
            _board.GetCell(fromX, fromZ).ChessPieceView = null;
        }

        private void Rotate()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                Vector3 directionToOrigin = transform.position - pivot.position;

                transform.position -= directionToOrigin;

                Quaternion rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * speed, Vector3.up);
                transform.rotation = rotation * transform.rotation;

                transform.position += directionToOrigin;
            }
        }

        private void Scale()
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                float scaleFactor = Mathf.Pow(2f, Input.GetAxis("Mouse X") * speed);

                float newX = Mathf.Clamp(transform.localScale.x * scaleFactor, minScale, maxScale);
                float newZ = Mathf.Clamp(transform.localScale.z * scaleFactor, minScale, maxScale);

                transform.localScale = new Vector3(
                    newX,
                    transform.localScale.y,
                    newZ);
            }
        }

        private void Awake()
        {
            _board = new ChessBoard(boardSizeX, boardSizeZ);
            var piece = Instantiate(chessPieceViewPrefab);
            Visualize();
            _board.SetPiece(piece, boardSizeX / 2, boardSizeZ / 2);
        }

        private void Update()
        {
            Rotate();
            Scale();
        }

        private void Visualize()
        {
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int z = 0; z < boardSizeZ; z++)
                {
                    var cellInstance = Instantiate(chessBoardCellPrefab);
                    cellInstance.transform.localScale = new Vector3(cellSize, cellInstance.transform.localScale.y, cellSize);
                    cellInstance.transform.position = new Vector3(x * cellSize, 0, z * cellSize);
                    cellInstance.transform.SetParent(tilesParent, false);
                    _board.SetCell(cellInstance, x, z);
                }
            }

            var offset = new Vector3(-boardSizeX / 2f * cellSize * tilesParent.localScale.x, 0, -boardSizeZ / 2f * cellSize * tilesParent.localScale.z);
            transform.position -= offset;
            tilesParent.transform.position += offset;
        }
    }
}