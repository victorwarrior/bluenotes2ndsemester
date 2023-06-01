using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    public GameObject playerCamera;

    float radius;
    Vector2 cameraPosition2D;
    Vector2 myPosition2D;
    // Start is called before the first frame update
    void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius;

    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition2D = new Vector2(playerCamera.transform.position.x, playerCamera.transform.position.y);
        myPosition2D = new Vector2(transform.position.x, transform.position.y);

        float playerDistance = Vector2.Distance(cameraPosition2D, myPosition2D);
        //Debug.Log(playerCamera.GetComponent<Camera>().myPositionZ);

        if (playerDistance < radius)
        {
            Debug.Log(playerCamera.name);
            playerCamera.GetComponent<CameraScript>().ZoomOut();
        }
        else if (playerDistance > radius && playerCamera.GetComponent<CameraScript>().cam.orthographicSize >= 16)
        {
            playerCamera.GetComponent<CameraScript>().ZoomIn();
        }
    }
}
