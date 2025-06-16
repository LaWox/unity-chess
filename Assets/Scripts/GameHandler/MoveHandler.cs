using Grid;
using UnityEngine;

namespace GameHandler
{
    public class MoveHandler : MonoBehaviour
    {
        public GameObject moveIndicatorPrefab;
        private MoveIndicator _moveIndicator;
        private Vector2Int _pieceStartPos;
        private Vector2Int _activeCellIndex;
        private BoardHandler _boardHandler;
        private GridHandler _gridHandler;
        private bool _currentMoveValid;

        private void Awake()
        {
            _pieceStartPos = new Vector2Int();
            _currentMoveValid = false;
            _moveIndicator = Instantiate(moveIndicatorPrefab, new Vector3(0, 0.01f, 0), Quaternion.identity).GetComponent<MoveIndicator>();
            PointerHandler.OnActiveCellUpdate += OnActiveCellUpdate;
            PointerHandler.OnMovableObjectDropped += OnPieceDropped;
        }

        private void Start()
        {
            _boardHandler = FindFirstObjectByType<BoardHandler>();
            _gridHandler = FindFirstObjectByType<GridHandler>();
        }

        public bool IsCurrentMoveValid()
        {
            return _currentMoveValid;
        }
        
        public void SetPieceStartPos(Vector2Int pos)
        {
            _pieceStartPos = pos;
        }
        
        private void OnActiveCellUpdate(Vector2Int pos)
        {
            var isValid = _boardHandler.IsValidMove(_pieceStartPos, pos);
            _activeCellIndex = pos;
            _moveIndicator.SetEnabled(true);
            _moveIndicator.SetValid(isValid);
            _currentMoveValid = isValid;

            var worldPos = _gridHandler.GetWorldPositionFromCellIndex(pos);
            _moveIndicator.transform.position = new Vector3(worldPos.x, 0.01f, worldPos.z);
        }

        private void OnPieceDropped()
        {
            _moveIndicator.SetValid(false);
            _moveIndicator.SetEnabled(false);

            if (!_currentMoveValid) return;
            
            var activePiece = _boardHandler.GetCellState(_pieceStartPos);
                
            _boardHandler.SetCellState(_pieceStartPos, null);
            _boardHandler.SetCellState(_activeCellIndex, activePiece);
        }

        public Vector2Int GetPieceStartPos()
        {
            return _pieceStartPos;
        }

        
    }
}
