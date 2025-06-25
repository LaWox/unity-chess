using GameHandler;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public UIDocument uiDoc;
        private GameManager _gameManager;
        private VisualElement _ui;
        private Button _vsPlayerButton, _vsAIButton, _quitButton;


        private void Awake()
        {
            uiDoc = gameObject.GetComponent<UIDocument>();
            _ui = uiDoc.rootVisualElement;
        }

        private void Start()
        {
            _gameManager = FindFirstObjectByType<GameManager>();
        }

        private void OnEnable()
        {
            _vsPlayerButton = _ui.Q<Button>("vsPlayerButton");
            _vsPlayerButton.clicked += OnPlayerButtonClicked;

            _vsAIButton = _ui.Q<Button>("vsAIButton");
            _vsAIButton.clicked += OnPlayerButtonClicked;

            _quitButton = _ui.Q<Button>("quitButton");
            _quitButton.clicked += GameManager.QuitGame;
        }

        private void OnPlayerButtonClicked()
        {
            _gameManager.StartGame();
            gameObject.SetActive(false);
        }
    }
}