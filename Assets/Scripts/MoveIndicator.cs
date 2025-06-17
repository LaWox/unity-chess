using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    public Material validMoveMaterial;
    public Material invalidMoveMaterial;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.enabled = false;
    }

    public void SetValid(bool isValid)
    {
        _renderer.material = isValid ? validMoveMaterial : invalidMoveMaterial;
    }

    public void SetEnabled(bool isEnabled)
    {
        _renderer.enabled = isEnabled;
    }
}