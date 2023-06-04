using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grayman : MonoBehaviour {

    // references
    public GameObject player;
    BoxCollider2D damageCollider;
    public AudioSource audioSource;
    public AudioClip whileDamage;
    public AudioClip afterDamage;

    // constants
    float fadeSpeed = 0.01f;
    float maxAlpha  = 0.40f;
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

        timeAlive = 2.4f + Random.Range(-0.25f, 0.25f);
        speed     = 0.06f + Random.Range(-0.03f, 0.03f);
        timer     = 0f;
        alpha     = 0f;
        fadeIn    = true;
        fadeOut   = false;

        //transform.position = new Vector3(player.transform.position.x + Random.Range(- 20f, 20f),
        //                                 player.transform.position.y + Random.Range(- 20f, 20f),
        //                                 transform.position.z);

        SpriteRenderer[] sprites = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++) {
            sprites[i].material.color = new Color(1f, 1f, 1f, alpha);
        }
    }

    void FixedUpdate() {

        // fading in and out
        if (alpha < maxAlpha && fadeIn) {
            alpha += fadeSpeed;
            if (alpha >= maxAlpha) {
                alpha  = maxAlpha;
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
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            player.GetComponent<Player>().hp -= 0.5f;
            audioSource.clip = whileDamage;
            audioSource.volume = 0.4f;
           // audioSource.Play();
        }
    }
    
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            player.GetComponent<Player>().hp -= 0.5f;
        }
    }
  /*  private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
                audioSource.Stop();
                audioSource.clip = afterDamage;
                audioSource.volume = 0.4f;
                audioSource.PlayOneShot(afterDamage , 04f);

        }
    }*/
}

