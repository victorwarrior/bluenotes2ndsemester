using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuckelavee : MonoBehaviour
{
    //references
   public GameObject player;
   public GameObject enemy;
          Rigidbody2D rb;

    //vectors
    private Vector2 dashPosition;
    private Vector2 enemySpawnPosition;

    private float spawnTime;
    private float enemyCallTime;
    private float enemyCallCD;
    private float enemyCallCDAdd;
    private int   enemySpawnNumber;

    private float dashTime;
    private float dashCD;
    private float dashCDAdd;
    private float dashSpeed;

    private float distanceToPlayer;
    private float followSpeed;

    public float dashDistance;
    public float dashPositionScale;
    public float spawnRangeMaxX;
    public float spawnRangeMaxY;
    public float spawnRangeMinX;
    public float spawnRangeMinY;

    private bool dashMode;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime      = Time.time;
        
        enemyCallTime  = spawnTime;
    }

    
    void FixedUpdate()
    {
        dashTime = Time.time - spawnTime;
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //Moves the Nuckelavee or initiates dash sequence
        if (dashTime >= dashCD && distanceToPlayer < dashDistance)
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
            if(transform.position.x == dashPosition.x && transform.position.y == dashPosition.y)
            {
                dashMode = false;
                dashCD = dashTime + dashCDAdd;
            }
        }
    }

    void Update()
    {
       
       enemyCallTime = Time.time - spawnTime;

        if(enemyCallTime >= enemyCallCD)
        {
            for(int i = 0; i < enemySpawnNumber; i++)
            {
                enemySpawnPosition = new Vector2(Random.Range(spawnRangeMinX, spawnRangeMaxX), Random.Range(spawnRangeMinY, spawnRangeMaxY));
                Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
            }
            enemyCallCD = enemyCallTime + enemyCallCDAdd;
        }
    }

}
