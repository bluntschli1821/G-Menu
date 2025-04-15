using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    [HideInInspector]
    float speed;

    Rigidbody2D myBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Start() { }

    // Update is called once per frame
    void Update() { }
}
