using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraymenSpawner : MonoBehaviour {

    
    // references
    public GameObject       player;
    public GameObject       graymanPrefab;

    // other
    public bool spawning = false;
    
    void Start() {
        SpriteRenderer[] editorSprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in editorSprites) {
            sprite.enabled = false;
        } 
    }

    void FixedUpdate() {
        if (spawning && player != null && graymanPrefab != null) {
            if (Random.Range(0, 100) < 14) {
                GameObject inst = Instantiate(graymanPrefab,
                                              new Vector3(transform.position.x + Random.Range(-32.5f, 32.5f),
                                                          transform.position.y + Random.Range(-22f, 22f),
                                                          transform.position.z + 100),
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
