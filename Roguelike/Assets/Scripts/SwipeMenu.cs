using UnityEngine;
using System.Collections;


public enum MenuState
{
    MAIN,
    CREDITS,
    SETTINGS,
    DIRECTIONS,
    TRANSITIONING
}
public class SwipeMenu : MonoBehaviour {

    public float swipeMinLength;

    MenuState curMenu = MenuState.MAIN;

	// Update is called once per frame
	void Update ()
    {
	    foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Moved && touch.deltaPosition.magnitude >= swipeMinLength)
            {
                if (touch.deltaPosition.x > touch.deltaPosition.y)
                {
                    //Down or Right
                    if (touch.deltaPosition.x > -touch.deltaPosition.y)
                    {
                        MenuTransitionRight();
                    }
                    else
                    {
                        MenuTransitionDown();
                    }
                }
                else
                {
                    //Up or Left
                    if (touch.deltaPosition.x > -touch.deltaPosition.y)
                    {
                        MenuTransitionUp();
                    }
                    else
                    {
                        MenuTransitionLeft();
                    }
                }
            }
        }
	}

    void MenuTransitionRight()
    {
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Credits
                break;
            case MenuState.SETTINGS:
                //Main
                break;
            case MenuState.CREDITS:
                //Directions
                break;
        }
    }

    void MenuTransitionLeft()
    {
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Settings
                break;
            case MenuState.DIRECTIONS:
                //Credits
                break;
            case MenuState.CREDITS:
                //Main
                break;
        }
    }

    void MenuTransitionUp()
    {
        switch (curMenu)
        {
            case MenuState.MAIN:
                //Directions
                break;
            case MenuState.SETTINGS:
                //Credits
                break;
            case MenuState.DIRECTIONS:
                //Settings
                break;
        }
    }

    void MenuTransitionDown()
    {
        switch (curMenu)
        {
            case MenuState.DIRECTIONS:
                //Main
                break;
            case MenuState.SETTINGS:
                //Directions
                break;
            case MenuState.CREDITS:
                //Settings
                break;
        }
    }


}
