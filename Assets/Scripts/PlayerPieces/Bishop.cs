using UnityEngine;

namespace PlayerPieces
{
    public class Bishop : PlayerPiece
    {
        public override Vector2Int[] ValidMoves { get; } = { new(1, 1), new(-1, -1), new(1, -1), new(-1, 1) };
    }
}