using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public PlayerController player;
    private string totalPoints;
    private Text results;

    void Start()
    {
        results = GetComponent<Text>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    void Update()
    {
        totalPoints = player.points.ToString();
        results.text = "Points : " + totalPoints;
    }
}
