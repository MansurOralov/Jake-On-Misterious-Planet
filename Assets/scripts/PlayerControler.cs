using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControler : MonoBehaviour
{

    public static PlayerControler instance;
    public float jumpForce = 6f;
    private Rigidbody2D rigidBody;
    public Animator animator;
    public float runningSpeed = 1.5f;
    public Vector3 startingPosition;




    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        instance = this;
        startingPosition = this.transform.position;
    }


    // Start is called before the first frame update
  public  void Start()
    {
        animator.SetBool("isAlive", true);
        this.transform.position = startingPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
                Debug.Log("loh");

            }


            animator.SetBool("isGrounded", IsGrounded());
        }
    }


    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public LayerMask groundLayer;

    bool IsGrounded()
    {

        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.3f, groundLayer.value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            }

        }
    }


    public void Kill()
    {
        GameManager.instance.GameOver();
        animator.SetBool("isAlive", false);
    }
} 
