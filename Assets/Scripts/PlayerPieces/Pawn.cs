using UnityEngine;

namespace PlayerPieces
{
    public class Pawn : MonoBehaviour, IPlayerPiece
    {
        private readonly Vector2Int[] _captureMoves = {new (1, 1), new (-1, 1)};
        private readonly Vector2Int[] _firstMoveMoves = {new (0, 2), new (0, 1)};
        private readonly Vector2Int[] _validMoves = {new (0, 1)};
        
        public void Initialize(bool isWhite, Vector2Int startPos)
        {
            IsWhite = isWhite;
            StartPos = startPos;
        }

        public Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false)
        {
            if (isFirstMove) return _firstMoveMoves;
            return isCapture ?  _captureMoves : _validMoves;
        }
        
        public bool MovesAreRepeatable => false;
        public bool IsWhite { get; set; }
        public Vector2Int StartPos { get; set; }
    }
}