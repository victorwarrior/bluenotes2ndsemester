using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nuckelavee : MonoBehaviour
{

    // references
    public GameObject player;
    public GameObject enemy;
    public GameObject mud;
    Rigidbody2D rb;
    Player      playerScript;

    // constants
    float followSpeed              = 0.12f;        //Speed with which the nuckelavee follows the player.
    float dashSpeed                = 0.3f;        //Speed with which the nuckelavee dashes towards the player
    public float dashDistance      = 15f;        //Distance from which the nuckelavee can dash
    public float dashPositionScale = 1.1f;      //Scale that determines the distance which is dashed relative to the distance from which the nuckelavee can dash
    float dashAbilityCD            = 10f;      //Cooldown for dash
    float callAbilityCD            = 15;      //Cooldown for calling standard enemies ability
    int enemySpawnNumber           = 4;      //Number of standard enemies called when call ability is used
    float spawnRange               = 4f;    //The distance from the nuckelavee the called enemies can have
    float speedCoefficient;                //Variable which changes depending on whether nuck is dashing or no, and which changes the speed with which trail is left 
    float speedCoefficientMod      = 40f; //Variable which modifies the speedcoefficient
    int   positionSaverInt;              //Int that changes every frame so that the position of the nuckelavee can be saved at different frames
    public int damage;                  //Damage received from nuck
    public int damageInterval;         //Interval between every damage-tick

    // other
    Vector2 dashPosition;                           //The position towards which the nuckelavee will dash
    Vector2 trailPosition;                         //The position at which the trail is to be instantiated
    Vector2 nuckPosition1;                        //Position of Nuckelavee at uneven frames
    Vector2 nuckPosition2;                       //Position of Nuckelavee at  even frames
    Vector2 nuckPosDif;                         //The difference in position one frame to the other
    float trailInstantiationMeasure;           //The scaled time until a trail is instantiated
    float initialInstantiationMeasure = 2f;   //The initial scaled time until a trail is instantiated
    float callAbilityTimer;                  //The time until call ability is used
    float dashAbilityTimer;                 //The time until the dash ability is used
    float distanceToPlayer;                //The distance relative to the player used to determine whether dash can be used
    float trailOffset = 2f;               //The offset of the trail from the center of the nuckelavee
    float trailOffsetMod;                //Variable to determine whether the offset of the trail is positive or negative
    bool dashMode;                      //Boolean to start the dash sequence
    bool leaveTrail = true;            //Boolean to be asked before trail is instantiated

    void Start()
    {
        playerScript = player.GetComponent<Player>();

        dashMode = false; //This is so that everything that uses the truth value of dash has an initial configuration.

        dashAbilityTimer          = dashAbilityCD;
        callAbilityTimer          = callAbilityCD;
        trailInstantiationMeasure = initialInstantiationMeasure;
        trailOffsetMod            = 1f;
        positionSaverInt          = 0;
    }


    void FixedUpdate()
    {
        dashAbilityTimer = dashAbilityTimer - Time.deltaTime;
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // moves the Nuckelavee or initiates dash sequence
        if (dashAbilityTimer <= 0 && distanceToPlayer < dashDistance)
        {
            if (!dashMode)
            {
                dashPosition = (new Vector2(player.transform.position.x, player.transform.position.y)
              - new Vector2(transform.position.x, transform.position.y)) * dashPositionScale;
            }
            dashMode = true;
          
        }
        else if (!dashMode)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
        }

        // executes dash sequence
        if (dashMode)
        {
            transform.Translate(dashPosition.normalized * dashSpeed);
            //if (transform.position.x == dashPosition.x && transform.position.y == dashPosition.y)
            if(dashAbilityTimer<-1.5)
            {
                dashMode = false;
                dashAbilityTimer = dashAbilityCD;
            }
        }

        callAbilityTimer = callAbilityTimer - Time.deltaTime;

        if (callAbilityTimer <= 0f)
        {
            for (int i = 0; i < enemySpawnNumber; i++)
            {
                Vector2 enemySpawnPosition = new Vector2(transform.position.x + Random.Range(-spawnRange, spawnRange),
                                                         transform.position.y + Random.Range(-spawnRange, spawnRange));
                Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
            }
            callAbilityTimer = callAbilityCD;
        }

        if (positionSaverInt == 0)
        {
            nuckPosition1 = transform.position;
            positionSaverInt = 1;
        }

        else if (positionSaverInt == 1)
        {
            nuckPosition2 = transform.position;
            positionSaverInt = 0;
        }


        if (positionSaverInt == 0)
        {
            nuckPosDif = (nuckPosition2 - nuckPosition1).normalized;
        }

        else if (positionSaverInt == 1)
        {
            nuckPosDif = (nuckPosition1 - nuckPosition2).normalized;
        }

        if (leaveTrail)
        {
            trailInstantiationMeasure = trailInstantiationMeasure - Time.deltaTime * speedCoefficient;
            
            if (dashMode)
            {
                speedCoefficient = dashSpeed * speedCoefficientMod;
            }
            else if (!dashMode)
            {
                speedCoefficient = followSpeed * speedCoefficientMod;
            }

            if(trailInstantiationMeasure <= 0f)
            {
                if (trailOffsetMod == 1)
                {

                    trailPosition = new Vector2(transform.position.x + trailOffset  * nuckPosDif.y * trailOffsetMod, transform.position.y + trailOffset * nuckPosDif.x * trailOffsetMod);
                    Instantiate(mud, trailPosition, transform.rotation); //instantiates trail.
                    trailInstantiationMeasure = initialInstantiationMeasure; // resets timer for trail instantiation.
                    trailOffsetMod = -1f;
                }

                else if (trailOffsetMod == -1)
                {

                    trailPosition = new Vector2(transform.position.x + trailOffset  * nuckPosDif.y * trailOffsetMod, transform.position.y + trailOffset * nuckPosDif.x * trailOffsetMod);
                    Instantiate(mud, trailPosition, transform.rotation); //instantiates trail.
                    trailInstantiationMeasure = initialInstantiationMeasure; // resets timer for trail instantiation.
                    trailOffsetMod = 1f;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InvokeRepeating("TakeDamage", 0f, damageInterval);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CancelInvoke("TakeDamage");
        }
    }

    void TakeDamage()
    {
        playerScript.hp -= damage;
    }

}
