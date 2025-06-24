using Constants;
using GameHandler;
using Grid;
using PlayerPieces;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerHandler : MonoBehaviour
{
    public delegate void PointerHandlerDelegate();

    public delegate void PointerHandlerDelegateWithVector2(Vector2Int input);

    private Vector2Int _activeCellIndex;

    private Camera _camera;
    private GameManager _gameManager;
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
        _gameManager = FindFirstObjectByType<GameManager>();
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
        var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out var hit)) return;

        switch (hit.transform.gameObject.tag)
        {
            case Tags.Movable:
                if (Mouse.current.leftButton.isPressed && !_selectedObject)
                    if (hit.transform.gameObject.TryGetComponent<PlayerPiece>(out var playerPiece))
                        if (_gameManager.IsWhitesTurn == playerPiece.IsWhite)
                            if (hit.transform.gameObject.TryGetComponent<Movable>(out var movable))
                            {
                                _selectedObject = movable;
                                movable.SetSelected();
                                _moveHandler.SetPieceStartPos(
                                    _gridHandler.GetCellIndexFromWorldPosition(hit.point));
                                OnMovableObjectHeld?.Invoke();
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

    public Vector3 GetPointerPosition()
    {
        return _pointerPosition;
    }
}