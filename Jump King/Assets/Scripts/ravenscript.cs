using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ravenscript : MonoBehaviour
{

    //Detect trigger with player
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If we triggerd the player enable playerdeteced and show indicator
        if( other.tag == "Player")
        {
            UnityEngine.Debug.Log("Collided with player");
        }
    }
}
