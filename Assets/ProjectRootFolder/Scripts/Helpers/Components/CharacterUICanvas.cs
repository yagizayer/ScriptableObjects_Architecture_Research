using UnityEngine;

// constantly looks at the camera (with correct orientation)
[RequireComponent(typeof(Canvas))]
public class CharacterUICanvas : MonoBehaviour
{
    private Vector3 _cameraLookDir;
    private Canvas _myCanvas;

    private void Awake()
    {
        _myCanvas = GetComponent<Canvas>();
        _myCanvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        _cameraLookDir = _myCanvas.worldCamera.transform.forward;
        transform.forward = _cameraLookDir;
    }
}
