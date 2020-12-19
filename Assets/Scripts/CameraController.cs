using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*
        leftBound = smallest permitted x position of the camera
        rightBound = largest permitted x position of the camera
        xPos = the camera's current x position
    */
    public float leftBound, rightBound, xPos;
    public bool canMove = true;
    
    // the player character the camera will follow
    public GameObject fPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        fPlayer = GameObject.FindWithTag("Player");     // find the player character in the scene
    }

    // Update is called once per frame
    void Update()
    {
        // move the camera if permitted
        if (canMove)
            MoveCamera();
    }

    // MoveCamera matches the camera's x position with the player's within the permitted bounds
    void MoveCamera()
    {
        if(fPlayer.transform.position.x < leftBound)
        {   xPos = leftBound;   }   // if the player is outside of the leftmost boundary, freeze the camera at leftBound
        else if (fPlayer.transform.position.x > rightBound)
        {   xPos = rightBound;  }   // if the player is outside of the rightmost boundary, freeze the camera at rightBound
        else
        {   xPos = fPlayer.transform.position.x;    }   // otherwise, have the camera's x position equal to the player's

        transform.position = new Vector3(xPos, transform.position.y, -10);  // move the camera to the appropriate position
    }
}