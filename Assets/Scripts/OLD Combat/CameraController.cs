using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera controller for combat. Attach this script to the camera in the scene.
public class CameraController : MonoBehaviour
{
    public AnimationCurve zoomCurve;
    public float maxCameraScrollSpeed;

    // Percentage of the screen to be used for scrolling
    public float edgeScrollThresholdPercentage;

    private float _edgeWidth;
    private Vector3 _cameraPosition = new Vector3();

    private void Awake()
    {
        // Don't let the cursor leave the screen (Note: this only works if the game is full screen)
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Use this for initialization
    void Start ()
    {
        _edgeWidth = Screen.width * edgeScrollThresholdPercentage;
        _cameraPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = Input.mousePosition;

        // Clamp the mouse positions for calculation to be within the screen bounds
        mousePos.x = Mathf.Clamp(mousePos.x, 0f, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0f, Screen.height);
        _cameraPosition = this.transform.position;

        // Scroll left
        if(mousePos.x < _edgeWidth)
        {
            _cameraPosition.x -= ((_edgeWidth-mousePos.x)/_edgeWidth) * maxCameraScrollSpeed;
        }
        // Scroll right
        else if(mousePos.x > Screen.width-_edgeWidth)
        {
            _cameraPosition.x += ((_edgeWidth-(Screen.width-mousePos.x))/_edgeWidth) * maxCameraScrollSpeed;
        }

        // Scroll down
        if(mousePos.y < _edgeWidth)
        {
            _cameraPosition.z -= ((_edgeWidth-mousePos.y)/_edgeWidth) * maxCameraScrollSpeed;
        }
        // Scroll up
        else if(mousePos.y > Screen.height-_edgeWidth)
        {
            _cameraPosition.z += ((_edgeWidth-(Screen.height-mousePos.y))/_edgeWidth) * maxCameraScrollSpeed;
        }

        this.transform.position = _cameraPosition;
	}
}
