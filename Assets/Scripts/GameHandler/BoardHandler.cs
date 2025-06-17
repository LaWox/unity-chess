using System.Linq;
using JetBrains.Annotations;
using PlayerPieces;
using UnityEngine;

namespace GameHandler
{
    public class BoardHandler : MonoBehaviour
    {
        public GridConfig gridConfig;
        private PlayerPiece[,] _board;
        private MoveHandler _moveHandler;

        private void Start()
        {
            _board = new PlayerPiece[gridConfig.width, gridConfig.height];
            _moveHandler = FindFirstObjectByType<MoveHandler>();
        }

        public void SetCellState(Vector2Int cellIndex, PlayerPiece piece)
        {
            _board[cellIndex.x, cellIndex.y] = piece;
        }

        [CanBeNull]
        public PlayerPiece GetCellState(Vector2Int cellIndex)
        {
            return _board[cellIndex.x, cellIndex.y];
        }

        public bool IsValidMove(Vector2Int startIndex, Vector2Int endIndex)
        {
            var piece = GetCellState(startIndex);
            var endPiece = GetCellState(endIndex);

            if (piece == null) return false;
            if (endPiece != null && piece.IsWhite == endPiece.IsWhite) return false;

            var move = piece.IsWhite ? endIndex - startIndex : startIndex - endIndex;
            if (move is { x: 0, y: 0 }) return false;

            var isCapture = endPiece != null && piece.IsWhite != endPiece.IsWhite;

            var validMoves = piece.GetValidMoves(isFirstMove: startIndex == piece.StartPos, isCapture: isCapture);

            if (!piece.MovesAreRepeatable) return validMoves.ToList().Contains(move);

            if (Mathf.Abs(move.x) == Mathf.Abs(move.y)) move /= Mathf.Abs(move.x);

            if (move.x == 0 || move.y == 0) move = move / Mathf.FloorToInt(move.magnitude);

            return validMoves.ToList().Contains(move);
        }
    }
}