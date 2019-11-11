using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    private Rigidbody2D rig;
    private AudioSource source;

    public AudioClip heal;
    public AudioClip damage;
    public AudioClip death;
    public AudioClip point;
    public AudioClip walk;
    public AudioClip jump;

    //Player Speeds
    public float moveSpeed = 150f;
    public float jumpSpeed = 350f;

    //Variables for player states, mostly used in animation controller
    public bool idle;
    public bool moving;
    public bool jumping;
    public bool dead = false;
    public bool paused = false;
    private bool isGrounded; //to check if the player is touching the ground
    
    public Dialogue dialogue;

    //Mechanics variables
    private int points = 0;
    public int lives = 3;
    private float lastDirectionPressed = 1;

    //UI variables
    public GameObject pausePanel;
    public Text pauseText;
    public GameObject gameOverPanel;
    public GameObject winPanel;


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
        if (dialogue.watchingIntro == 0 && !dead && !paused) //If player isn't currently watching the level intro, isn't dead and isn't paused, then check for movement input
        {
            if (Input.GetAxis("Vertical") > 0 && isGrounded) //If the model is touching the ground and the player pressed the up button...
            {
                rig.AddForce(new Vector2(0, jumpSpeed)); //...Add a vertical force to the model
                isGrounded = false;
                jumping = true;
                source.PlayOneShot(jump);
            }

            if (Input.GetAxis("Horizontal") != 0) //If the player presses any of the horizontal axis buttons...
            {
                rig.velocity = (new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rig.velocity.y)); //...move the model to the direction the player presses, while retaining the vertical velocity of the rigidbody
                moving = true;
                if (isGrounded && !source.isPlaying)
                {
                    source.PlayOneShot(walk);
                }
            }

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != lastDirectionPressed) //For flipping the player sprite
            {
                Flip();
                lastDirectionPressed = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                moving = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && !dead && !paused) //For Pausing
        {
            paused = true;
            pausePanel.SetActive(true);
            pauseText.text = "Press P to Unpause";
            rig.bodyType = RigidbodyType2D.Static;
        }
        else if (Input.GetKeyDown(KeyCode.P) && paused) //For Unpausing
        {
            paused = false;
            pausePanel.SetActive(false);
            pauseText.text = "Press P to Pause";
            rig.bodyType = RigidbodyType2D.Dynamic;
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
            source.PlayOneShot(point);
        }

        //Adding HP
        if (trigger.gameObject.tag == "Heal")
        {
            Destroy(trigger.gameObject);
            lives += 1;
            source.PlayOneShot(heal);
        }

        //Death
        if (trigger.gameObject.tag == "Enemy" && lives > 0)
        {
            Destroy(trigger.gameObject);
            lives -= 1;
            source.PlayOneShot(damage);
        }
        else if (trigger.gameObject.tag == "Enemy" && lives == 0)
        {
            dead = true;
            gameOverPanel.gameObject.SetActive(true);
            source.PlayOneShot(death);
        }

        //Winning
        if (trigger.gameObject.tag == "Win" && !dead)
        {
            source.PlayOneShot(damage);
            winPanel.gameObject.SetActive(true);
            rig.bodyType = RigidbodyType2D.Static;
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