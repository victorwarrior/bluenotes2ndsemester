using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLight : MonoBehaviour
{

    private Object Light;

    Transform child;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Updates 60 times pr. second
    private void FixedUpdate()
    {
        Light = Resources.Load("StoneLight", typeof(GameObject));
    }

    // Emits light if touched by light
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "CircleLight")
        {
            GameObject EmitLight = Instantiate(Light) as GameObject;
            EmitLight.transform.parent = this.transform;
            EmitLight.transform.position = this.transform.position;
        }
    }


}
