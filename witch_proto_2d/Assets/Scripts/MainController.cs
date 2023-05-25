using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainController : MonoBehaviour {
    
    // references
    public GameObject       player;
    public GameObject       enemyPrefab;
    public GameObject       graymanPrefab;
    public GameObject       simpleMistPrefab;
    public GameObject       mapImage;
 
    public GameObject       dialogueParent;
    //public GameObject       dialogueNameTag;
    //public GameObject       dialogueCharacterNameObject;
    public TextMeshProUGUI  dialogueCharacterName;
    public TextMeshProUGUI  dialogue;
 
    public GameObject       bridgetIcon;
    public GameObject       catIcon;
 
    public Image[]          allEyes   = new Image[5];
    public Image[]          allMouths = new Image[5];

    Dictionary<string, int> eyeDictionary = new Dictionary<string, int>()   {{"angry", 0}, {"happy", 1}, {"iffy", 2}, {"relaxed", 3}, {"wide", 4}    };
    Dictionary<string, int> mouthDictionary = new Dictionary<string, int>() {{"excited", 0}, {"happy", 1}, {"relaxed", 2}, {"sad", 3}, {"worried", 4}};

    public GameObject       recapDialogueParent;
    public TextMeshProUGUI  recapDialogue;


    // constants
    public const int numberOfEnemies = 0;

    // dialogue
    public Dialogue[] testDialogue = new Dialogue[] {
        new Dialogue("Bridget",
                     "Hi, this is Bridget talking! This text is maybe a bit boring... Ha ha ha!",
                     "happy", "relaxed", 4f, 0, 1),
        new Dialogue("CAT",
                     "...well, THIS text is on the screen for a long time...",
                     "happy", "happy", 8f, 1, 2),
        new Dialogue("CAT",
                     "short exclamation!",
                     "happy", "happy", 1.5f, 2, 3),
        new Dialogue("Bridget",
                     "Right... i guess this is the last line of dialogue, maybe i should make it a bit longer than the other lines - to end it on a high note, you know?",
                     "relaxed", "happy", 6.2f, 3, -1)
    };


    // other
    bool  mapOnScreen   = false;
    bool  recapOnScreen = false;
    float mistTimer     = 0f;
    float dialogueTimer = 0f;
    int   nextDialogue  = -1;
    Dialogue[] passedDialogue;


    void Start() {
        if (SceneManager.GetActiveScene().name == "Level design 1") {
            mistTimer = 1f;
        }
        
        if (dialogueParent != null) dialogueParent.SetActive(false);
        if (recapDialogueParent != null && recapDialogue != null) {
            recapDialogueParent.SetActive(false);
        }

        //StartDialogue(testDialogue, 0, 2f);

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

        if (Input.GetKeyDown("tab")) {
            recapOnScreen = !recapOnScreen;
            recapDialogueParent.SetActive(recapOnScreen); 
        }

        if (Input.GetKeyDown("j")) {
            StartDialogue(testDialogue, 2, 0f);
        }



    }

    void FixedUpdate() {


        if (mistTimer > 0f) mistTimer -= Time.deltaTime;

        // mist & graymen
        if (SceneManager.GetActiveScene().name == "Level design 1" && mistTimer <= 0f) {

            if (Random.Range(0, 100) < 30) {
                int n = 2;
                for (int i = 0; i < n; i++) {
                    GameObject inst = Instantiate(simpleMistPrefab,
                                                  new Vector3(Random.Range(-474.7f + 4f, -408.8f - 4f),
                                                              Random.Range(-274.6f + 4f, -208.3f - 1f),
                                                              0),
                                                  Quaternion.identity);
                }
            }
            
            /*
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
            }*/

        }

        // dialogue
        
        if (dialogueTimer >= 0f) {
            dialogueTimer = dialogueTimer - Time.deltaTime;

            if (dialogueTimer <= 0f && dialogueCharacterName != null && dialogue != null && dialogueParent != null) {
                if (nextDialogue != -1) {
                    dialogueParent.SetActive(true);
                    if (passedDialogue[nextDialogue].characterTalking == "Bridget" && bridgetIcon != null && catIcon != null) {
                        bridgetIcon.SetActive(true);
                        catIcon    .SetActive(false);
                        for (int i = 0; i < 5; i++) {
                            allMouths[i].enabled = (mouthDictionary[passedDialogue[nextDialogue].mouth] == i) ? true : false;
                        }
                        for (int i = 0; i < 5; i++) {
                            allEyes[i].enabled = (eyeDictionary[passedDialogue[nextDialogue].eyes] == i) ? true : false;
                        }
                    } else {
                        bridgetIcon.SetActive(false);
                        catIcon    .SetActive(true);
                    }
                    dialogueCharacterName.text = passedDialogue[nextDialogue].characterTalking;
                    dialogue.text              = passedDialogue[nextDialogue].text;
                    dialogueTimer              = passedDialogue[nextDialogue].timeOnScreen; // @TODO: should some of this be moved to update instead?
                    
                    recapDialogue.text         = recapDialogue.text + "\n\n" + passedDialogue[nextDialogue].characterTalking + ":\n" + testDialogue[nextDialogue].text;

                    nextDialogue               = passedDialogue[nextDialogue].nextId;

                } else {
                    dialogueParent.SetActive(false);
                }
            }
        }


    }
    

    public void StartDialogue(Dialogue[] _dialogue, int _start, float _waitTime) {
        passedDialogue = _dialogue;
        nextDialogue   = _start;
        dialogueTimer  = _waitTime;

        if (recapDialogue != null) recapDialogue.text = "";
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