using System.Linq;
using JetBrains.Annotations;
using PlayerPieces;
using UnityEngine;

namespace GameHandler
{
    public class BoardHandler : MonoBehaviour
    {
        public GridConfig gridConfig;
        private MoveHandler _moveHandler;
        private IPlayerPiece[,] _board;

        private void Start()
        {
            _board = new IPlayerPiece[gridConfig.width, gridConfig.height];
            _moveHandler = FindFirstObjectByType<MoveHandler>();
        }

        public void SetCellState(Vector2Int cellIndex, IPlayerPiece piece)
        {
            _board[cellIndex.x, cellIndex.y] = piece;
        }

        [CanBeNull]
        public IPlayerPiece GetCellState(Vector2Int cellIndex)
        {
            return _board[cellIndex.x, cellIndex.y];
        }

        public bool IsValidMove(Vector2Int startIndex, Vector2Int endIndex)
        {
            var piece = GetCellState(startIndex);

            if (piece == null) return false;

            var move = piece.IsWhite ? endIndex - startIndex : startIndex - endIndex;

            var validMoves = piece.ValidMoves;
            
            if (!piece.MovesAreRepeatable) return validMoves.ToList().Contains(move);

            foreach (var validMove in validMoves)
                if (validMove.x % move.x == 0 && validMove.y % move.y == 0 &&
                    validMove.x / move.x == validMove.y / move.y)
                    return true;

            return false;
        }


    }
}