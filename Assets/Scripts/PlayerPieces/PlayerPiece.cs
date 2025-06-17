using UnityEngine;

namespace PlayerPieces
{
    public abstract class PlayerPiece : MonoBehaviour
    {
        public Vector2Int StartPos { get; private set; }
        public virtual bool MovesAreRepeatable { get; private set; }
        public virtual Vector2Int[] ValidMoves { get; }
        public bool IsWhite { get; set; }

        public void Initialize(bool isWhite, Vector2Int startPos, bool movesAreRepeatable)
        {
            MovesAreRepeatable = movesAreRepeatable;
            StartPos = startPos;
            IsWhite = isWhite;
        }

        public virtual Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false)
        {
            return ValidMoves;
        }
    }
}