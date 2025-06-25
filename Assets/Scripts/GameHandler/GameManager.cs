using PlayerPieces;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameHandler
{
    public class GameManager : MonoBehaviour
    {
        public PieceGenerator pieceGenerator;
        public PauseMenuController pauseMenuController;
        public PointerHandler pointerHandler;
        private bool _isGameOver;

        public bool IsWhitesTurn { get; private set; }

        private void Start()
        {
            IsWhitesTurn = true;
            pointerHandler.enabled = false;

            MoveHandler.OnTurnOver += OnTurnEnded;
            MoveHandler.OnKingCaptured += OnKingCaptured;
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                pauseMenuController.ToggleVisibility();
                pointerHandler.enabled = !pauseMenuController.IsVisible();
            }
        }

        private void OnTurnEnded()
        {
            IsWhitesTurn = !IsWhitesTurn;
        }

        public void StartGame()
        {
            pointerHandler.enabled = true;
            pieceGenerator.GeneratePieces();
        }

        private void OnKingCaptured(bool isWhite)
        {
            Debug.Log($"King captured and is White = {isWhite}");
        }

        public static void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
                                Application.Quit();
#endif
        }
    }
}