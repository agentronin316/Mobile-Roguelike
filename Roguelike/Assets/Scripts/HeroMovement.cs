using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour
{

    public Collider2D orb;
    float orbSpeed = 20f;

    public float moveSpeed = 2f;
    Animator animator;

    bool Left, Right, Up, Down;
	// Use this for initialization
	void Start ()
    {
        Left = Right = Up = Down = false;
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody2D orbInstance;
            orbInstance = Instantiate(orb, transform.position, Quaternion.identity) as Rigidbody2D;

            if (Right)
            {
                orbInstance.velocity = new Vector2(orbSpeed, 0f);
            }
            if (Left)
            {
                orbInstance.velocity = new Vector2(-orbSpeed, 0f);
            }
            if (Up)
            {
                orbInstance.velocity = new Vector2(0f, orbSpeed);
            }
            if (Down)
            {
                orbInstance.velocity = new Vector2(0f, -orbSpeed);
            }

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
}
