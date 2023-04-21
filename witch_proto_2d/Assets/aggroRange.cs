using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class aggroRange : MonoBehaviour
{
    //Constancs
    float aggro    = 11.5f;
    float deaggro  = 15.5f;
    Vector2 origin;

    //Variables
    GameObject enemy;
    public bool attacking;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
       

        if (col.gameObject.tag == "Enemy")
        {
            enemy = col.gameObject;
            //Debug.DrawLine(this.transform.position, enemy.transform.position, Color.green, 10.0f, false);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, (enemy.transform.position - player.transform.position).normalized, Mathf.Infinity);
            Debug.DrawRay(player.transform.position, (enemy.transform.position - player.transform.position).normalized, Color.green, 10.0f, false);

            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("The ray is hitting an " + hit.collider.gameObject.tag);
                //Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.green, 100.0f, false);
                enemy.GetComponent<Enemy>().isAttacking = true;
                attacking = true;
            }
            


        }
    }
}
