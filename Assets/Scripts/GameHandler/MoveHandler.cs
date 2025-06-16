using Grid;
using UnityEngine;

namespace GameHandler
{
    public class MoveHandler : MonoBehaviour
    {
        public GameObject moveIndicatorPrefab;
        private MoveIndicator _moveIndicator;
        private Vector2Int _pieceStartPos;
        private BoardHandler _boardHandler;
        private GridHandler _gridHandler;
  
        void Start()
        {
            _moveIndicator = Instantiate(moveIndicatorPrefab, new Vector3(0, 0.01f, 0), Quaternion.identity).GetComponent<MoveIndicator>();
            
            _boardHandler = FindFirstObjectByType<BoardHandler>();
            _gridHandler = FindFirstObjectByType<GridHandler>();
            _pieceStartPos = new Vector2Int();
            PointerHandler.OnActiveCellUpdate += OnActiveCellUpdate;
            PointerHandler.OnMovableObjectDropped += OnPieceDropped;
        }
        
        public void SetPieceStartPos(Vector2Int pos)
        {
            _pieceStartPos = pos;
        }
        
        private void OnActiveCellUpdate(Vector2Int pos)
        {
            _moveIndicator.SetEnabled(true);
            _moveIndicator.SetValid(_boardHandler.IsValidMove(_pieceStartPos, pos));

            var worldPos = _gridHandler.GetWorldPositionFromCellIndex(pos);
            _moveIndicator.transform.position = new Vector3(worldPos.x, 0.01f, worldPos.z);
        }

        private void OnPieceDropped()
        {
            _moveIndicator.SetValid(false);
            _moveIndicator.SetEnabled(false);
        }

        
    }
}
