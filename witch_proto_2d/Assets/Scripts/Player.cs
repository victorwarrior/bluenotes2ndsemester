using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    // references
    Rigidbody2D rb;
    Transform child;

    // constants               // CAREFUL WHEN MAKING CONSTANTS PUBLIC, IF PUBLIC THEY PRIORIZE THE VALUE IN THE EDITOR
    float speed       = 70.2f; // how fast you accelerate, rigidbody velocity is the real measurement of speed
    float maxSpeed    = 8f;    // top speed
    float sprintBoost = 1.75f; // speed * sprintBoost = sprinting speed, max speed * sprintBoost = top sprinting speed 
    
    float friction    = 6.5f;  // used to make the character slow down, so it eventually stands still no keys are being pressed
    
    int stamina       = 1000;  // used for sprinting
    int staminaMax    = 1000;  
    int staminaDrain  = 8;     // how much stamina is used per tick when sprinting
    int staminaRegen  = 1;     // how much stamina is gained when not sprinting

    // other
    bool  staminaRegenBool = false;
    bool latestVerIsUp     = false;
    bool latestHorIsRight  = false;

    public float xVelForDisplay = 0f;
    public float xVelForDisplayPreCalc = 0f;
    public float yVelForDisplay = 0f;
    public float yVelForDisplayPreCalc = 0f;

    Object     stoneLight;
    GameObject emitLight; 



    void Start() {

        rb         = GetComponent<Rigidbody2D>();
        rb.drag    = friction; 
        stoneLight = Resources.Load("PlayerLight", typeof(GameObject)); // @NOTE: this line was previously in FixedUpdate - it only needs to happen once though! -Victor

    }


    void FixedUpdate() {

        // checks and stores what keys are pressed
        bool keyUp    = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        bool keyDown  = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        bool keyLeft  = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool keyRight = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))    latestVerIsUp    = true;
        if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))  latestVerIsUp    = false;
        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))  latestHorIsRight = false;
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)) latestHorIsRight = true;

        bool keySprint    = Input.GetKey(KeyCode.LeftShift);
        bool keySprintEnd = Input.GetKeyUp(KeyCode.LeftShift);

        bool keyLight     = Input.GetKeyDown("l");
        bool keyLightEnd  = Input.GetKeyUp("l");

        // determines direction based on what keys are pressed @TODO: decide what pressing left then right should result in and then write something that shouldnt be touched again
        bool isMovingDiagonally = false;

        float horDir = 0f;
        float verDir = 0f;

        if (keyUp)    verDir =  1f;
        if (keyDown)  verDir = -1f;
        if (keyRight) horDir =  1f;
        if (keyLeft)  horDir = -1f;
        if (keyDown && keyUp)    verDir = (latestVerIsUp) ? 1f : -1f;
        if (keyLeft && keyRight) horDir = (latestHorIsRight) ? 1f : -1f;

        if ((keyDown || keyUp || keyRight || keyLeft)
        &&  (verDir + horDir == 0f || verDir + horDir == 2f || verDir + horDir == -2f)) {
            horDir = horDir * 0.70710678118654f; // cos(45)
            verDir = verDir * 0.70710678118654f; // cos(45)
            isMovingDiagonally = true;
        }

        // moves the character in the determined direction, possibly sprint boosted

        float spdMultiplier = 1f;
        if (keySprintEnd) staminaRegenBool = true;

        if (keySprint && stamina >= staminaDrain && (keyDown || keyUp || keyRight || keyLeft)) {
            stamina         -= staminaDrain;
            spdMultiplier  = sprintBoost;
            staminaRegenBool = false;
        } else if (stamina <= staminaMax && staminaRegenBool) {
            if (stamina < 0) stamina = 0;
            stamina += staminaRegen;
            if (stamina > staminaMax) stamina = staminaMax;
        }

        rb.AddForce(new Vector2(horDir * speed * spdMultiplier, verDir * speed * spdMultiplier));

        xVelForDisplayPreCalc = rb.velocity.x;
        yVelForDisplayPreCalc = rb.velocity.y;

        if (isMovingDiagonally == false) {
            if (rb.velocity.x > (maxSpeed * spdMultiplier))       rb.velocity = new Vector2((maxSpeed * spdMultiplier), rb.velocity.y);
            else if (rb.velocity.x < -(maxSpeed * spdMultiplier)) rb.velocity = new Vector2(-(maxSpeed * spdMultiplier), rb.velocity.y);
            if (rb.velocity.y > (maxSpeed * spdMultiplier))       rb.velocity = new Vector2(rb.velocity.x, (maxSpeed * spdMultiplier));
            else if (rb.velocity.y < -(maxSpeed * spdMultiplier)) rb.velocity = new Vector2(rb.velocity.x, -(maxSpeed * spdMultiplier));
        } else {
            if (rb.velocity.x > (maxSpeed * 0.70710678118654f * spdMultiplier))       rb.velocity = new Vector2((maxSpeed * 0.70710678118654f * spdMultiplier), rb.velocity.y);
            else if (rb.velocity.x < -(maxSpeed * 0.70710678118654f * spdMultiplier)) rb.velocity = new Vector2(-(maxSpeed * 0.70710678118654f * spdMultiplier), rb.velocity.y);
            if (rb.velocity.y > (maxSpeed * 0.70710678118654f * spdMultiplier))       rb.velocity = new Vector2(rb.velocity.x, (maxSpeed * 0.70710678118654f * spdMultiplier));
            else if (rb.velocity.y < -(maxSpeed * 0.70710678118654f * spdMultiplier)) rb.velocity = new Vector2(rb.velocity.x, -(maxSpeed * 0.70710678118654f * spdMultiplier));            
        }

        xVelForDisplay = rb.velocity.x;
        yVelForDisplay = rb.velocity.y;

        // updates z coordinate to be in front / behind other objects
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // emits a light to activate the stone circles
        if (keyLight && emitLight == null) {
            emitLight = Instantiate(stoneLight) as GameObject;
            emitLight.transform.parent   = this.transform;
            emitLight.transform.position = this.transform.position;

        }
        if (keyLightEnd) {
            for (int i = 0; i < this.transform.childCount; i++) {
                child = this.transform.GetChild(i); // @TODO: i think theres a get all children function instead, is probably better to use as data is pulled from memory one time instead of x insteads this way (but maybe the compiler optimizes this for us) -Victor
                if (child.name == "PlayerLight(Clone)") {
                    Destroy(child.gameObject);
                }

            }
        }


    }


    
}
