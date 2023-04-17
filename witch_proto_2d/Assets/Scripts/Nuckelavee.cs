using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nuckelavee : MonoBehaviour
{
    //references
   public GameObject player;
   public GameObject enemy;
          TrailRenderer mudTrail;
          EdgeCollider2D mudCollider;
          Rigidbody2D rb;

    //vectors
    private Vector2 dashPosition;
    private Vector2 enemySpawnPosition;

    private float spawnTime;
    private float enemyCallTime;
    private float enemyCallCD = 15;
    private float enemyCallCDAdd = 15;
    private int   enemySpawnNumber = 4;

    private float dashTime;
    private float dashCD = 10f;
    private float dashCDAdd = 10f;
    private float dashSpeed = 0.3f;

    private float distanceToPlayer;
    private float followSpeed = 0.12f;

    public float dashDistance = 11f;
    public float dashPositionScale = 1.1f;
    public float spawnRangeMaxX = 30f;
    public float spawnRangeMaxY = 30f;
    public float spawnRangeMinX = 15f;
    public float spawnRangeMinY = 15f;

    private bool dashMode;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime      = Time.time;
        enemyCallTime  = spawnTime;

        mudTrail = this.GetComponent<TrailRenderer>();

        GameObject colliderObject = new GameObject("TrailCollider", typeof(EdgeCollider2D));
        mudCollider = colliderObject.GetComponent<EdgeCollider2D>();
        mudCollider.isTrigger = true;
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
        else if (dashTime < dashCD)
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

        //Call method which creates trail collider
        SetColliderPointsFromTrail(mudTrail, mudCollider);
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

    void SetColliderPointsFromTrail(TrailRenderer trail, EdgeCollider2D collider)
    {
        List<Vector2> points = new List<Vector2>();
        for(int position = 0; position < trail.positionCount; position++)
        {
            points.Add(trail.GetPosition(position));
        }
        collider.SetPoints(points);
    }
}
