using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class aggroRange : MonoBehaviour
{
    //Constancs
    float aggro   = 11.5f;
    float deaggro = 15.5f;

    //Variables
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemy = col.gameObject;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position, aggro);

            if (hit.collider != null)
            {
                Debug.Log("The ray is hitting an " + hit.collider.tag);
            }

        }
    }
}
