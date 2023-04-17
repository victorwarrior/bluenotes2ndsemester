using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    // references
    public GameObject  player;
           Rigidbody2D rb;

    // constants
    float aggroRange   = 11.5f;
    float deaggroRange = 15.5f;
    float walkSpeed    = 0.04f;
    float followSpeed  = 0.16f;
    float dashSpeed    = 0.24f;
    float walkDistance = 20f;

    // other
    float   timer          = 0f;
    string  mode           = "";
    int     type           = 0;
    Vector3 walkPosition   = new Vector3(0f, 0f, 0f);
    Vector2 chaseDirection;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb      = GetComponent<Rigidbody2D>();
        rb.drag = 5.25f;

        mode      = "wait";
        timer     = Random.Range(0f, 10f);
        type      = Random.Range(0, 2);

        //transform.position = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), transform.position.z);

    }



    void FixedUpdate() {

        string nextMode = "";
        if (false) {

        // states: wait, move, attack, stunned

        // wait: alert. do nothing.
        // move: alert. move towards random point.
        // attack: move towards player.
        // stunned: do nothing.

        } else if (mode == "wait") {

            // behaviour
            timer -= Time.deltaTime;

            if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange) {
                nextMode = "attack";
            } else if (timer <= 0f) {
                nextMode = "move";
            }

        } else if (mode == "move") {

            // behaviour
            if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange) {
                nextMode = "attack";
            } else if (transform.position != walkPosition) {
                transform.position = Vector2.MoveTowards(transform.position, walkPosition, walkSpeed);
            } else {
                nextMode = "wait";
            }

        } else if (mode == "attack") {

            // behaviour
            if (Vector2.Distance(transform.position, player.transform.position) >= 15) {
                nextMode = "wait";
            } else {
                if (type == 0) {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
                }
                else if (type == 1) {
                    if (transform.position != new Vector3(chaseDirection.x, chaseDirection.y, transform.position.z)) { // @BUG: @TODO: if two chase you they can get stuck and never reach this point. anyway the dash should go past the player and also be based a fixed time -Victor 
                        transform.position = Vector2.MoveTowards(transform.position, chaseDirection, dashSpeed);
                    } else {
                        nextMode = "stunned";
                    }
                }
            }
        
        } else if (mode == "stunned") {

            // behaviour
            timer -= Time.deltaTime;

            if (timer <= 0) {
                if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange) {
                    nextMode = "attack";
                } else {
                    nextMode = "wait";
                }
            }

        }


        if (nextMode == "") {

            // no transition

        } else if (nextMode == "wait") {
            mode = "wait";
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    timer = Random.Range(4f, 30f);
                    break;
            }
        } else if (nextMode == "move") {
            mode = "move";
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    walkPosition = new Vector3(transform.position.x + Random.Range(-walkDistance, walkDistance), // @TODO: should it be a circle instead of a square?
                                               transform.position.y + Random.Range(-walkDistance, walkDistance),
                                               transform.position.z);
                    break;
            }
        } else if (nextMode == "attack") {
            mode = "attack";
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    chaseDirection = new Vector2(player.transform.position.x, player.transform.position.y);
                    break;
            }
        } else if (nextMode == "stunned") {
            mode = "stunned";
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    timer = 0.8f;
                    break;
            }
        }



        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 
        }
    }
}
