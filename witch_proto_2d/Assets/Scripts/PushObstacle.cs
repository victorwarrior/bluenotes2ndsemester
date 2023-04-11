using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushObstacle : MonoBehaviour
{
    public float pushSpeed;
    public GameObject Player;

    //position and scale variables
   

    void OnCollisionEnter2D(Collision2D col)
    {
        
        ContactPoint2D contact = col.GetContact(0);
        switch (contact)
        {
            
            
        }
    }

}

