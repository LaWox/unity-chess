using GameHandler;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{
    public Material validMoveMaterial;
    public Material invalidMoveMaterial;
    private Renderer _renderer;
    private BoardHandler _boardHandler;

    private void Awake()
    {
        _renderer = gameObject.GetComponent<Renderer>();
        _renderer.enabled = false;
        
       
    }
    
    void Start()
    {
        _boardHandler = FindFirstObjectByType<BoardHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnActiveCellUpdate()
    {
        _renderer.material = _boardHandler.IsValidMove(new Vector2Int(), new Vector2Int()) ? validMoveMaterial : invalidMoveMaterial;   
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
