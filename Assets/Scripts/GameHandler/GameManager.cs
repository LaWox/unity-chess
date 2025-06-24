using UnityEngine;

namespace GameHandler
{
    public class GameManager : MonoBehaviour
    {
        private bool _isGameOver;

        public bool IsWhitesTurn { get; private set; }

        private void Start()
        {
            IsWhitesTurn = true;

            MoveHandler.OnTurnOver += OnTurnEnded;
            MoveHandler.OnKingCaptured += OnKingCaptured;
        }

        private void OnTurnEnded()
        {
            IsWhitesTurn = !IsWhitesTurn;
        }

        private void OnKingCaptured(bool isWhite)
        {
            Debug.Log($"King captured and is White = {isWhite}");
        }
    }
}