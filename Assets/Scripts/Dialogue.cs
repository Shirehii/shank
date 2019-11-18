using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    //Components and stuff
    private Rigidbody2D rig;
    private Animator animator;
    private GameObject keys;

    //For checking the scene name
    private string level;
    private Scene scene;

    //Dialogue boxes and checking if the player has watched the intro or not
    public GameObject dialogueBox;
    public GameObject dialogueBoxSkip;
    public int watchingIntro = 0; // had to use int instead of bool here :/ 1=true 0=false
    private bool watchedIntro = false;

    //script haha
    public PlayerController player;

    //For animation purposes
    //For moving
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float timeToReachTarget;
    private float t = 0; /////////////////////////////////////////////////////////
    private bool moveSmoothly = false;

    private void Start()
    {
        //Get components
        rig = GetComponent<Rigidbody2D>();
        animator = GameObject.Find("PlayerAnim").GetComponent<Animator>();
        if (animator = null)
        {
            Debug.Log("can't find animator");
        }
        keys = GameObject.Find("Keys");

        scene = SceneManager.GetActiveScene();
        level = scene.name;
    }

    private void Update()
    {
        if (watchingIntro == 1 && Input.GetKeyDown(KeyCode.S)) //For skipping the level intro
        {
            ExitIntro();
        }

        if (moveSmoothly)
        {
            t += Time.deltaTime / timeToReachTarget; /////////////////////////////////////////////////////////
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t); /////////////////////////////////////////////////////////
            if (transform.position == targetPosition)
            {
                player.dead = true;
                moveSmoothly = false;
                keys.transform.position = new Vector3(0,-2,0); //LEVEL 4 INTRO CONTINUE HERE
            }
        }
    }

    //Trigger stuff
    void OnTriggerEnter2D(Collider2D trigger)
    {
        //Dialogue
        if (trigger.gameObject.tag == "DialogueTrigger" && !watchedIntro)
        {
            IntroTriggered();
            Destroy(trigger.gameObject);

            startingPosition = transform.position;
            switch (level)
            {
                case "1":
                    //gonzalo and ricardo fight as ricardo steals the spatula
                    break;
                case "2":
                    //gonzalo tells papa alfonso
                    break;
                case "3":
                    //gonzalo must get the keys to ricardo's kitchen
                    break;
                case "4": //gonzalo drops the keys in salsa
                    player.moving = true;
                    targetPosition = startingPosition + new Vector3(5, 0, 0);
                    timeToReachTarget = 2f;
                    moveSmoothly = true;
                    break;
                case "5":
                    //boss fight vs ricardo
                    break;
            }
        }
    }

    private void IntroTriggered()
    {
        dialogueBox.SetActive(true);
        dialogueBoxSkip.SetActive(true);
        watchingIntro = 1;
        rig.bodyType = RigidbodyType2D.Static;
    }

    private void ExitIntro()
    {
        watchingIntro = 0;
        rig.bodyType = RigidbodyType2D.Dynamic;
        dialogueBox.SetActive(false);
        dialogueBoxSkip.SetActive(false);
        watchedIntro = true;
        moveSmoothly = false;
    }

    //  Prologue. -Gonzalo wins golden spatula but ricardo looks upset/jealous
    //

    //  1. gonzalo and ricardo fight as ricardo steals the spatula
    //G: Ricardo! What are you doing?!
    //G: That Golden Spatula is mine!
    //R: Ay dios mio, just get a regular spatula.
    //R: You would use this to cook anyway, we both know you'd just frame it on the wall and never use it.
    //G: ...that's what trophies are for, Ricardo. To sit on the shelf.
    //G: *gasp* Are you saying that you were thinking of using that Spatula to actually cook?!
    //R: Uhh...
    //R: I guess I should leave now.
    //G: No! Ricardo!
    //G: I have to get the Golden Spatula back!

    //  2. gonzalo tells papa alfonso
    //G: ...!
    //G: Papa Alfonso! Mamma mia, it really is you!
    //A: ...Huh?? Who's there?
    //G: Papa, it's me! Your son Gonzalo! You showed up at the right time, I really need your advice!
    //A: Whatsa say sonnie??
    //G: Do you remember papa? When I was younger and you'd teach me all kinds of tricks in the kitchen? I wanted to make you feel proud of me papa...
    //G: I won the Golden Spatula papa, but that wicked Ricardo stole it away from me!
    //G: He's probably using it to cook right now! He's going to get it dirty!
    //A: A little bit louder sonnie!
    //G: ...You're right.
    //G: I shouldn't just sit here, I should go take it back from him! At all costs!
    //G: Thank you, papa Alfonso. You're my hero.
    //G: I'm going to take the Golden Spatula back and make you proud!
    //A: ...
    //A: ...What a weird kiddo.

    //  3. gonzalo must get the keys to ricardo's kitchen
    //G: Right, so Ricardo must be locked up in his kitchen right now...
    //G: That means I should find the keys to his kitchen!
    //G: Oh! My eagle vision sees the keys!
    // --camera pans to the end of the level where the keys are--
    //G: Let's a go!

    //  4. gonzalo drops the keys in salsa
    //G: Whoopsie daisy!
    //G: !
    //G: Mamma mia! The keys!
    //G: I have to get them back!

    //  5. boss fight vs ricardo
    //G: This is it, Ricardo.
    //G: You shall get away with this no more!
    //R: Oh, it's you, my dear friend Gonzalo...
    //R: It took you quite a while to find me, I was starting to lose hope.
    //G: Mamma mia, can you believe it? On my way here, I found a giant pot filled with tomato salsa, and I accidentally dropped your kitchen's keys in it, and had to dive in there to get them back--
    //R: You
    //R: You did what
    //G: Huh?
    //R: Huh?
    //G: Well, whatever. That Spatula is mine and you know it, Ricardo!
    //G: I shall take it back!
    //R: Then perish.

    //  Epilogue. -Gonzalo gets back spatula, ricardo goes on a long talk about friendship and they ??? become friends i guess
    //
}