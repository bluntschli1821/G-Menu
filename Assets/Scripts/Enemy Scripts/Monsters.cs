using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    Rigidbody2D myBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake() //Function
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start() //Function
    { }

    // Update is called once per frame
    void FixedUpdate() //Function
    {
        myBody.linearVelocity = new Vector2(speed, myBody.linearVelocity.y);
    }
} //Class
