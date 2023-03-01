using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark : MonoBehaviour
{
    void Start() {
        // @TODO: instead of all landmarks having a script, the controller could maybe do this for all landmarks? -Victor
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);        
    }

    void Update() {
        
    }
}
