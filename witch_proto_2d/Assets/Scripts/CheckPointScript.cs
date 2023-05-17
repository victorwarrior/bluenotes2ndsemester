using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckPointManagerScript.currentCheckpoint = transform.position;
            CheckPointManagerScript.hasCheckPoint = true;
            Debug.Log("Checkpoint!!" + CheckPointManagerScript.hasCheckPoint);
        }
    }
}
