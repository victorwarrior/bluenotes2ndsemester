using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PushObstacle : MonoBehaviour
{
    public float pushSpeed;
    public GameObject Player;
    Rigidbody2D rb;
    Rigidbody2D rbPlayer;

    public AudioSource Audiosource;
    public AudioClip   scrape;

    //position and scale variables

    BoxCollider2D colliderComponent;
    Vector3 colliderSize;
    Vector2 movementDirection;

    //void OnCollisionEnter2D(Collision2D col)
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //ContactPoint2D contact = col.GetContact(0);
        //switch (contact)
        colliderComponent = GetComponent<BoxCollider2D>();
        colliderSize = colliderComponent.bounds.size;

        rbPlayer = Player.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
        //pushSpeed = 1f;

        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

    }



    void OnCollisionStay2D(Collision2D col) {

        foreach (ContactPoint2D conts in col.contacts) {

            if (col.gameObject.tag == "Player") {
                Vector2 contPosition     = conts.point;
                Vector2 relativePosition = new Vector2(contPosition.x - transform.position.x, contPosition.y - transform.position.y);
                
                if (Input.GetKey("g")) {
                    if (contPosition.y - transform.position.y - (colliderSize.y / 2) >= 0
                    &&  (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))) {
                        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                        movementDirection = new Vector2(0, -1);
                        rb.velocity       = movementDirection * pushSpeed;
                        rbPlayer.velocity = movementDirection * pushSpeed;
                        LoopScrape();
                    } else if (contPosition.y - transform.position.y + (colliderSize.y / 2) <= 0
                           &&  (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))) {
                        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                        movementDirection = new Vector2(0, 1);
                        rb.velocity       = movementDirection * pushSpeed;
                        rbPlayer.velocity = movementDirection * pushSpeed;
                        LoopScrape();
                    } else if (contPosition.x - transform.position.x - (colliderSize.x / 2) >= 0
                           &&  (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))) {
                        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                        movementDirection = new Vector2(-1, 0);
                        rb.velocity       = movementDirection * pushSpeed;
                        rbPlayer.velocity = movementDirection * pushSpeed;
                        LoopScrape();
                    }
                    else if (contPosition.x - transform.position.x + (colliderSize.x / 2) <= 0
                         &&  (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))) {
                        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                        movementDirection = new Vector2(1, 0);
                        rb.velocity       = movementDirection * pushSpeed;
                        rbPlayer.velocity = movementDirection * pushSpeed;
                        LoopScrape();
                    }
                
                } else {
                    rb.velocity = new Vector2(0, 0);
                    Audiosource.Stop();                    
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        rb.velocity = new Vector2(0, 0);
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void LoopScrape() {
        Audiosource.clip   = scrape;
        Audiosource.loop   = false;
        Audiosource.volume = 0.34f;
        
        if (Audiosource.isPlaying == false) {
            Audiosource.PlayOneShot(scrape, 0.4f);
        }
    }
}

