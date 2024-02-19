using UnityEngine;

namespace _Assets.Scripts.Demos.Chess
{
    public class ChessBoardCellView : MonoBehaviour
    {
        public ChessPieceView ChessPieceView { get; set; }
        public int PositionX { get; set; }
        public int PositionZ { get; set; }
    }
}