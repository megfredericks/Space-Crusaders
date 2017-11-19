using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerCombat : MonoBehaviour
{
	public GameObject player;
    public float cameraSpeed;

    private Camera _mainCamera;
    private Vector3 _targetLocation = new Vector3();
    private Vector3 _offset = new Vector3();

	void Start ()
    {
        _mainCamera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () 
	{
		if (player == null)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        mousePosition = MouseToXZPlane(mousePosition);
        _targetLocation.x = (player.transform.position.x + mousePosition.x)/ 2.0f;
        _targetLocation.y = this.transform.position.y;
        _targetLocation.z = (player.transform.position.z + mousePosition.z) / 2.0f;
        Vector3 difference = _targetLocation-this.transform.position;
        difference.x = difference.x * .5f;
        difference.z = difference.z * .5f;
        _offset = Vector3.MoveTowards(_offset, difference, cameraSpeed * Time.deltaTime);
        this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z) + _offset;

        // zoom with mousewheel
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        float cameraSize = _mainCamera.fieldOfView;
        
        if (mouseWheel > 0 && cameraSize > 60)
        {
            _mainCamera.fieldOfView -= mouseWheel * 20;
        }
        else if (mouseWheel < 0 && cameraSize < 116)
        {
            _mainCamera.fieldOfView -= mouseWheel * 20;
        }
        // click mousewheel button to return to original field of view
        if (Input.GetMouseButtonDown(2))
        {
            _mainCamera.fieldOfView = 100;
        }
    }

    private Vector3 MouseToXZPlane(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.point;
    }

    private void FixedUpdate()
    {
        // zoom with mousewheel
        //float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        //float cameraSize = mainCamera.fieldOfView;
        //if(mouseWheel > 0 && cameraSize > 25)
        //{
        //    mainCamera.fieldOfView -= mouseWheel * 20;
        //}
        //else if (mouseWheel < 0 && cameraSize < 80)
        //{
        //    mainCamera.fieldOfView -= mouseWheel * 20;
        //}
        //// click mousewheel button to return to original field of view
        //if (Input.GetMouseButtonDown(2))
        //{
        //    mainCamera.fieldOfView = 60;
        //}
    }
}
