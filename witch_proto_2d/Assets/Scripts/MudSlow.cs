using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlow : MonoBehaviour
{
    public float slowScale = 0.7f; //to modify the maxspeed of the player
    public GameObject player;
    public GameObject nuckelavee;
   // Player playerScript = player.GetComponent<Player>();
  /*void OnTriggerEnter2D(Collider2D col) //slows the player when the player enters the slow field
    {
        if (col.GetComponent<Player>())
        {
           // player.maxSpeed = player.maxSpeed * slowScale;
        }
    }

    void OnTriggerExit2D(Collider2D col) //unslows player when leaving slow field
    {
        if (col.GetComponent<Player>())
        {
            //  playerScript.maxSpeed = playerScript.maxSpeed / slowScale;
        }
    }*/
}
