using UnityEngine;
using System.Collections;

public class CameraScreen : MonoBehaviour {

    public float perspectiveZoomSpeed = 0.5f;
    public float orthographicZoomSpeed = 0.5f;

    float hMovement = 0f;
    float vMovement = 0f;
    float cameraMoveSpeed = .1f;
    float cameraMove = 01f;
    bool isOrthographic;
    Camera cam;
    // Use this for initialization
    void Start ()
    {
        cam = GetComponent<Camera>();
        isOrthographic = cam.orthographic;
        if (isOrthographic)
        {
            cameraMoveSpeed = cameraMove * GetComponent<Camera>().orthographicSize;
        }
        else
        {
            cameraMoveSpeed = cameraMove * GetComponent<Camera>().fieldOfView;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(hMovement, vMovement, 0);

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(0);
            Vector2 touchZeroPreviousPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPos = touchOne.position - touchOne.deltaPosition;
            Vector2 lastFrameVector = touchOnePreviousPos - touchZeroPreviousPos;
            Vector2 curFrameVector = touchOne.position - touchZero.position;
            float deltaMag = lastFrameVector.magnitude - curFrameVector.magnitude;


            if (isOrthographic)
            {
                cam.orthographicSize += deltaMag * cameraMoveSpeed;
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, .1f);
                cameraMoveSpeed = cameraMove * cam.orthographicSize;
            }
            else
            {
                cam.fieldOfView += deltaMag * cameraMoveSpeed;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, .1f, 179.9f);
                cameraMoveSpeed = cameraMove * cam.fieldOfView;
            }
        }

        //Move the camera with accelerometer
        float tempX = (float)System.Math.Round(Input.acceleration.x, 2);
        float tempY = (float)System.Math.Round(Input.acceleration.y, 2);

        if(tempX > .02f || tempX < -.02 || tempY > .02f || tempY < -.02)
        {
            transform.Translate(new Vector3(tempX * cameraMoveSpeed, tempY * cameraMoveSpeed));
            Debug.Log("Moving: hMove: " + (tempX * cameraMoveSpeed) + " vMove: " + (tempY * cameraMoveSpeed));
        }
        else
        {
            Debug.Log("Not Moving: hMove: " + (tempX * cameraMoveSpeed) + " vMove: " + (tempY * cameraMoveSpeed));
        }
    }
}
