using Constants;
using UnityEngine;

namespace Grid
{
    public class GridHandler : MonoBehaviour
    {
        public GridConfig gridConfig;
        private Vector3[,] _cellPositions;

        private float _cellWidth, _cellHeight;
        private float _invCellWidth, _invCellHeight;
        private float _meshWidth, _meshHeight;
        private float _minLocalBoundsX, _minLocalBoundsZ;

        private int _width, _height;

        private void Awake()
        {
            _width = gridConfig.width;
            _height = gridConfig.height;
            _minLocalBoundsX = gameObject.GetComponent<MeshRenderer>().localBounds.min.x;
            _minLocalBoundsZ = gameObject.GetComponent<MeshRenderer>().localBounds.min.z;

            _meshWidth = gameObject.GetComponent<MeshRenderer>().localBounds.size.x;
            _meshHeight = gameObject.GetComponent<MeshRenderer>().localBounds.size.z;

            _cellWidth = _meshWidth / _width;
            _cellHeight = _meshHeight / _height;
            _invCellWidth = 1 / _cellWidth;
            _invCellHeight = 1 / _cellHeight;

            gameObject.tag = Tags.Board;

            _cellPositions = GetCellPositions();
        }

        public Vector3 GetCellSnappingPoint(Vector3 position)
        {
            var cellIndex = GetCellIndexFromWorldPosition(position);
            var gridPos = _cellPositions[cellIndex.x, cellIndex.y];

            return new Vector3(gridPos.x, position.y, gridPos.z);
        }

        public Vector3 GetWorldPositionFromCellIndex(Vector2Int cellIndex)
        {
            return _cellPositions[cellIndex.x, cellIndex.y];
        }

        public Vector2Int GetCellIndexFromWorldPosition(Vector3 position)
        {
            var localPos = transform.InverseTransformPoint(position);

            var row = Mathf.FloorToInt((localPos.x - _minLocalBoundsX) * _invCellWidth);
            var col = Mathf.FloorToInt((localPos.z - _minLocalBoundsZ) * _invCellHeight);

            return new Vector2Int(row, col);
        }

        private Vector3[,] GetCellPositions()
        {
            var positions = new Vector3[_width, _height];
            for (var i = 0; i < _width; i++)
            for (var j = 0; j < _height; j++)
            {
                var localPos = new Vector3(_cellWidth * i - Mathf.Abs(_minLocalBoundsX + _cellWidth / 2), 0,
                    _cellHeight * j - Mathf.Abs(_minLocalBoundsZ) + _cellHeight / 2);
                var worldPos =
                    transform.TransformPoint(localPos);
                positions[i, j] = worldPos;
            }

            return positions;
        }
    }
}