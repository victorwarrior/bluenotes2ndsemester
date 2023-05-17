using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManagerScript : MonoBehaviour
{
    public GameObject[] checkPoints;
    private static CheckPointManagerScript instance;

    public static Vector3 currentCheckpoint;

    public static bool hasCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        hasCheckPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
