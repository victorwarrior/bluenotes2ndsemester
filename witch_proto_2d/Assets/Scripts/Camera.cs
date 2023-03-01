using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // references
    public GameObject player;

    void Start() {
        
    }

    void Update() {
        // following the player  
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -500);

        // @TODO: notice camera z = -500 and background z = 500, this could cause trouble when
        //        y gets < -500 or > 500... could you move the background and the camera too?

    }
}
