using Constants;
using Grid;
using UnityEngine;

public class Movable : MonoBehaviour
{
    public PointerHandler pointerHandler;
    public GridHandler gridHandler;
    private Collider _collider;
    private bool _isSelected;
    private Material _material;
    private Color _selectedColor;
    private Color _startColor;

    private void Awake()
    {
        gameObject.tag = Tags.Movable;
        _collider = gameObject.GetComponent<Collider>();
        _material = gameObject.GetComponent<Renderer>().material;
        _startColor = _material.color;
        _selectedColor = new Color(_startColor.r, _startColor.g, _startColor.b, 0.5f);
        PointerHandler.OnMovableObjectDropped += DropObject;
    }
    
    private void Update()
    {
        if (_isSelected) transform.position = gridHandler.GetCellSnappingPoint(pointerHandler.GetPointerPosition());
    }

    private void OnDisable()
    {
        PointerHandler.OnMovableObjectDropped -= DropObject;
    }

    public void SetSelected()
    {
        _material.color = _selectedColor;
        _isSelected = true;
        _collider.enabled = false;
    }


    private void DropObject()
    {
        _material.color = _startColor;
        _collider.enabled = true;
        _isSelected = false;
    }
}