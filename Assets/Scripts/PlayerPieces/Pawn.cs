using UnityEngine;

namespace PlayerPieces
{
    public class Pawn : MonoBehaviour, IPlayerPiece
    {
        public Pawn(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public Vector2Int[] ValidMoves { get; } = { new(0, 1) };
        public bool MovesAreRepeatable => false;
        public bool IsWhite { get; set; }
    }
}