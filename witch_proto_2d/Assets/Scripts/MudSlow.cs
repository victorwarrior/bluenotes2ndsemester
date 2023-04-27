using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSlow : MonoBehaviour
{
    float spawnTime;
    float timeActive;
    public float fadeOutTime = 3f;
    public float waitingTime = 0.1f;

    public GameObject player;
    Player playerScript;

    public void Start()
    {
        player       = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        spawnTime    = Time.time;
    }

    public void FixedUpdate()
    {
        timeActive = Time.time - spawnTime;
        if (timeActive > fadeOutTime)
        {
            StartCoroutine(Fade());
        }
        if (GetComponent<Renderer>().material.color.a <= 0.1)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Fade()
    {
        //Color c = renderer.material.color;
        Color c = GetComponent<Renderer>().material.color;
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            c.a = alpha;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(waitingTime);
        }
    }

    // CHeck if it is player in its trigger. Call method from player
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Mud object: " + collision.gameObject);


            //maxSpeed = maxSpeed * slowMultiplier;
            //speed    = speed * slowMultiplier;
            playerScript.Slow();
            Debug.Log("Is slowed");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // maxSpeed = maxSpeed / slowMultiplier;
            // speed = speed / slowMultiplier;
            playerScript.UnSlow();
        }
        // CHeck if it is player in its trigger. Call method from player
    }
}
