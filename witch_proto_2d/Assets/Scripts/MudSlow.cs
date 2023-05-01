using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlow : MonoBehaviour {

    // constants
    float lifeTime    = 3f;
    float fadeAmount  = 0.01f;

    // other
    float timer;
    float alpha;

    // references
    public GameObject player;
    Player playerScript;

    public void Start() {
        player       = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        alpha        = 1f;
        timer        = lifeTime;
    }

    public void FixedUpdate() {
        timer -= Time.deltaTime;
        if (timer <= 0f) {
            alpha = alpha - fadeAmount;
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, alpha);
            if (alpha <= 0f) {
                Debug.Log("Destroyed mud object");
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerScript.slowMultiplier = playerScript.mudMultiplier;
            //Debug.Log("player is mud slowed.");
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerScript.slowMultiplier = 1f;
            //Debug.Log("player is not longer slowed.");
        }
    }
}
