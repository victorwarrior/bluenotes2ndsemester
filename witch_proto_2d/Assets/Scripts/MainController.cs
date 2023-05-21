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
    public GameObject      dialogueNameTag;
    public GameObject      dialogueCharacterNameObject;
    public TextMeshProUGUI dialogueCharacterName;
    public TextMeshProUGUI dialogue;


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
            mistTimer = 200f;
        }
        
        int n = 0;

        testDialogue[n++] = new Dialogue("Bridget",
                                         "Hi, this is Bridget talking! This text is maybe a bit boring... Hmm...",
                                         4f, 0, 1);
        testDialogue[n++] = new Dialogue("CAT",
                                         "...well, THIS text is on the screen for a long time...",
                                         8f, 1, 2);
        testDialogue[n++] = new Dialogue("CAT",
                                         "short exclamation!",
                                         1.5f, 2, 3);
        testDialogue[n++] = new Dialogue("Bridget",
                                         "Right... i guess this is the last line of dialogue, maybe i should make it a bit longer than the other lines - to end it on a high note, you know?",
                                         6.2f, 3, -1);

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

        // mist
        if (mistTimer <= 0f) mistTimer = 0f; // @TODO: resetting doesn't work right now because it runs this all the time :) - Victor
        if (SceneManager.GetActiveScene().name == "MistTest" && mistTimer <= 0f) {

            if (Random.Range(0, 3) == 0) {
                int n = Random.Range(1, 3);
                for (int i = 0; i < n; i++) {
                    GameObject inst = Instantiate(simpleMistPrefab,
                                                  new Vector3(player.transform.position.x + Random.Range(-36f, 36f),
                                                              player.transform.position.y + Random.Range(-36f, 36f),
                                                              player.transform.position.z + 100),
                                                  Quaternion.identity);
                    inst.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);                    
                }
            }
 
            if (Random.Range(0, 100) >= 84) {
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

                    if (testDialogue[nextDialogue].characterTalking == "Bridget") {
                        dialogueNameTag.GetComponent<RectTransform>().localPosition             = new Vector3(-357f, -174f, 0f);
                        dialogueCharacterNameObject.GetComponent<RectTransform>().localPosition = new Vector3(-357f, -174f, 0f);
                        float zRotation = dialogueNameTag.GetComponent<RectTransform>().localRotation.z;
                        if (zRotation < 0) {
                            dialogueNameTag.GetComponent<RectTransform>().Rotate(0, 0, 2*6.777f);
                            dialogueCharacterNameObject.GetComponent<RectTransform>().Rotate(0, 0, 2*6.777f);
                        }
                    } else {
                        dialogueNameTag.GetComponent<RectTransform>().localPosition             = new Vector3(327f, -174f, 0f);
                        dialogueCharacterNameObject.GetComponent<RectTransform>().localPosition = new Vector3(327f, -174f, 0f);
                        float zRotation = dialogueNameTag.GetComponent<RectTransform>().localRotation.z;
                        if (zRotation > 0) {
                            dialogueNameTag.GetComponent<RectTransform>().Rotate(0, 0, 2*-6.777f);
                            dialogueCharacterNameObject.GetComponent<RectTransform>().Rotate(0, 0, 2*-6.777f);
                        }
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
    public float timeOnScreen;
    public int id;
    public int nextId;

    public Dialogue(string _characterTalking, string _text, float _timeOnScreen, int _id, int _nextId) {
        characterTalking = _characterTalking;
        text             = _text;
        timeOnScreen     = _timeOnScreen;
        id               = _id; // @NOTE: isn't necessary in the current implementation, could be though in another, so lets keep it for now -Victor
        nextId           = _nextId;
    }

}