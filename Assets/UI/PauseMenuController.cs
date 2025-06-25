using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuController : MonoBehaviour
    {
        private Button _resumeButton, _quitToMenuButton;

        private VisualElement _ui;

        private void Awake()
        {
            _ui = gameObject.GetComponent<UIDocument>().rootVisualElement;
            _ui.visible = false;
        }

        private void OnEnable()
        {
            _resumeButton = _ui.Q<Button>("resumeButton");
            _resumeButton.clicked += Resume;

            _quitToMenuButton = _ui.Q<Button>("quitToMenuButton");
            _quitToMenuButton.clicked += QuitToMenu;
        }

        public void ToggleVisibility()
        {
            _ui.visible = !_ui.visible;
        }

        public bool IsVisible()
        {
            return _ui.visible;
        }

        private void Resume()
        {
            _ui.visible = false;
        }

        private void QuitToMenu()
        {
            _ui.visible = false;
        }
    }
}