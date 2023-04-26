using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mist : MonoBehaviour {

    // references
    public GameObject player;

    // constants
    float fadeInSpeed  = 0.004f;
    float fadeOutSpeed = 0.002f;
    float maxAlpha     = 0.36f;
    float timeAlive    = 3.2f;

    // other
    int   dir;
    float speed;
    float alpha;
    float timer;
    bool  fadeIn;
    bool  fadeOut;

    void Start() {
        dir     = (Random.Range(0, 2) == 1) ? 1 : -1;
        speed   = Random.Range(0.02f, 0.06f);
        alpha   = 0f;
        timer   = 0f;
        fadeIn  = true;
        fadeOut = false;

        transform.localScale = transform.localScale * Random.Range(1f, 1.5f);
    }

    void FixedUpdate() {

        // fading in and out
        if (alpha < maxAlpha && fadeIn) {
            alpha += fadeInSpeed;
            if (alpha >= maxAlpha) {
                alpha  = maxAlpha;
                fadeIn = false;
                timer  = timeAlive;
            }
        } else if (fadeOut) {
            alpha -= fadeOutSpeed;
            if (alpha <= 0) {
                Destroy(gameObject);
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0f && fadeIn == false) fadeOut = true;

        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, alpha);

        // moving the mist
        transform.Translate(speed * new Vector3(dir, 0, transform.position.z));

        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
