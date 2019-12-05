using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour //This scipt is used in the score counter at the end of the level
{
    public PlayerController player; //The PlayerController script
    private Text totalPoints; //The text component in the WinPanel

    void Start()
    {
        totalPoints = GetComponent<Text>(); //Get the text component
        player = GameObject.Find("Player").GetComponent<PlayerController>(); //Find the PlayerController script
    }
    
    void Update()
    {
        totalPoints.text = "Points : " + player.points.ToString(); //Set the points in the text component, after converting them to a string 
    }
}