using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteDialogue : MonoBehaviour
{
    // Start is called before the first frame update

    //References
    public GameObject mainController;
    public GameObject burnTheKelpDialogue;
    public MainController mainScript;

    public SpriteRenderer editorSprite;

    void Start()
    {
        mainScript = mainController.GetComponent<MainController>();

        SpriteRenderer[] spritesInChildren = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in spritesInChildren) {
            editorSprite = GetComponentsInChildren<SpriteRenderer>()[0];            
        }
        if (editorSprite != null) editorSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "DialogueFirstTime")
            {
                mainScript.StartDialogue(mainScript.introDialogue, 0, 0);
                Destroy(this);

            }
            else if (this.gameObject.name == "DialogueBoxInfo")
            {
                mainScript.StartDialogue(mainScript.boxDialogue, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "DialogueBridge")
            {
                mainScript.StartDialogue(mainScript.bridgeDialogue, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "DialogueEnemyEncounter")
            {
                mainScript.StartDialogue(mainScript.enemyEncounterDialogue, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "DialogueEnterFarm")
            {
                mainScript.StartDialogue(mainScript.enterFarm, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "LabyrinthDialogue")
            {
                mainScript.StartDialogue(mainScript.labyrinthDialogue, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "Collectable")
            {
                mainScript.StartDialogue(mainScript.testPickupDialogue, 0, 0);
                burnTheKelpDialogue.gameObject.SetActive(true);
                Destroy(this);
            }
            else if (this.gameObject.name == "VictoryDialogue")
            {
                mainScript.StartDialogue(mainScript.beachDialogue, 0, 0);
                Destroy(this);
            }
            else if (this.gameObject.name == "DialogueBurnTheKelp")
            {
                mainScript.StartDialogue(mainScript.beachDialogue, 0, 0);
                Destroy(this);
            }


        }
    }
}
