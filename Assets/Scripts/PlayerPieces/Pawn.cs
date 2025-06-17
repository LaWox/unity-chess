using UnityEngine;

namespace PlayerPieces
{
    public class Pawn : PlayerPiece
    {
        private readonly Vector2Int[] _captureMoves = { new(1, 1), new(-1, 1) };
        private readonly Vector2Int[] _firstMoveMoves = { new(0, 2), new(0, 1) };
        private readonly Vector2Int[] _validMoves = { new(0, 1) };

        public override Vector2Int[] GetValidMoves(bool isCapture = false, bool isFirstMove = false)
        {
            if (isFirstMove) return _firstMoveMoves;
            return isCapture ? _captureMoves : _validMoves;
        }
    }
}