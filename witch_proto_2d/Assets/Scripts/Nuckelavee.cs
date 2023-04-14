using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuckelavee : MonoBehaviour
{
    //references
    GameObject player;
    GameObject enemy;
    Rigidbody2D rb;

    //vectors
    private Vector2 dashPosition;

    private float spawnTime;
    private float timeSinceSpawn;
    private float enemyCallTime;
    private float dashTime;
    private float dashSpeed;
    private float distanceToPlayer;
    private float followSpeed;

    public float dashDistance;
    public float dashPositionScale;

    private bool dashMode;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    
    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //Moves the Nuckelavee or initiates dash sequence
        if (timeSinceSpawn >= dashTime && distanceToPlayer < dashDistance)
        {
            dashMode = true;
            dashPosition = player.transform.position*dashPositionScale;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followSpeed);
        }
        
        //Executes dash sequence.
        if (dashMode)
        {
            Vector2.MoveTowards(transform.position, dashPosition, dashSpeed);
        }
    }

    void Update()
    {
        timeSinceSpawn = Time.time - spawnTime;

        if(timeSinceSpawn >= enemyCallTime)
        {

        }
    }
}
