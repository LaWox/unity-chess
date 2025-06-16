using Constants;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerHandler : MonoBehaviour
{
    public delegate void PointerHandlerDelegate();

    private Camera _camera;
    private Vector3 _pointerPosition;
    private Movable _selectedObject;

    private void Start()
    {
        _pointerPosition = new Vector3();
        _camera = Camera.main;
        _selectedObject = null;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.isPressed && _selectedObject)
        {
            _selectedObject = null;
            OnMovableObjectDropped?.Invoke();
        }

        SetPointerPosition();
    }


    public static event PointerHandlerDelegate OnMovableObjectDropped;

    private void SetPointerPosition()
    {
        // Create a ray from the camera through the mouse position
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        // If the ray hits something
        if (Physics.Raycast(ray, out var hit))
            switch (hit.transform.gameObject.tag)
            {
                case Tags.Movable:
                    if (Mouse.current.leftButton.isPressed && !_selectedObject)
                    {
                        _selectedObject = hit.transform.gameObject.GetComponent<Movable>();
                        _selectedObject.SetSelected();
                    }

                    break;
                case Tags.Board:
                    _pointerPosition = hit.point;
                    break;
                default:
                    _pointerPosition = hit.point;
                    OnMovableObjectDropped?.Invoke();
                    break;
            }
    }

    public Vector3 GetPointerPosition()
    {
        return _pointerPosition;
    }
}