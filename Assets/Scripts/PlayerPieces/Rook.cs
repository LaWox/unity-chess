using UnityEngine;

namespace PlayerPieces
{
    public class Rook : MonoBehaviour, IPlayerPiece
    {
        private readonly Vector2Int[] _validMoves = {new (1,0), new (0, 1), new (0, -1), new (-1, 0)};
        
        public void Initialize(bool isWhite, Vector2Int startPos)
        {
            IsWhite = isWhite;
            StartPos = startPos;
        }

        public Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false)
        {
            return _validMoves;
        }
        
        public bool MovesAreRepeatable => true;
        public bool IsWhite { get; private set; }
        public Vector2Int StartPos { get; private set; }
    }
}