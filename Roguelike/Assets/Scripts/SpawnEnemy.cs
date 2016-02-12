using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{

    Rigidbody2D enemy;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("EnemySpawn", 3f, 3f);
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void EnemySpawn()
    {
        Rigidbody2D enemyInstance = Instantiate(Resources.Load("Dwarf") as GameObject, new Vector3(Random.Range(2, 8), Random.Range(-4, 4), 0), Quaternion.identity) as Rigidbody2D;
    }
}
