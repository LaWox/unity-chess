using UnityEngine;

namespace PlayerPieces
{
    public class Pawn : MonoBehaviour, IPlayerPiece
    {
        public void Initialize(bool isWhite)
        {
            IsWhite = isWhite;
            ValidMoves = new[] {new Vector2Int(0, 1)};
        }
        public Vector2Int[] ValidMoves { get; set; }
        public bool MovesAreRepeatable => false;
        public bool IsWhite { get; set; }
    }
}