using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{

    GameObject orb;
    float orbSpeed = 20f;

    public float moveSpeed = 2f;
    Animator animator;

    bool Left, Right, Up, Down;
	// Use this for initialization
	void Start ()
    {
        Left = Right = Up = Down = false;
        animator = GetComponent<Animator>();
        orb = Resources.Load("Orb") as GameObject;
	}

    void Update()
    {
        bool spawnedOrb = false;
        for (int i = 0; i < Input.touchCount && !spawnedOrb; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                SpawnOrb();
                spawnedOrb = true;
            }
        }

#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnOrb();
        }
#endif
    }
	
    void SpawnOrb()
    {
        Rigidbody2D orbInstance;
        orbInstance = (Instantiate(orb, transform.position, Quaternion.identity) as GameObject).GetComponent<Rigidbody2D>();

        if (Right)
        {
            orbInstance.velocity = new Vector2(orbSpeed, 0f);
        }
        else if (Left)
        {
            orbInstance.velocity = new Vector2(-orbSpeed, 0f);
        }
        else if (Up)
        {
            orbInstance.velocity = new Vector2(0f, orbSpeed);
        }
        else
        {
            orbInstance.velocity = new Vector2(0f, -orbSpeed);
        }
        
    }

	void FixedUpdate ()
    {
        MoveCharacter();
	}

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Left", false);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", true);
            Left = Up = Down = false;
            Right = true;
        }

        if (Right)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Left", false);
            animator.SetBool("Up", true);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            Left = Right = Down = false;
            Up = true;
        }

        if (Up)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Left", false);
            animator.SetBool("Up", false);
            animator.SetBool("Down", true);
            animator.SetBool("Right", false);
            Left = Up = Right = false;
            Down = true;
        }

        if (Down)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Left", true);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Right", false);
            Right = Up = Down = false;
            Left = true;
        }

        if (Left)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I hit something.... probably...");
        if(other.gameObject.name == "Dwarf(clone)")
        {
            Debug.Log("It was an enemy... I are dead");
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }
}
