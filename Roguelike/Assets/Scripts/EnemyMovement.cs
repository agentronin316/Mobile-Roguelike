using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    GameObject hero;
    bool right;
    bool left;
    bool up;
    bool down;
    float enemySpeed;
    Animator enemyAnimator;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Accelerate", 2f, 5f);
        down = left = up = right = false;
        enemyAnimator = GetComponent<Animator>();
        hero = GameObject.Find("Hero");
        enemySpeed = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        EnemyMove();
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Orb(Clone")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void EnemyMove()
    {
        if(hero != null)
        {
            if (hero.transform.position.y < transform.position.y)
            {
                enemyAnimator.SetBool("DwarfDown", true);
                enemyAnimator.SetBool("DwarfUp", false);
                enemyAnimator.SetBool("DwarfLeft", false);
                enemyAnimator.SetBool("DwarfRight", false);

                transform.Translate(Vector3.down * enemySpeed * Time.deltaTime);
            }
            else if (hero.transform.position.y > transform.position.y)
            {
                enemyAnimator.SetBool("DwarfDown", false);
                enemyAnimator.SetBool("DwarfUp", true);
                enemyAnimator.SetBool("DwarfLeft", false);
                enemyAnimator.SetBool("DwarfRight", false);

                transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);
            }

            if (hero.transform.position.x < transform.position.x)
            {
                enemyAnimator.SetBool("DwarfDown", false);
                enemyAnimator.SetBool("DwarfUp", false);
                enemyAnimator.SetBool("DwarfLeft", true);
                enemyAnimator.SetBool("DwarfRight", false);

                transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
            }
            else if (hero.transform.position.x > transform.position.x)
            {
                enemyAnimator.SetBool("DwarfDown", false);
                enemyAnimator.SetBool("DwarfUp", false);
                enemyAnimator.SetBool("DwarfLeft", false);
                enemyAnimator.SetBool("DwarfRight", true);

                transform.Translate(Vector3.right * enemySpeed * Time.deltaTime);
            }
        }
    }

    void Accelerate()
    {
        
    }
}
