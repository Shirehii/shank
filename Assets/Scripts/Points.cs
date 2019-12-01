using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public PlayerController player;
    private string totalPoints;
    private Text results;

    // Start is called before the first frame update
    void Start()
    {
        results = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        totalPoints = player.points.ToString();
        results.text = "Points : " + totalPoints;
    }
}
