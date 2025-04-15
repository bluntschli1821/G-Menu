using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    private Vector3 tempPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
