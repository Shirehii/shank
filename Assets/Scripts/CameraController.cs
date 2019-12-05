using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; //The Player
    private Vector3 offset = new Vector3(0, 0, -5); //The distance between the player and the camera

    void Start() 
        {
            player = GameObject.FindGameObjectWithTag("Player"); //Find the Player
        }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset; //The camera's position should be the player's position + the distance
    }
}
