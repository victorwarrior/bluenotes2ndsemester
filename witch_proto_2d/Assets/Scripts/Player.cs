using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    // debug
    //float xVelForDisplay        = 0f;
    //float xVelForDisplayPreCalc = 0f;
    //float yVelForDisplay        = 0f;
    //float yVelForDisplayPreCalc = 0f;

    // references
    Rigidbody2D rb;
    Animator    animator;
    Object      stoneLight;
    GameObject  emitLight;
    
    public Transform   ColliderTransform;


    // references (set in editor)
    public Image    staminaBar;
    public Image    hpBar;
    public Gradient hpGradient;
    public Gradient staminaGradient;

    // constants
    public const float speed            = 70.2f; // how fast you accelerate, rigidbody velocity is the real measurement of speed
    public const float maxSpeed         = 7.6f;  // top speed
    public const float sprintMultiplier = 1.75f; // speed * sprintMultiplier = sprinting speed, max speed * sprintMultiplier = top sprinting speed 
    public const float mudMultiplier    = 0.6f;  // to slow down the player when in the nuckelavee mud trail.
    public const float friction         = 6.5f;  // used to make the character slow down, so it eventually stands still no keys are being pressed

    public const float hpMax            = 100;
    public const int staminaMax         = 1000;       
    public const int staminaMin         = 100;   // can't sprint if below this unless you're already sprinting
    public const int staminaDrain       = 8;     // how much stamina is used per tick when sprinting
    public const int staminaRegen       = 1;     // how much stamina is gained when not sprinting

    // other
    public float hp             = 100;
    public int stamina          = 1000;
    public float spdMultiplier  = 1f;
    public float slowMultiplier = 1f;
   
    bool keyUp                  = false;
    bool keyDown                = false;
    bool keyLeft                = false;
    bool keyRight               = false;
    bool keySprint              = false;
    bool keyLight               = false;
    bool keyLightEnd            = false;
      
    bool staminaRegenBool       = false;
    bool staminaExhausted       = false;
    bool latestVerIsUp          = false;
    bool latestHorIsRight       = false;

    string latestRelease        = "down";



    void Start() {
        rb         = GetComponent<Rigidbody2D>();
        rb.drag    = friction;
        stoneLight = Resources.Load("PlayerLight", typeof(GameObject));
        animator   = GetComponent<Animator>();

        staminaBar.color = staminaGradient.Evaluate(1f);
        hpBar.color      = hpGradient.Evaluate(1f);
       // Collider soundArea = ColliderTransform.GetChild(10).GetComponent<CircleCollider2D>;
    }

    private void Awake() {
        if (CheckPointManagerScript.hasCheckPoint) {
            transform.position = CheckPointManagerScript.currentCheckpoint;
        }
    }

    void Update() {

        // checks and stores what keys are pressed
        keyUp    = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        keyDown  = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        keyLeft  = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        keyRight = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))    latestVerIsUp    = true;
        if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))  latestVerIsUp    = false;
        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))  latestHorIsRight = false;
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow)) latestHorIsRight = true;
        if (Input.GetKeyUp("w") || Input.GetKeyUp(KeyCode.UpArrow))    latestRelease = "up";
        if (Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.DownArrow))  latestRelease = "down";
        if (Input.GetKeyUp("a") || Input.GetKeyUp(KeyCode.LeftArrow))  latestRelease = "left";
        if (Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.RightArrow)) latestRelease = "right";
        keySprint   = Input.GetKey(KeyCode.LeftShift);
        keyLight    = Input.GetKeyDown("l");
        keyLightEnd = Input.GetKeyUp("l");
        
    }


    void FixedUpdate() {

        // determines direction based on what keys are pressed
        bool isMovingDiagonally = false;

        float horDir = 0f;
        float verDir = 0f;

        if (keyUp)    verDir =  1f;
        if (keyDown)  verDir = -1f;
        if (keyRight) horDir =  1f;
        if (keyLeft)  horDir = -1f;
        if (keyDown && keyUp)    verDir = (latestVerIsUp)    ? 1f : -1f;
        if (keyLeft && keyRight) horDir = (latestHorIsRight) ? 1f : -1f;

        if ((keyDown || keyUp || keyRight || keyLeft)
        &&  (verDir + horDir == 0f || verDir + horDir == 2f || verDir + horDir == -2f)) {
            horDir = horDir * 0.70710678118654f; // cos(45)
            verDir = verDir * 0.70710678118654f; // cos(45)
            isMovingDiagonally = true;
        }

        // moves the character in the determined direction, possibly sprint boosted
        spdMultiplier    = 1f;
        staminaRegenBool = (stamina == staminaMax) ? false : true;

        if (staminaExhausted) staminaExhausted = (stamina >= staminaMin) ? false : true;
        
        if (keySprint && stamina >= staminaDrain && (keyDown || keyUp || keyRight || keyLeft) && staminaExhausted == false) {
            stamina               = stamina - staminaDrain;
            spdMultiplier         = sprintMultiplier;
            staminaRegenBool      = false;
            stamina               = (stamina <= 0) ? 0 : stamina;
            staminaExhausted      = (stamina <= 0) ? true : false;
            staminaBar.fillAmount = (float) stamina / (float) staminaMax;
            staminaBar.color      = staminaGradient.Evaluate((float) stamina / (float) staminaMax);
            }

        if (staminaRegenBool) {
            stamina               = (stamina > staminaMax) ? staminaMax : stamina + staminaRegen;
            staminaBar.fillAmount = (float) stamina / (float) staminaMax;
            staminaBar.color      = staminaGradient.Evaluate((float) stamina / (float) staminaMax);
        }

        rb.AddForce(new Vector2(horDir * speed * spdMultiplier, verDir * speed * spdMultiplier));

        if (isMovingDiagonally == false) {
            if      (rb.velocity.x > (maxSpeed * spdMultiplier * slowMultiplier))  rb.velocity = new Vector2((maxSpeed * spdMultiplier * slowMultiplier), rb.velocity.y);
            else if (rb.velocity.x < -(maxSpeed * spdMultiplier * slowMultiplier)) rb.velocity = new Vector2(-(maxSpeed * spdMultiplier * slowMultiplier), rb.velocity.y);
            if      (rb.velocity.y > (maxSpeed * spdMultiplier * slowMultiplier))  rb.velocity = new Vector2(rb.velocity.x, (maxSpeed * spdMultiplier * slowMultiplier));
            else if (rb.velocity.y < -(maxSpeed * spdMultiplier * slowMultiplier)) rb.velocity = new Vector2(rb.velocity.x, -(maxSpeed * spdMultiplier * slowMultiplier));
        } else {
            if      (rb.velocity.x > (maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier))  rb.velocity = new Vector2((maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier), rb.velocity.y);
            else if (rb.velocity.x < -(maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier)) rb.velocity = new Vector2(-(maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier), rb.velocity.y);
            if      (rb.velocity.y > (maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier))  rb.velocity = new Vector2(rb.velocity.x, (maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier));
            else if (rb.velocity.y < -(maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier)) rb.velocity = new Vector2(rb.velocity.x, -(maxSpeed * 0.70710678118654f * spdMultiplier * slowMultiplier));            
        }

        // animation
        float xScale = 1f;

        if (horDir != 0f || verDir != 0f) {
            if (horDir < 0f) xScale = -1f;
            if (horDir > 0f) xScale =  1f;
            if (spdMultiplier <= 1f) animator.SetFloat("SPEED", 1f);
            if (spdMultiplier >  1f) animator.SetFloat("SPEED", 2f);
            animator.SetFloat("VER", verDir);
            animator.SetFloat("HOR", horDir);
        } else {
            animator.SetFloat("SPEED", 0f);
            if (latestRelease == "up")    {animator.SetFloat("VER",   1f); animator.SetFloat("HOR", 0.5f); xScale =  1f;}
            if (latestRelease == "down")  {animator.SetFloat("VER",  -1f); animator.SetFloat("HOR", 0.5f); xScale =  1f;}
            if (latestRelease == "left")  {animator.SetFloat("VER", 0.5f); animator.SetFloat("HOR",  -1f); xScale = -1f;}
            if (latestRelease == "right") {animator.SetFloat("VER", 0.5f); animator.SetFloat("HOR",   1f); xScale =  1f;}
        }

        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);



        // update hp
        if (hp < hpMax) {
            hp += 0.005f; // @TODO: this is hp regen, it should be a variable
        } else {
            hp = hpMax;
        }

        hpBar.fillAmount = hp / hpMax;
        hpBar.color      = hpGradient.Evaluate(hp / hpMax);
        if (hp <= 0f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
                Transform child = this.transform.GetChild(i); // @TODO: i think theres a get all children function instead, is probably better to use as data is pulled from memory one time instead of x insteads this way (but maybe the compiler optimizes this for us) -Victor
                if (child.name == "PlayerLight(Clone)") {
                    Destroy(child.gameObject);
                }
            }
        }

    }



}
