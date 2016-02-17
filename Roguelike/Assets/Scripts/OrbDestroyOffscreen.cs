using UnityEngine;
using System.Collections;

public class OrbDestroyOffscreen : MonoBehaviour {

    float countdown = 3f;
	// Update is called once per frame
	void Update ()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            Destroy(gameObject);
        }
	}
}
