using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grayman : MonoBehaviour {

    // references
    public GameObject player;
    BoxCollider2D damageCollider;

    // constants
    float fadeSpeed = 0.01f;
    float timeAlive;
    float speed;

    // other
    float timer;
    float alpha;
    bool  fadeIn;
    bool  fadeOut;

    void Start() {
        damageCollider = GetComponent<BoxCollider2D>();
        damageCollider.enabled = false;

        timeAlive = 2.8f + Random.Range(-0.4f, 0.4f);
        speed     = 0.06f + Random.Range(-0.01f, 0.01f);
        timer     = 0f;
        alpha     = 0f;
        fadeIn    = true;
        fadeOut   = false;

        transform.position = new Vector3(player.transform.position.x + Random.Range(- 20f, 20f),
                                         player.transform.position.y + Random.Range(- 20f, 20f),
                                         transform.position.z);

        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].material.color = new Color(1f, 1f, 1f, alpha);
        }
    }

    void FixedUpdate() {

        // fading in and out
        if (alpha < 1f && fadeIn) {
            alpha += fadeSpeed;
            if (alpha >= 1f) {
                alpha  = 1f;
                fadeIn = false;
                timer  = timeAlive;

                damageCollider.enabled = true;
            }
        } else if (fadeOut) {
            alpha -= fadeSpeed;
            if (alpha <= 0) {
                Destroy(gameObject);
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0f && fadeIn == false) {
            fadeOut = true;
            damageCollider.enabled = false;
        }

        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].material.color = new Color(1f, 1f, 1f, alpha);
        }

        // move down
        transform.Translate(speed * new Vector3(0, -1, transform.position.z));

        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 
        }
    }
}