using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    private Rigidbody2D rig;
    private AudioSource source;

    //Player Speeds
    public float moveSpeed = 150f;
    public float jumpSpeed = 350f;

    //Ground check
    private bool isGrounded; //to check if the player is touching the ground

    //Variables for player states, used in animation controller
    public bool idle;
    public bool moving;
    public bool jumping;

    //Other variables
    private int points = 0;
    private int lives = 3;
    private float lastDirectionPressed = 1;


    //Initializes when the script starts
    void Start()
    {
        //Get Components
        rig = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    //Player Input
    void Update()
    {
        moving = false;
        if (Input.GetAxis("Vertical") > 0 && isGrounded == true) //If the model is touching the ground and the player pressed the up button...
        {
            rig.AddForce(new Vector2(0, jumpSpeed)); //...Add a vertical force to the model
            isGrounded = false;
            jumping = true;
        }

        if (Input.GetAxis("Horizontal") != 0) //If the player presses any of the horizontal axis buttons...
        {
            rig.velocity = (new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rig.velocity.y)); //...move the model to the direction the player presses, while retaining the vertical velocity of the rigidbody
            moving = true;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != lastDirectionPressed)
        {
            Flip();
            lastDirectionPressed = Input.GetAxisRaw("Horizontal");
        }
    }

    //Collision stuff
    void OnCollisionEnter2D(Collision2D col)
    {
        //Checking for ground
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumping = false;
        }
    }

    //Trigger stuff
    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Point collecting
        if (trigger.gameObject.tag == "Point")
        {
            points += 1;
            Debug.Log(points);
            Destroy(trigger.gameObject);
        }

        //Adding HP
        if (trigger.gameObject.tag == "Heal")
        {
            Destroy(trigger.gameObject);
            lives += 1;
            Debug.Log(lives);
        }

        //Death
        if (trigger.gameObject.tag == "Enemy" && lives > 0)
        {
            Destroy(trigger.gameObject);
            lives -= 1;
            Debug.Log(lives);
        }
        else if (trigger.gameObject.tag == "Enemy" && lives == 0)
        {
            Destroy(gameObject);
            Debug.Log(lives);
        }
    }

    //Method to use to flip the player sprite
    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
