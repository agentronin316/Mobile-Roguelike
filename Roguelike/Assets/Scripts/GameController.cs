using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    Text scoreBox;
    float score = 0f;

    // Use this for initialization
	void Start ()
    {
        scoreBox = GameObject.Find("Score Text Goes Here").GetComponent<Text>();
        InvokeRepeating("AddScore", 0.5f, 0.5f);
	}
	
	void AddScore ()
    {
        score += .5f;
        scoreBox.text = System.Math.Round(score, 1).ToString();
	}
}
