using UnityEngine;
using System.Collections;

public class TwoFingerGestures : MonoBehaviour {

    public GameObject toRotate;
    public float zoomSensitivity;
	// Update is called once per frame
	void Update ()
    {
	    if(Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Vector2 thisFrame = Input.GetTouch(0).position - Input.GetTouch(1).position;
            Vector2 lastFrame = thisFrame + Input.GetTouch(1).deltaPosition - Input.GetTouch(0).deltaPosition;

            float touchDelta = thisFrame.magnitude - lastFrame.magnitude;
            if (touchDelta > zoomSensitivity)
            {
                Debug.Log("zoom in");
                Camera.main.fieldOfView -= touchDelta;
            }
            else if (touchDelta < -zoomSensitivity)
            {
                Debug.Log("zoom out");
                Camera.main.fieldOfView -= touchDelta;

            }


        }
	}
}
