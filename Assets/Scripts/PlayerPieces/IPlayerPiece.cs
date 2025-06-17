using UnityEngine;

namespace PlayerPieces
{
    public interface IPlayerPiece
    {
        void Initialize(bool isWhite, Vector2Int startPos);
        public Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false);
        Vector2Int StartPos { get; }
        bool MovesAreRepeatable { get; }
        bool IsWhite { get; }
        
    }
}