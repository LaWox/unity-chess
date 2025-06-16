using UnityEngine;

namespace PlayerPieces
{
    public interface IPlayerPiece
    {
        Vector2Int[] ValidMoves { get; }

        bool MovesAreRepeatable { get; }
        bool IsWhite { get; set; }
    }
}