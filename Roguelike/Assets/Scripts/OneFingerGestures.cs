using UnityEngine;
using System.Collections;

public class OneFingerGestures : MonoBehaviour {

    public float swipeMinLength = 3f;

    GameObject movingObject;
    
    Vector2 start;
    Vector2 end;
    Vector2 swipe;

    // Update is called once per frame
    void Update ()
    {
	    if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                start = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                end = Input.GetTouch(0).position;
                swipe = end - start;
                if (swipe.magnitude >= swipeMinLength)
                {
                    Debug.Log("Swipe Detected");
                    if (swipe.x > swipe.y)
                    {
                        //Down or Right
                        if (swipe.x > -(swipe.y))
                        {
                            Debug.Log("Swipe Right");
                        }
                        else
                        {
                            Debug.Log("Swipe Down");
                        }
                    }
                    else
                    {
                        //Up or Left
                        if (swipe.x > -(swipe.y))
                        {
                            Debug.Log("Swipe Up");
                        }
                        else
                        {
                            Debug.Log("Swipe Left");
                        }
                    }
                }
                else if (Input.GetTouch(0).tapCount == 1)
                {
                    //Debug.Log("Single Tap");
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit, 10f))
                    {
                        movingObject = hit.collider.gameObject;
                    }
                }
                else if (Input.GetTouch(0).tapCount == 2)
                {
                    Debug.Log("Double Tap");
                    if (movingObject != null)
                    {
                        Vector3 pos = new Vector3();
                        pos.x = Input.GetTouch(0).position.x;
                        pos.y = Input.GetTouch(0).position.y;
                        pos.z = Mathf.Abs(Camera.main.transform.position.z - movingObject.transform.position.z);

                        movingObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
                    }
                }
            }
        }
	}
}
