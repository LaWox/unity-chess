using UnityEngine;

namespace PlayerPieces
{
    public class Pawn : MonoBehaviour, IPlayerPiece
    {
        public Pawn(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public Vector2Int[] ValidMoves { get; set; }
        public bool MovesAreRepeatable => false;
        public bool IsWhite { get; set; }
    }
}