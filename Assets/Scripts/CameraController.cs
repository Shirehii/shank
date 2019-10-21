using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    //Initializes when the script starts
    void Start()
    {
        //Get Components
        offset = new Vector3(0,0,-5);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
