using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AggroRange : MonoBehaviour {

    // references
           GameObject enemy;
    public GameObject player;


    void OnTriggerStay2D(Collider2D col) {
       

        if (col.gameObject.tag == "Enemy") {
            enemy = col.gameObject;
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position,
                                                 (enemy.transform.position - player.transform.position).normalized,
                                                 Mathf.Infinity);
            if (hit.collider.gameObject.tag == "Enemy") enemy.GetComponent<Enemy>().isAttacking = true;
        }
    }
}
