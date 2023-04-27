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

    // constants
    float followSpeed = 0.12f;
    float dashSpeed = 0.3f;
    public float dashDistance = 15f;
    public float dashPositionScale = 1.1f;
    float dashAbilityCD = 10f;
    float callAbilityCD = 15;
    int enemySpawnNumber = 4;
    float spawnRange = 4f;
    float speedCoefficient;
    float speedCoefficientMod = 40f;

    // other
    Vector2 dashPosition;
    float trailInstantiationMeasure;
    float initialInstantiationMeasure = 4f;
    float callAbilityTimer;
    float dashAbilityTimer;
    float distanceToPlayer;
    bool dashMode;
    bool leaveTrail = true;

    void Start()
    {
        dashMode = false; // this is so that everything that uses the truth value of dash has an initial configuration.

        dashAbilityTimer          = dashAbilityCD;
        callAbilityTimer          = callAbilityCD;
        trailInstantiationMeasure = initialInstantiationMeasure;
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
                Debug.Log("dash done");
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

        if (leaveTrail)
        {
            trailInstantiationMeasure = trailInstantiationMeasure - Time.deltaTime * speedCoefficient;
            Debug.Log("SpeedCoefficient is "+speedCoefficient);
            
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
                Instantiate(mud, transform.position, transform.rotation); //instantiates trail.
                trailInstantiationMeasure = initialInstantiationMeasure; // resets timer for trail instantiation.
                
            }
        }
    }


}
