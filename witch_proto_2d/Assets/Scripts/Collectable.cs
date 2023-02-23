using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    

    void Start() {
        
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D col) { // Collision2D eller Collider2D?
        if (col.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }
}
