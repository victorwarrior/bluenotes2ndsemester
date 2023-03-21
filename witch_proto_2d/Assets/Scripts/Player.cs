using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    // references
    Rigidbody2D rb;

    // constants               ** explanations: **

    // CAREFUL WHEN MAKING CONSTANTS PUBLIC, IF PUBLIC THEY FOLLOW THE VALUE IN THE EDITOR
    float speed       = 40.5f; // how fast you accelerate
    float maxSpeed    = 100f;  // top speed
    float sprintBoost = 1.6f;  // speed * sprintBoost = speed when sprinting, top speed * sprintBoost = top speed when sprinting
    
    float friction    = 5.85f; // used to make the character slow down, so it eventually stands still no keys are being pressed
    
    int stamina       = 1000;  // used for sprinting
    int staminaMax    = 1000;  
    int staminaDrain  = 10;    // how much stamina is used per tick when sprinting
    int staminaRegen  = 1;     // how much stamina is gained when not sprinting

    // other
    bool staminaRegenBool = false;

    private Object StoneLight;

    Transform child;


    void Start() {

        rb      = GetComponent<Rigidbody2D>();
        rb.drag = friction; 

    }


    void FixedUpdate() {

        // checks and stores what keys are pressed
        bool keyUp        = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        bool keyDown      = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        bool keyLeft      = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool keyRight     = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);

        bool keySprint    = Input.GetKey(KeyCode.LeftShift);
        bool keySprintEnd = Input.GetKeyUp(KeyCode.LeftShift);

        bool keyLight     = Input.GetKeyDown("l");
        bool keyLightEnd  = Input.GetKeyUp("l");

        // determines direction based on what keys are pressed @TODO: decide what pressing left then right should result in and then write something that shouldnt be touched again
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

        float possibleSprintBoost = 1f;
        if (keySprintEnd) staminaRegenBool = true;

        if (keySprint && stamina >= staminaDrain && (keyDown || keyUp || keyRight || keyLeft)) {
            stamina            -= staminaDrain;
            possibleSprintBoost = sprintBoost;
            staminaRegenBool    = false;
        } else if (stamina <= staminaMax && staminaRegenBool) {
            stamina += staminaRegen;
            if (stamina > staminaMax) stamina = staminaMax;
        }

        rb.AddForce(new Vector2(horDir * speed * possibleSprintBoost, verDir * speed * possibleSprintBoost));
        if (rb.velocity.x > (maxSpeed * possibleSprintBoost)) rb.velocity = new Vector2((maxSpeed * possibleSprintBoost), rb.velocity.y); // @TODO: does this enforce the wanted amount of restriction on diagonal movement too? hmm, double check
        if (rb.velocity.y > (maxSpeed * possibleSprintBoost)) rb.velocity = new Vector2(rb.velocity.x, (maxSpeed * possibleSprintBoost));

        // update z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // emits a light to activate the stone circles
        StoneLight = Resources.Load("PlayerLight", typeof(GameObject));


        //if (keyLightEnd)
        //{
        //    Destroy(EmitLight.GameObject);
        //}
        
        if (keyLight)
        {
            GameObject EmitLight         = Instantiate (StoneLight) as GameObject;
            EmitLight.transform.parent   = this.transform;
            EmitLight.transform.position = this.transform.position;

        }
        if (keyLightEnd)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                child = this.transform.GetChild(i);
                if (child.name == "PlayerLight(Clone)")
                {
                    Destroy(child.gameObject);
                }

            }
        }

    }

    
}
