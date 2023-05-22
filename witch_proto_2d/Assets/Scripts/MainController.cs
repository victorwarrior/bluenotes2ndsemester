using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainController : MonoBehaviour {
    
    // references
    public GameObject      player;
    public GameObject      enemyPrefab;
    public GameObject      graymanPrefab;
    public GameObject      simpleMistPrefab;
    public GameObject      mapImage;

    public GameObject      dialogueParent;
    //public GameObject      dialogueNameTag;
    //public GameObject      dialogueCharacterNameObject;
    public TextMeshProUGUI dialogueCharacterName;
    public TextMeshProUGUI dialogue;

    public GameObject      bridgetIcon;
    public GameObject      catIcon;

    public Image[]         allEyes   = new Image[5];
    public Image[]         allMouths = new Image[5];

    Dictionary<string, int> eyeDictionary = new Dictionary<string, int>()   {{"angry", 0}, {"happy", 1}, {"iffy", 2}, {"relaxed", 3}, {"wide", 4}    };
    Dictionary<string, int> mouthDictionary = new Dictionary<string, int>() {{"excited", 0}, {"happy", 1}, {"relaxed", 2}, {"sad", 3}, {"worried", 4}};


    // constants
    public const int numberOfEnemies = 0;

    // other
    bool  mapOnScreen   = false;
    float mistTimer     = 0f;
    float dialogueTimer = 0f;
    int   nextDialogue  = -1;

    Dialogue[] testDialogue = new Dialogue[4];

    void Start() {
        if (SceneManager.GetActiveScene().name == "MistTest") {
            mistTimer = 1f;
        }
        
        int n = 0;
        dialogueParent.SetActive(false);

        testDialogue[n++] = new Dialogue("Bridget",
                                         "Hi, this is Bridget talking! This text is maybe a bit boring... Ha ha ha!",
                                         "happy", "relaxed", 4f, 0, 1);
        testDialogue[n++] = new Dialogue("CAT",
                                         "...well, THIS text is on the screen for a long time...",
                                         "happy", "happy", 8f, 1, 2);
        testDialogue[n++] = new Dialogue("CAT",
                                         "short exclamation!",
                                         "happy", "happy", 1.5f, 2, 3);
        testDialogue[n++] = new Dialogue("Bridget",
                                         "Right... i guess this is the last line of dialogue, maybe i should make it a bit longer than the other lines - to end it on a high note, you know?",
                                         "relaxed", "happy", 6.2f, 3, -1);

        nextDialogue = 0;
        dialogueTimer = 2f;

    }

    void Update() {

        // controls
        if (Input.GetKeyDown("m")) {
            mapOnScreen = !mapOnScreen;
            mapImage.SetActive(mapOnScreen);
        }
        
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown("escape")) {
            // add a menu? 
        }



    }

    void FixedUpdate() {

        mistTimer = mistTimer - Time.deltaTime; // @TODO: should timers only go down to 0 and then stop? if they run for long enough they become positive again right? Oo -Victor

        // mist & graymen
        if (mistTimer <= 0f) mistTimer = 0f; // @TODO: resetting doesn't work right now because it runs this all the time :) - Victor
        if (SceneManager.GetActiveScene().name == "MistTest" && mistTimer <= 0f) {

            if (Random.Range(0, 100) < 33) {
                int n = 2;
                for (int i = 0; i < n; i++) {
                    GameObject inst = Instantiate(simpleMistPrefab,
                                                  new Vector3(player.transform.position.x + Random.Range(-36f, 36f),
                                                              player.transform.position.y + Random.Range(-36f, 36f),
                                                              player.transform.position.z + 100),
                                                  Quaternion.identity);
                }
            }
 
            if (Random.Range(0, 100) < 16) {
                GameObject inst = Instantiate(graymanPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-32f, 32f),
                                                          player.transform.position.y + Random.Range(-32f, 32f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity); // right now it figures out a random position on its own
                inst.GetComponent<Grayman>().player = player;
            }

            if (mistTimer <= -30f) {
                mistTimer = Random.Range(30f, 120f);
                Debug.Log("mistTimer set to " + mistTimer);
            }

        }

        // dialogue
        
        if (dialogueTimer >= 0f) {
            dialogueTimer = dialogueTimer - Time.deltaTime;

            if (dialogueTimer <= 0f && dialogueCharacterName != null && dialogue != null) {
                if (nextDialogue != -1) {
                    dialogueParent.SetActive(true);
                    if (testDialogue[nextDialogue].characterTalking == "Bridget" && bridgetIcon != null && catIcon != null) {
                        bridgetIcon.SetActive(true);
                        catIcon.SetActive(false);

                        for (int i = 0; i < 5; i++) {
                            if (mouthDictionary[testDialogue[nextDialogue].mouth] == i) {
                                allMouths[i].enabled = true;
                            } else {
                                allMouths[i].enabled = false;
                            }
                        }
                        for (int i = 0; i < 5; i++) {
                            if (eyeDictionary[testDialogue[nextDialogue].eyes] == i) {
                                allEyes[i].enabled = true;
                            } else {
                                allEyes[i].enabled = false;
                            }
                        }

                        //dialogue.alignment = TextAlignmentOptions.Left;
                        //dialogueNameTag.transform.localPosition             = new Vector3(-357f, -174f, 0f);
                        //dialogueCharacterNameObject.transform.localPosition = new Vector3(-357f, -174f, 0f);
                        //if (dialogueNameTag.transform.localRotation.z < 0) {
                        //    dialogueNameTag.transform.Rotate(0, 0, 2*6.777f);
                        //    dialogueCharacterNameObject.transform.Rotate(0, 0, 2*6.777f);
                        //}
                    } else {
                        bridgetIcon.SetActive(false);
                        catIcon.SetActive(true);                            
                        //dialogue.alignment = TextAlignmentOptions.Right;
                        //dialogueNameTag.transform.localPosition             = new Vector3(327f, -174f, 0f);
                        //dialogueCharacterNameObject.transform.localPosition = new Vector3(327f, -174f, 0f);
                        //if (dialogueNameTag.transform.localRotation.z > 0) {
                        //    dialogueNameTag.transform.Rotate(0, 0, 2*-6.777f);
                        //    dialogueCharacterNameObject.transform.Rotate(0, 0, 2*-6.777f);
                        //}
                    }
                    dialogueCharacterName.text = testDialogue[nextDialogue].characterTalking;
                    dialogue.text              = testDialogue[nextDialogue].text;
                    dialogueTimer              = testDialogue[nextDialogue].timeOnScreen; // @TODO: should some of this be moved to update instead?
                    nextDialogue               = testDialogue[nextDialogue].nextId;
                } else {
                    dialogueParent.SetActive(false);
                }
            }
        }


    }
}


public class Dialogue {
    
    public string characterTalking;
    public string text;
    public string mouth;
    public string eyes;
    public float timeOnScreen;
    public int id;
    public int nextId;

    public Dialogue(string _characterTalking, string _text, string _mouth, string _eyes, float _timeOnScreen, int _id, int _nextId) {
        characterTalking = _characterTalking;
        text             = _text;
        mouth            = _mouth;
        eyes             = _eyes;
        timeOnScreen     = _timeOnScreen;
        id               = _id; // @NOTE: isn't necessary in the current implementation, could be though in another, so lets keep it for now -Victor
        nextId           = _nextId;
    }

}