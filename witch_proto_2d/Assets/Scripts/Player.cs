using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // references
    Rigidbody2D rb;

    // constants
    float speed    = 40.5f; // how fast you accelerate
    float maxSpeed = 100f;  // top speed


    void Start() {

        rb = GetComponent<Rigidbody2D>();
        rb.drag = 5.25f; // used to make the character slow down, so it eventually stands still no keys are being pressed

    }


    void FixedUpdate() {

        // checks and stores what keys are pressed
        bool keyUp    = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        bool keyDown  = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        bool keyLeft  = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);

        // determines direction based on what keys are pressed
        float horDir = 0f;
        float verDir = 0f;

        if (keyUp)   verDir += 1f;
        if (keyDown) verDir += -1f;
        if (keyLeft) {
            if (verDir == 0) {
                horDir += -1f;
            } else {
                horDir = -0.70710678118654f; // -cos(45), so you don't travel faster when inputting a diagonal direction
                verDir =  0.70710678118654f * verDir;
            }
        }
        if (keyRight) {
            if (verDir == 0) {
                horDir += 1f;
            } else {
                horDir = 0.70710678118654f;
                verDir = 0.70710678118654f * verDir;
            }
        }

        // moves the character in the determined direction
        rb.AddForce(new Vector2(horDir * speed, verDir * speed));
        if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y); // @TODO: does this enforce the wanted amount of restriction on diagonal movement too? hmm, double check
        if (rb.velocity.y > maxSpeed)  rb.velocity = new Vector2(rb.velocity.x, maxSpeed);
    }

    
}
