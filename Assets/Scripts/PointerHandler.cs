using Constants;
using GameHandler;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerHandler : MonoBehaviour
{
    public delegate void PointerHandlerDelegate();

    public delegate void PointerHandlerDelegateWithVector2(Vector2Int input);

    private Vector2Int _activeCellIndex;

    private Camera _camera;
    private GridHandler _gridHandler;
    private MoveHandler _moveHandler;
    private Vector3 _pointerPosition;
    private Movable _selectedObject;

    private void Start()
    {
        _pointerPosition = new Vector3();
        _camera = Camera.main;
        _selectedObject = null;
        _gridHandler = FindFirstObjectByType<GridHandler>();
        _moveHandler = FindFirstObjectByType<MoveHandler>();
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.isPressed && _selectedObject)
        {
            OnMovableObjectDropped?.Invoke();

            _selectedObject.transform.position = _moveHandler.IsCurrentMoveValid
                ? _gridHandler.GetCellSnappingPoint(_pointerPosition)
                : _gridHandler.GetWorldPositionFromCellIndex(_moveHandler.GetPieceStartPos());

            _selectedObject = null;
        }

        SetPointerPosition();
    }


    public static event PointerHandlerDelegate OnMovableObjectHeld;
    public static event PointerHandlerDelegate OnMovableObjectDropped;
    public static event PointerHandlerDelegateWithVector2 OnActiveCellUpdate;

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
                        OnMovableObjectHeld?.Invoke();
                        _selectedObject = hit.transform.gameObject.GetComponent<Movable>();
                        _selectedObject.SetSelected();
                        _moveHandler.SetPieceStartPos(_gridHandler.GetCellIndexFromWorldPosition(hit.point));
                    }

                    break;
                case Tags.Board:
                    _pointerPosition = hit.point;
                    var activeCell = _gridHandler.GetCellIndexFromWorldPosition(hit.point);
                    if (activeCell != _activeCellIndex && _selectedObject)
                    {
                        _activeCellIndex = activeCell;
                        OnActiveCellUpdate?.Invoke(activeCell);
                    }

                    break;
                default:
                    _pointerPosition = hit.point;
                    OnMovableObjectDropped?.Invoke();
                    break;
            }
    }

    public Vector2Int GetActiveCellIndex()
    {
        return _activeCellIndex;
    }

    public Vector3 GetPointerPosition()
    {
        return _pointerPosition;
    }
}