using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    // these values indicate whether the platform is moving vertically or horizontal
    public bool moveH, moveV;

    /*
        pointA - the farthest left or downward position that the platform can travel to
        pointB - the farthest right or upward position that the platform can travel to
        direction - if negative, the platform is moving to pointA. moving to pointB otherwise
        moveSpeed - how quickly the platform is moving to either point
    */
    public float pointA, pointB, direction, moveSpeed;

    // Start is called before the first frame update
    void Start(){
        direction = 1;      // the platform starts off moving towards pointB
    }

    // Update is called once per frame
    // void Update(){}

    // FixedUpdate is called a fixed number of times per second
    void FixedUpdate()
    {
        if (moveH)
            MoveHorizontal();   // call MoveHorizontal() if the platform is meant to move horizonally
        else if (moveV)
            MoveVertical();     // call MoveVertical() otherwise
        else
            Debug.Log("Both are Inactive");     // Log if neither moveH or moveV is active
    }

    // MoveHorizontal is called to move the platform along the x-axis
    void MoveHorizontal()
    {
        // change horizontal direction if...
        if (transform.position.x <= pointA)
            direction = 1;      //  ...the platform reaches pointA
        else if (transform.position.x >= pointB)
            direction = -1;     // ... the platform reaches pointB

        // change the x position of the platform to move horizontally
        transform.position = new Vector3(transform.position.x + (moveSpeed * direction * Time.fixedDeltaTime), transform.position.y, 0);
    }

    // MoveVertical is called to move the platform along the y-axis
    void MoveVertical()
    {
        // change vertical direction if...
        if (transform.position.y <= pointA)
            direction = 1;      //  ...the platform reaches pointA
        else if (transform.position.y >= pointB)
            direction = -1;     // ... the platform reaches pointB

        // change the y position of the platform to move horizontally
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * direction * Time.fixedDeltaTime), 0);
    }
}