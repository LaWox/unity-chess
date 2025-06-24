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
        }

        private void OnTurnEnded()
        {
            IsWhitesTurn = !IsWhitesTurn;
        }
    }
}