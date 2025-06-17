using UnityEngine;

namespace PlayerPieces
{
    public class Knight : MonoBehaviour, IPlayerPiece
    {
        private readonly Vector2Int[] _validMoves =
            { new(1, 2), new(2, 1), new(1, -2), new(-2, 1), new(-1, -2), new(-2, -1), new(-1, 2), new(2, -1) };

        public void Initialize(bool isWhite, Vector2Int startPos)
        {
            IsWhite = isWhite;
            StartPos = startPos;
        }

        public Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false)
        {
            return _validMoves;
        }

        public bool MovesAreRepeatable => false;
        public bool IsWhite { get; private set; }
        public Vector2Int StartPos { get; private set; }
    }
}