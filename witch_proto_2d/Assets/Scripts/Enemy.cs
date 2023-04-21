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
    float dashSpeed    = 0.36f;
    float walkDistance = 20f;

    // other
    float   timer        = 0f;
    string  mode         = "";
    int     type         = 0;
    Vector3 walkPosition = new Vector3(0f, 0f, 0f);
    Vector2 chaseDirection;
    public bool isAttacking;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb      = GetComponent<Rigidbody2D>();
        rb.drag = 5.25f;

        mode      = "wait";
        timer     = Random.Range(2f, 20f);
        type      = Random.Range(0, 2);
        type      = 1;

        isAttacking = false;

        //transform.position = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), transform.position.z);

    }


    void FixedUpdate() {

        string nextMode = "";
        timer           = timer - Time.deltaTime;

        if (false) {

        // states: wait, move, attack, stunned

        // wait: alert. do nothing.
        // move: alert. move towards random point.
        // attack: move towards player.
        // stunned: do nothing.

        } else if (mode == "wait") {

            if (isAttacking == true) {
                nextMode = "attack";
            } else if (timer <= 0f) {
                nextMode = "move";
            }

        } else if (mode == "move") {

//            float threshold = 1f;

            if (isAttacking == true) {
                nextMode = "attack";
            } else if (timer <= 0f) {
                nextMode = "wait";
            } else if (transform.position != walkPosition) {
                transform.position = Vector2.MoveTowards(transform.position, walkPosition, walkSpeed);
            }
//            } else if (((transform.position.x - walkPosition.x <= threshold && transform.position.x - walkPosition.x > 0) || (transform.position.x - walkPosition.x >= -threshold && transform.position.x - walkPosition.x < 0))
//                   &&  ((transform.position.y - walkPosition.y <= threshold && transform.position.y - walkPosition.y > 0) || (transform.position.y - walkPosition.y >= -threshold && transform.position.y - walkPosition.y < 0))) {
//                Debug.Log("move to wait.");
//                nextMode = "wait";
//            }

        } else if (mode == "attack") {

            if (Vector2.Distance(transform.position, player.transform.position) >= 15) {
                nextMode = "wait";
            } else {
                if (type == 0) {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
                }
                else if (type == 1) {
                    if (timer > 0f) {
                        transform.Translate(dashSpeed * chaseDirection);
                    } else {
                        nextMode = "stunned";
                    }
                }
            }
        
        } else if (mode == "stunned") {

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
            timer = Random.Range(2f, 20f);
            switch (mode) {
                case "attack":
                    isAttacking = false;
                    break;
                case "wait":
                case "move":
                case "stunned":
                    break;
            }
        } else if (nextMode == "move") {
            mode             = "move";
            timer            = Random.Range(4f, 14f); // @TODO: enemies should switch to walk when they reach their new position instead of this stupid fix -Victor
            float newWalkDir = Random.Range(0, 2.0f*Mathf.PI);
            walkPosition     = new Vector3(transform.position.x + Mathf.Sin(newWalkDir)*walkDistance,
                                           transform.position.y + Mathf.Cos(newWalkDir)*walkDistance,
                                           transform.position.z);
            //walkPosition = new Vector3(transform.position.x + Random.Range(-walkDistance, walkDistance), // @TODO: should it be a circle instead of a square?
            //                           transform.position.y + Random.Range(-walkDistance, walkDistance),
            //                           transform.position.z);
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    break;
            }
        } else if (nextMode == "attack") {
            mode           = "attack";
            timer          = 0.85f;
            chaseDirection = (new Vector2(player.transform.position.x, player.transform.position.y)
                             -new Vector2(transform.position.x, transform.position.y)).normalized;
            switch (mode) {
                case "wait":
                case "move":
                case "attack":
                case "stunned":
                    break;
            }
        } else if (nextMode == "stunned") {
            mode  = "stunned";
            timer = 0.7f;
            switch (mode) {
                case "attack":
                    isAttacking = false;
                    break;
                case "wait":
                case "move":
                case "stunned":
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
