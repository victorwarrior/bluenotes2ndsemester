using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    // references
    public GameObject  player;
           Rigidbody2D rb;

    // constants
           float   aggroRange     = 11.5f;
           float   deaggroRange   = 15.5f;
           float   walkSpeed      = 0.04f;
           float   followSpeed    = 0.16f;
           float   walkDistance   = 20f;
           int     enemyType      = 0;

    // other
    public float   timer          = 0f;
           Vector3 walkPosition   = new Vector3(0f, 0f, 0f);
           string  mode           = "";
           Vector2 chaseDirection;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb      = GetComponent<Rigidbody2D>();
        rb.drag = 5.25f;

        mode    = "wait";
        timer   = Random.Range(0f, 10f);

        //transform.position = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), transform.position.z);

        enemyType = Random.Range(0, 2);
    }



    void FixedUpdate() {

        bool to_mode_wait   = false;
        bool to_mode_move   = false;
        bool to_mode_attack = false;

        if (false) {

        // states: wait, move, attack

        // wait: do nothing. be aware of aggro range.
        // move: move towards point, transition to wait when arriving. be aware of aggro range.
        // attack: move towards player.

        } else if (mode == "wait") {

            // behaviour
            timer -= Time.deltaTime;

            if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange) {
                to_mode_attack = true;
               // chaseDirection = (Vector2)player.transform.position;
            } else if (timer <= 0f) {
                to_mode_move = true;
            }

            // transitions
            if (to_mode_move) {
                mode         = "move";
                walkPosition = new Vector3(transform.position.x + Random.Range(-walkDistance, walkDistance), // @TODO: should it be a circle instead of a square?
                                           transform.position.y + Random.Range(-walkDistance, walkDistance),
                                           transform.position.z);
            }
            if (to_mode_attack) {
                mode  = "attack";
                timer = 0.8f;
                chaseDirection = (Vector2)player.transform.position;
            }


        } else if (mode == "move") {

            // behaviour
            if (Vector2.Distance(transform.position, player.transform.position) <= aggroRange) {
                to_mode_attack = true;
               // chaseDirection = (Vector2)player.transform.position;
            } else if (transform.position != walkPosition) {
                transform.position = Vector2.MoveTowards(transform.position, walkPosition, walkSpeed);
            } else {
                to_mode_wait = true;
            }

            // transitions
            if (to_mode_wait)   {
                mode  = "wait";
                timer = Random.Range(4f, 30f);
            }
            if (to_mode_attack) {
                mode  = "attack";
                //timer = 0.1f;
                chaseDirection = (Vector2)player.transform.position;
            }


        } else if (mode == "attack") {

            // behaviour    
            if (Vector2.Distance(transform.position, player.transform.position) >= 15) {
                to_mode_wait = true;
            } else {
                if(enemyType == 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
                }
                else if (enemyType == 1)
                {
                    //chaseDirection = player.transform.position;
                    // StartCoroutine(Attacking());
                    //rb.velocity = chaseDirection * 2 * followSpeed;
                    transform.position = Vector2.MoveTowards(transform.position, chaseDirection * 2, 2*followSpeed);
                }
            }

            // transitions
            if (to_mode_wait) {
                mode  = "wait";
                timer = Random.Range(4f, 30f);
            }
            if (to_mode_move) mode = "move";

        }

        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    IEnumerator Attacking()
    {
       // Vector2 chaseDirection = player.transform.position - transform.position;
       // chaseDirection = player.transform.position;
         rb.velocity            = chaseDirection * 5 * followSpeed;
        yield return new WaitForSeconds(10f);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 
        }
    }
}
