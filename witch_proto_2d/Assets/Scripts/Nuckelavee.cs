using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nuckelavee : MonoBehaviour
{

    // references
    public GameObject player;
    public GameObject enemy;
   // TrailRenderer mudTrail;
   // EdgeCollider2D mudCollider;
    Rigidbody2D rb;

    // constants
    float followSpeed = 0.12f;
    float dashSpeed = 0.3f;
    public float dashDistance = 11f;
    public float dashPositionScale = 1.1f;
    float dashAbilityCD = 10f;
    float callAbilityCD = 15;
    int enemySpawnNumber = 4;
    float spawnRange = 4f;

    // other
    Vector2 dashPosition;
    float spawnTime;
    float callAbilityTimer;
    float dashAbilityTimer;
    float distanceToPlayer;
    bool dashMode;


    void Start()
    {
        dashAbilityTimer = dashAbilityCD;
        callAbilityTimer = callAbilityCD;

       // mudTrail = this.GetComponent<TrailRenderer>();

        //GameObject colliderObject = new GameObject("TrailCollider", typeof(EdgeCollider2D));
        //mudCollider = colliderObject.GetComponent<EdgeCollider2D>();
       // mudCollider.isTrigger = true;
    }


    void FixedUpdate()
    {
        dashAbilityTimer = dashAbilityTimer - Time.deltaTime;
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // moves the Nuckelavee or initiates dash sequence
        if (dashAbilityTimer <= 0 && distanceToPlayer < dashDistance)
        {
            dashMode = true;
            dashPosition = player.transform.position * dashPositionScale;
        }
        else if (dashAbilityTimer > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
        }

        // executes dash sequence
        if (dashMode)
        {
            Vector2.MoveTowards(transform.position, dashPosition, dashSpeed);
            if (transform.position.x == dashPosition.x && transform.position.y == dashPosition.y)
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

        //Call method which creates trail collider
        //SetColliderPointsFromTrail(mudTrail, mudCollider);
    }


   /* void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        for (int position = 0; position < trail.positionCount; position++)
        {
            points.Add(trail.GetPosition(position));
        }
        collider.SetPoints(points);
    }*/
}
