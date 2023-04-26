using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mist : MonoBehaviour {

    // references
    public GameObject graymanPrefab;
    public GameObject player;

    // other
    int dir = 1;
    float speed = 0.2f;

    void Start() {
        //dir = (Random.Range(0, 2) ? 1 : -1;
    }

    void FixedUpdate() {
        transform.Translate(speed * new Vector3(dir, 0, transform.position.z));
    }
}
