using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float moveSpeed = 4f;       // how quickly the player moves
    private float jumpHeight = 6.5f;    // how high the player can jump

    public bool facingRight = true;     // indicates what direction the player is facing

    Rigidbody2D playerRigid;            // the player rigidbody
    Sprite playerSprite;                // the player sprite

    RaycastHit2D horizontalGC;          // the raycast checking if the player is grounded               (horizontal Ground Check)
    Vector3 horizontalRO;               // the offset of the raycast relative to the player's position  (horizontal Raycast Offset)

    Animator playerAnim;                // the player animator

    public bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody, sprite, and animator components of the player object
        playerRigid = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>().sprite;
        playerAnim = GetComponent<Animator>();

        // set the player's raycast offset
        horizontalRO = new Vector3(playerSprite.bounds.extents.x,playerSprite.bounds.extents.y + 0.05f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast to check if grounded
        horizontalGC = Physics2D.Raycast(transform.position - horizontalRO, Vector2.right, 1);
        Debug.DrawRay(transform.position - horizontalRO, Vector2.right * 1, Color.red);

        MovePlayer();       // move the player
        IsGrounded();
        PlayerJump();       // check if the player can jump
        PlayAnimations();   // play the appropriate animations
    }

    // MovePlayer allows the player to move across the level
    void MovePlayer()
    {
        // if horizontal input is detected
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            // change the velocity of the player
            playerRigid.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, playerRigid.velocity.y);

            // flip the player to face the correct direction
            if ((facingRight && Input.GetAxisRaw("Horizontal") < 0)
            || (!facingRight && Input.GetAxisRaw("Horizontal") > 0))
                FlipPlayer();
        }
        else
        {
            // do not move horizontally if there is no input
            playerRigid.velocity = new Vector2(0, playerRigid.velocity.y);
        }
    }

    // PlayerJump allows the player to jump when on the ground
    void PlayerJump()
    {   
        // if the space button is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && (grounded == true))
        {
            // Debug.Log("Jumped");
            playerRigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);     // add force to move the character up
            GameManagerScript.PlaySFX("Jump");      // play the jump SFX
        }
    }

    // IsGrounded returns whether or not the player is on the ground
    bool IsGrounded()
    {
        // if the raycast is in contact with a box collider, return true
        if (horizontalGC.collider)
        {
            // Debug.Log("Grounded");
            grounded = true;
            return true; 
        }

        // Debug.Log("Not Grounded");
        grounded = false;
        return false;   // return false otherwise
    }

    // PlayAnimations plays the player's animation based on their actions
    void PlayAnimations()
    {
        if (grounded == false)
            playerAnim.Play("PlayerJump");      // Play Jump animation
        else if ((playerRigid.velocity.x != 0) && (Input.GetAxisRaw("Horizontal") != 0))
            playerAnim.Play("PlayerWalk");      // Play Walk animation
        else
            playerAnim.Play("PlayerIdle");      // Stay Idle
    }

    // FlipPlayer flips the player to face the correct direction
    void FlipPlayer()
    {
        // flip the player's horizontal scale
        transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        facingRight = !facingRight;
    }

    // OnCollisionEnter2D checks when the player collides with an object
    void OnCollisionEnter2D(Collision2D collided)
    {
        // set the player as a child of the platfrom if the collider is connected to a moving platform
        // this allows the player to move with the platform
        if (collided.gameObject.tag == "Moving Plat.")
            this.transform.parent = collided.transform;
    }

    // OnCollisionExit2D checks when the player is no longer in contact with an object
    void OnCollisionExit2D(Collision2D collided)
    {
        // the player is no longer a child of a moving platform if they move or jump off
        if (collided.gameObject.tag == "Moving Plat.")
            this.transform.parent = null;
    }

    // OnTriggerEnter2D checks when the player enters a 2D trigger
    void OnTriggerEnter2D(Collider2D trig)
    {
        // if the trigger is attached to a coin
        if (trig.gameObject.tag == "Coin")
        {
            GameManagerScript.PlaySFX("coin1");     // play the coin SFX
            Destroy(trig.gameObject);               // destroy the coin
        }
    }
}