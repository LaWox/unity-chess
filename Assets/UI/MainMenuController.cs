using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        private VisualElement _ui;
        public Button vsPlayerButton, vsAIButton;


        private void Awake()
        {
            _ui = gameObject.GetComponent<UIDocument>().rootVisualElement;
        }

        private void OnEnable()
        {
            vsPlayerButton = _ui.Q<Button>("vsPlayerButton");
            vsPlayerButton.clicked += OnPlayerButtonClicked;

            vsAIButton = _ui.Q<Button>("vsAIButton");
        }

        private void OnPlayerButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}