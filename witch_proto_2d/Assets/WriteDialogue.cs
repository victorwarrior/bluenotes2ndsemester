using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteDialogue : MonoBehaviour
{
    // Start is called before the first frame update

    //References
    public GameObject mainController;
    public MainController mainScript;

    void Start()
    {
        mainScript = mainController.GetComponent<MainController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            mainScript.StartDialogue(mainScript.testDialogue, 0, 0);
        }
    }
}
