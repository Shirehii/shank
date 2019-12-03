using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    private Rigidbody2D rig;
    public GameObject leg;
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
    public bool dead = false;
    public bool paused = false;
    public bool won = false;
    public bool isGrounded; //to check if the player is touching the ground

    //Mechanics variables
    private bool jumpNow = false;
    private float lastDirectionPressed = 1;
    public int points = 0;
    public Lives lives;

    //UI variables
    public GameObject pausePanel;
    public Text pauseText;
    public GameObject winPanel;
    public GameObject gameOverPanel;


    //Initializes when the script starts
    void Start()
    {
        //Get Components
        rig = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        lives = GameObject.FindGameObjectWithTag("Collectibles").GetComponent<Lives>();
    }

    //Player Input
    void Update()
    {
        if (!dead && !paused && !won) //If player isn't dead and game isn't paused, then check for movement input
        {
            if (Input.GetAxis("Vertical") > 0 && isGrounded) //If the model is touching the ground and the player pressed the up button...
            {
                jumpNow = true;
            }

            if (Input.GetAxis("Horizontal") != 0) //If the player presses any of the horizontal axis buttons...
            { 
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

        //Death
        if (dead == true)
        {
            gameOverPanel.gameObject.SetActive(true);
            rig.bodyType = RigidbodyType2D.Static;
        }
    }

    private void FixedUpdate() //Physics stuff goes here
    {
        if (moving)
        {
            rig.velocity = (new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rig.velocity.y)); //...move the model to the direction the player presses, while retaining the vertical velocity of the rigidbody
        }

        if (jumpNow)
        {
            rig.AddForce(new Vector2(0, jumpSpeed)); //...Add a vertical force to the model
            isGrounded = false;
            source.PlayOneShot(jump);
            jumpNow = false;
        }
    }

    //Trigger stuff
    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Destroy secret wall
        if (trigger.gameObject.tag == "DestroyWall")
        {
            Destroy(GameObject.FindGameObjectWithTag("DestroyWall"));
            Destroy(trigger.gameObject);
        }

        //Winning
        if (trigger.gameObject.tag == "Win" && !dead)
        {
            source.PlayOneShot(damage);
            winPanel.gameObject.SetActive(true);
            rig.bodyType = RigidbodyType2D.Static;
            won = true;
            Destroy(trigger.gameObject);
        }

        //Points
        if (trigger.gameObject.tag == "Point")
        {
            source.PlayOneShot(point);
            points += 1;
            Debug.Log(points);
            trigger.gameObject.SetActive(false);
        }

        //Adding HP
        if (trigger.gameObject.tag == "Heal")
        {
            lives.hearts += 1;
            source.PlayOneShot(heal);
            trigger.gameObject.SetActive(false);
        }

        //Death
        if (trigger.gameObject.tag == "Enemy" && lives.hearts > 0)
        {
            lives.hearts -= 1;
            source.PlayOneShot(damage);
            trigger.gameObject.SetActive(false);
        }
        else if (trigger.gameObject.tag == "Enemy" && lives.hearts == 0)
        {
            dead = true;
            source.PlayOneShot(death);
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