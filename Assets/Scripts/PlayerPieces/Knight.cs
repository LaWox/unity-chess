using UnityEngine;

namespace PlayerPieces
{
    public class Knight : PlayerPiece
    {
        public override Vector2Int[] ValidMoves { get; } =
            { new(1, 2), new(2, 1), new(1, -2), new(-2, 1), new(-1, -2), new(-2, -1), new(-1, 2), new(2, -1) };
    }
}