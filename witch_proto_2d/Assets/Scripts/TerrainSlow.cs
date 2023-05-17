using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSlow : MonoBehaviour
{


    // references
    public GameObject player;
    Player playerScript;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript.slowMultiplier = playerScript.mudMultiplier;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript.slowMultiplier = 1f;
        }
    }
}
