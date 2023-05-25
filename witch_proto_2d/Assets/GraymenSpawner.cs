using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraymenSpawner : MonoBehaviour {

    
    // references
    public GameObject       player;
    public GameObject       graymanPrefab;

    // other
    public bool spawning = false;
    
    void FixedUpdate() {
        if (spawning && player != null && graymanPrefab != null) {
            if (Random.Range(0, 100) < 16) {
                GameObject inst = Instantiate(graymanPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-32f, 32f),
                                                          player.transform.position.y + Random.Range(-32f, 32f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity); // right now it figures out a random position on its own
                inst.GetComponent<Grayman>().player = player;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            spawning = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            spawning = false;
        }
    }
    
}
