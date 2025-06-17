using UnityEngine;

namespace PlayerPieces
{
    public class King : PlayerPiece
    {
        public override Vector2Int[] ValidMoves { get; } = { new(1, 0), new(0, 1), new(0, -1), new(-1, 0) };
    }
}