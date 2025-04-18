using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Analytics;
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

    //  This is used when limiting the player from leaving the scene
    Transform player;
    Vector3 tempPos;

    // This is a Rigidbody2D component as the name implies
    Rigidbody2D myBody;

    // This is a SpriteRenderer component as the name implies
    SpriteRenderer sr;

    // This is an Animator component ...........
    Animator anim;

    /* In the walk animation component,
    the walkAnimation string is set to the Walk.anim set earlier in unity engine. */
    string walkAnimation = "Walk";

    // This boolean is used to check if the player object is grounded before granting another jump instruction
    bool isGrounded = true;
    string Ground_Tag = "Ground";
    private string Enemy_Tag = "Enemies";

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
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

    /*
    // This delays the jump time of the in-game player
    void FixedUpdate()
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
        if (moveX > 0) // Moving Forward
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = false;
        }
        else if (moveX < 0) // Movong Backwards
        {
            anim.SetBool(walkAnimation, true);
            sr.flipX = true;
        }
        else // Standing Idle
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
        /* If statements containing has no need for the curly braces,
        when there a multiple lines of code that want to be nested in
        the if statement then the curly braces are used */

        if (!player)
            return;

        if (collision.gameObject.CompareTag(Ground_Tag))
            isGrounded = true;

        if (collision.gameObject.CompareTag(Enemy_Tag))
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // The Collider2D has a CompareTag property as opposed the Collision2d that has a pseudo "." property
        if (collision.CompareTag(Enemy_Tag))
            Destroy(gameObject);
    }
} // Class
