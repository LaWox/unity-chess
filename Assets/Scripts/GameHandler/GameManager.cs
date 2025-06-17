using UnityEngine;

namespace GameHandler
{
    public class GameManager : MonoBehaviour
    {
        private BoardHandler _boardHandler;

        private bool _isGameOver;

        public bool IsWhitesTurn { get; private set; }

        private void Start()
        {
            IsWhitesTurn = true;
            _boardHandler = FindFirstObjectByType<BoardHandler>();

            MoveHandler.OnTurnOver += OnTurnEnded;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnTurnEnded()
        {
            IsWhitesTurn = !IsWhitesTurn;
            _boardHandler.OnPlayerChange(IsWhitesTurn);
        }
    }
}