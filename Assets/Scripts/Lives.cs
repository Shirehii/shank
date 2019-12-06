using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public GameObject Heart1, Heart2, Heart3; //The 3 hearts at the top left of the screen
    public int hearts; //The player's lives

    void Start()
    {
        //At the start of the level, set lives to 3
        hearts = 3;
    }

    void Update()
    {
        if (hearts > 3) //This is here to make sure that the player never has more than 3 lives
        {
            hearts = 3;
        }

        switch (hearts) //Switch statement for how many lives the player has
        {
            case 0:
                Heart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(false);
                break;
            case 1:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(false);
                break;
            case 2:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(false);
                break;
            case 3:
                Heart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(true);
                break;
        }
    }
}
