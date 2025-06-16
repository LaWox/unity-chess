using Constants;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    public GridConfig gridConfig;
    private Vector3[,] _cellPositions;

    private float _cellWidth, _cellHeight;
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

    private Vector2Int GetCellIndexFromWorldPosition(Vector3 position)
    {
        var localPos = transform.InverseTransformPoint(position);
        var relativeX = localPos.x - _minLocalBoundsX;
        var relativeY = localPos.z - _minLocalBoundsZ;

        var row = Mathf.FloorToInt(relativeX / _cellWidth);
        var col = Mathf.FloorToInt(relativeY / _cellHeight);

        return new Vector2Int(row, col);
    }

    private Vector3[,] GetCellPositions()
    {
        var positions = new Vector3[_width, _height];
        for (var i = 0; i < _width; i++)
        for (var j = 0; j < _height; j++)
        {
            // positions[i, j] = transform.TransformPoint(new Vector2(_cellWidth * i, _cellHeight * j));
            var localPos = new Vector3(_cellWidth * i - Mathf.Abs(_minLocalBoundsX + _cellWidth / 2), 0,
                _cellHeight * j - Mathf.Abs(_minLocalBoundsZ) + _cellHeight / 2);
            var worldPos =
                transform.TransformPoint(localPos);
            positions[i, j] = worldPos;
        }

        return positions;
    }
}