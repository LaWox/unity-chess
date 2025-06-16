using UnityEngine;

namespace PlayerPieces
{
    public interface IPlayerPiece
    {
        void Initialize(bool isWhite);  
        Vector2Int[] ValidMoves { get; set; }

        bool MovesAreRepeatable { get; }
        bool IsWhite { get; set; }
    }
}