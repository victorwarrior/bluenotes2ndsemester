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

    //position and scale variables

    BoxCollider2D colliderComponent;
    Vector3 colliderSize;
    Vector2 movementDirection;

    //void OnCollisionEnter2D(Collision2D col)
    private void Start()
    {

        //ContactPoint2D contact = col.GetContact(0);
        //switch (contact)
        colliderComponent = GetComponent<BoxCollider2D>();
        colliderSize = colliderComponent.bounds.size;

        rb = GetComponent<Rigidbody2D>();
        pushSpeed = 1f;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        rbPlayer = Player.GetComponent<Rigidbody2D>();
        foreach (ContactPoint2D conts in col.contacts)
        {


            Vector2 contPosition = conts.point;
            if (contPosition.y - (colliderSize.y / 2) >= 0 && Input.GetKey("s") && Input.GetKey("g"))
            {
                movementDirection = new Vector2(0, -1);
                rb.velocity = movementDirection * pushSpeed;
                rbPlayer.velocity = movementDirection * pushSpeed;
                Debug.Log("pushing down");
            }
            else if (contPosition.y + (colliderSize.y / 2) <= 0 && Input.GetKey("w") && Input.GetKey("g"))
            {
                movementDirection = new Vector2(0, 1);
                rb.velocity = movementDirection * pushSpeed;
                rbPlayer.velocity = movementDirection * pushSpeed;
            }
            else if (contPosition.x - (colliderSize.x / 2) >= 0 && Input.GetKey("a") && Input.GetKey("g"))
            {
                movementDirection = new Vector2(-1, 0);
                rb.velocity = movementDirection * pushSpeed;
                rbPlayer.velocity = movementDirection * pushSpeed;
            }
            else if (contPosition.y + (colliderSize.y / 2) <= 0 && Input.GetKey("d") && Input.GetKey("g"))
            {
                movementDirection = new Vector2(1, 0);
                rb.velocity = movementDirection * pushSpeed;
                rbPlayer.velocity = movementDirection * pushSpeed;
            }
            else if (!Input.GetKey("g"))
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        rb.velocity = new Vector2(0, 0);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}

