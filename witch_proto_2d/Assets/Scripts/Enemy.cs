using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

    // references
    public GameObject  player;
           Rigidbody2D rb;

    // constants
    float aggroRange   = 9.5f;
    float deaggroRange = 9.5f;
    float walkSpeed    = 0.04f;
    float followSpeed  = 0.16f;
    float dashSpeed    = 0.32f;
    float walkDistance = 20f;

    // other
    float   timer        = 0f;
    
    int     type         = 0;
    Vector3 walkPosition = new Vector3(0f, 0f, 0f);
    Vector2 chaseDirection;
    public string mode   = "wait";
    public bool isAttacking;

    static float       soundTimer;
    public AudioSource audioSource1;
    public AudioClip growl1;
    public AudioClip growl2;
    public AudioClip growl3;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        rb      = GetComponent<Rigidbody2D>();
        rb.drag = 5.25f;

        timer     = Random.Range(2f, 20f);
        type      = Random.Range(0, 2);
        type      = 1;

        isAttacking = false;

        soundTimer = 0;

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

        } else if (mode == "guard") {

            if (isAttacking == true) {
                nextMode = "attack";
            }

        } else if (mode == "move") {

            if (isAttacking == true) {
                nextMode = "attack";
            } else if (timer <= 0f) {
                nextMode = "wait";
            } else if (transform.position != walkPosition) {
                transform.position = Vector2.MoveTowards(transform.position, walkPosition, walkSpeed);
            }

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
                        AttackSound();
                    } else {
                        nextMode = "stunned";
                    }
                }
            }
        
        } else if (mode == "stunned") {

            isAttacking = false;

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
                    break;
                case "wait":
                case "move":
                case "stunned":
                    break;
            }
        }

        
        

            
       
       if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange * 1.3)
            {
            soundTimer = soundTimer - Time.deltaTime;
            Debug.Log("angel sound" + soundTimer);
            if (soundTimer <= 0f)
            {
                int soundNr = Random.Range(1, 3);
                if (soundNr == 1)
                {
                    audioSource1.clip = growl1;
                    audioSource1.volume = 0.1f;
                    audioSource1.Play();
                    soundTimer = Random.Range(2f, 8f);
                }
                if (soundNr == 2)
                {
                    audioSource1.clip = growl2;
                    audioSource1.volume = 0.1f;
                    audioSource1.Play();
                    soundTimer = Random.Range(2f, 8f);
                }
            }
               
            }
        
        

        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void AttackSound()
    {
        audioSource1.clip = growl3;
        audioSource1.volume = 0.1f;
        float pitchMod = Random.Range(0.9f, 1.1f);
        audioSource1.pitch = pitchMod;
        audioSource1.Play();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            player.GetComponent<Player>().hp -= 40; // 
        }
    }
}
