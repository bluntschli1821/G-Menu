using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float movementF = 10f,
        jumpF = 11f,
        minX,
        maxX;

    // This is not serialized hence even being a floating point number I have kept it on it's own
    float moveX;

    Transform player;
    Vector3 tempPos;

    Rigidbody2D myBody;

    SpriteRenderer sr;

    Animator anim;
    string walkAnimation = "Walk";

    bool isGrounded = true;
    string Ground_Tag = "Ground";

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        /*   float v = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrows)
           float h = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
   
           Vector2 pos = transform.position; // Get the current position of the player
   
            pos.x += h * movementF * Time.deltaTime; // Update the x position based on horizontal input
            pos.y += v * movementF * Time.deltaTime; // Update the y position based on vertical input
   
           transform.position = pos; // Set the new position of the player
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();

        // Setting player limitations with regards to movement in scene
        tempPos = transform.position;
        tempPos.x = player.position.x;

        if (tempPos.x < minX)
            tempPos.x = minX;

        if (tempPos.x > maxX)
            tempPos.x = maxX;

        transform.position = tempPos;
    }

    // This delays the jump time of the in-game player
    /* void FixedUpdate()
     {
         PlayerJump();
     }
    */

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
        } // Movong Backwards
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Ground_Tag))
        {
            isGrounded = true;
        }
    }
}
