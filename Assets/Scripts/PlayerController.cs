using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    private Rigidbody2D rig;
    public GameObject leg;
    private AudioSource source;

    //Audio clips
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
    public bool idle = true;
    public bool moving = false;
    public bool dead = false;
    public bool paused = false;
    public bool won = false;

    //Movement & Jumping variables
    public bool isGrounded; //to check if the player is touching the ground
    private bool jumpNow = false;

    //Mechanics variables
    private float lastDirectionPressed = 1;
    public int points = 0;
    public Lives lives;

    //UI variables
    public GameObject pausePanel;
    public Text pauseText;
    public GameObject winPanel;
    public GameObject gameOverPanel;


    void Start()
    {
        //Get Components
        rig = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        lives = GameObject.FindGameObjectWithTag("Collectibles").GetComponent<Lives>();
    }

    void Update() //Player Input
    {
        //--MOVEMENT--
        if (rig.bodyType == RigidbodyType2D.Dynamic && !dead) //If player isn't dead, game isn't paused, and level isn't cleared then check for movement input
        {
            if (Input.GetAxis("Vertical") > 0 && isGrounded) //If the model is touching the ground and the player presses the up button...
            {
                jumpNow = true; //...Jump
                source.PlayOneShot(jump); //Play jump sound
            }

            if (Input.GetAxis("Horizontal") != 0) //If the player presses any of the horizontal axis buttons...
            { 
                moving = true; //...Move
                if (isGrounded && !source.isPlaying) //Play walking sound
                {
                    source.PlayOneShot(walk);
                }
            }

            if (Input.GetAxisRaw("Horizontal") == 0) //If the user isn't pressing the left or right keys...
            {
                moving = false; //...Don't move
            }

            if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != lastDirectionPressed) //For flipping the player sprite
            {
                Flip();
                lastDirectionPressed = Input.GetAxisRaw("Horizontal");
            }
        }

        //--UI--
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
        
        if (dead == true) //For Game Over
        {
            gameOverPanel.gameObject.SetActive(true);
            rig.bodyType = RigidbodyType2D.Static;
        }
    }

    private void FixedUpdate() //Movement physics
    {
        if (moving) //Moving
        {
            Move();
        }

        if (jumpNow) //Jumping
        {
            Jump();
        }
    }
    
    void OnTriggerEnter2D(Collider2D trigger) //Triggers
    {
        if (trigger.gameObject.tag == "DestroyWall") //For destroying secret walls
        {
            Destroy(GameObject.FindGameObjectWithTag("DestroyWall"));
            Destroy(trigger.gameObject);
        }
        
        if (trigger.gameObject.tag == "Win" && !dead) //Winning
        {
            source.PlayOneShot(damage);
            winPanel.gameObject.SetActive(true);
            rig.bodyType = RigidbodyType2D.Static;
            won = true;
            Destroy(trigger.gameObject);
        }
        
        if (trigger.gameObject.tag == "Point") //Getting points
        {
            source.PlayOneShot(point);
            points += 1;
            trigger.gameObject.SetActive(false);
        }
        
        if (trigger.gameObject.tag == "Heal") //Healing
        {
            lives.hearts += 1;
            source.PlayOneShot(heal);
            trigger.gameObject.SetActive(false);
        }
        
        if (trigger.gameObject.tag == "Enemy" && lives.hearts > 0) //Taking damage
        {
            lives.hearts -= 1;
            source.PlayOneShot(damage);
            trigger.gameObject.SetActive(false);
        }
        else if (trigger.gameObject.tag == "Enemy" && lives.hearts == 0) //Death
        {
            dead = true;
            source.PlayOneShot(death);
        }
    }

    void Move() //Moving (horizontally)
    {
        rig.velocity = (new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rig.velocity.y));
    }

    void Jump() //Jumping
    {
        rig.AddForce(new Vector2(0, jumpSpeed)); //Add a vertical force to the model
        isGrounded = false; //Mark player as not grounded
        jumpNow = false; //Don't jump again
    }

    void Flip() //Method to use to flip the player sprite
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}