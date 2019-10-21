using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    private Rigidbody2D rig;
    private AudioSource source;

    //Player Speeds
    public float moveSpeed = 150f;
    public float jumpSpeed = 350f;

    //Variables for player states, mostly used in animation controller
    public bool idle;
    public bool moving;
    public bool jumping;
    public bool dead = false;
    public int paused = -1; // had to use int instead of bool here :/ 1=true -1=false
    private bool isGrounded; //to check if the player is touching the ground

    //Mechanics variables
    private int points = 0;
    public int lives = 3;
    private float lastDirectionPressed = 1;

    //UI variables
    public GameObject PausePanel;
    public GameObject dialogueBox;
    public GameObject dialogueBoxSkip;
    private int watchingIntro = 0; // had to use int instead of bool here :/ 1=true 0=false
    private bool watchedIntro = false;
    public GameObject gameOverPanel;


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
        if (watchingIntro == 0 && !dead && paused == -1) //If player isn't currently watching the level intro, isn't dead and isn't paused, then check for movement input
        {
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

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != lastDirectionPressed) //For flipping the player sprite
            {
                Flip();
                lastDirectionPressed = Input.GetAxisRaw("Horizontal");
            }
        }

        if (watchingIntro == 1 && Input.GetKeyDown(KeyCode.S)) //For skipping the level intro
        {
            watchingIntro = 0;
            dialogueBox.SetActive(false);
            dialogueBoxSkip.SetActive(false);
            watchedIntro = true;
        }

        //if (Input.GetKeyDown(KeyCode.P) && !dead)
        //{
        //    paused *= -1;
        //    if (paused == 1)
        //    {
        //        PausePanel.SetActive(true);
        //        rig.bodyType = RigidbodyType2D.Static;
        //    }
        //    else
        //    {
        //        PausePanel.SetActive(false);
        //        rig.bodyType = RigidbodyType2D.Dynamic;
        //    }
        //}
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
        }

        //Death
        if (trigger.gameObject.tag == "Enemy" && lives > 0)
        {
            Destroy(trigger.gameObject);
            lives -= 1;
        }
        else if (trigger.gameObject.tag == "Enemy" && lives == 0)
        {
            dead = true;
            gameOverPanel.gameObject.SetActive(true);
        }

        //Dialogue
        if (trigger.gameObject.tag == "DialogueTrigger" && !watchedIntro)
        {
            dialogueBox.SetActive(true);
            dialogueBoxSkip.SetActive(true);
            watchingIntro = 1;
            Destroy(trigger.gameObject);
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