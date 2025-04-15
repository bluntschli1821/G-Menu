using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    public float movementF = 10f;
    public float jumpF = 11f;

    private float moveX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string walkAnimation = "Walk";

    private bool isGrounded = true;
    private string Ground_Tag = "Ground";

    /* Awake is called when the script instance is being loaded
     private void awake()
     {

        float v = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrows)
        float h = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)

        Vector2 pos = transform.position; // Get the current position of the player

         pos.x += h * movementF * Time.deltaTime; // Update the x position based on horizontal input
         pos.y += v * movementF * Time.deltaTime; // Update the y position based on vertical input

        transform.position = pos; // Set the new position of the player

     }*/

    // Start is called before the first frame update
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();

    }

    void PlayerMoveKeyboard()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(moveX, 0f, 0f) * movementF * Time.deltaTime;
    }

    void AnimatePlayer()
    {
        //    Moving Forward
        if (moveX > 0)
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = false;
        }  // Movong Backwards
        else if (moveX < 0)
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = true;
        } // Standing Idle
        else
        {
            anim.SetBool(walkAnimation, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpF), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Ground_Tag)){
            isGrounded = true;
        }
    }
}
