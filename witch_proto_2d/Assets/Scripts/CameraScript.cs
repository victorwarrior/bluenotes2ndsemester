using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraScript : MonoBehaviour
{
    // references
    public GameObject player;
    public Camera cam;

    public float zoom = 1;

    void Start()
    {
        //zoom = GetComponent<Camera>().
        cam= GetComponent<Camera>();
        cam.orthographicSize = 16f;
    }

    void Update()
    {
        // following the player  
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -500);


        // @TODO: notice camera z = -500 and background z = 500, this could cause trouble when
        //        y gets < -500 or > 500... could you move the background and the camera too?

    }
    public void ZoomOut()
    {
        Debug.Log("Zooming out");
        StartCoroutine(CinematicView());
    }

    public void ZoomIn()
    {
        StopCoroutine(CinematicView());
        StartCoroutine(CinematicToNormalView());
    }
    public IEnumerator CinematicView()
    {
        for (float distance = cam.orthographicSize; distance <= 32f; distance += .1f)
        {
            zoom = distance;
            cam.orthographicSize = distance;
            yield return null;
        }

    }
    public IEnumerator CinematicToNormalView()
    {
        for (float distance = zoom; distance >= 16f; distance -= .1f)
        {
            zoom = distance;
            cam.orthographicSize = distance;
            yield return null;
        }

    }
}

