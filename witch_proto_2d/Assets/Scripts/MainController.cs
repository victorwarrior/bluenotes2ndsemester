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

    public Dialogue[] introDialogue = new Dialogue[]
    {
         new Dialogue ("Bridget",
                        "...And you are sure this will help save my mother?",
                        "worried", "iffy", 3f, 0, 1), 

         new Dialogue ("Cat",
                        "Of courrse. You drruids are symbiotic with nature. Rrestoring this dead arrea to life will aid you both.",
                        "worried", "iffy", 5f, 1, 2),

         new Dialogue ("Bridget",
                        "I just don't understand why we can't go to her now. At least we can walk while we talk.",
                        "worried", "iffy", 4f, 2, 3),

         new Dialogue ("Cat",
                        "- Maybe we should slow down for a moment. Lay a plan. This place not only seems dead, it is as if all life has been forcibly sucked away from here.",
                        "worried", "iffy", 6f, 3, 4),

         new Dialogue ("Bridget",
                        "There is no time for that! For all we know, Mother could be dying right now! Besides, the death must have spread from somewhere. Finding the source is our best bet at solving this.",
                        "sad", "angry", 6f, 4, -1)


    };

    public Dialogue[] boxDialogue = new Dialogue[]
    {
         new Dialogue ("Bridget",
                        "Goddess, give me the strength to move these obstacles out of the way! *Hold down 'G' on the kyboard while moving to push the boxes.* ",
                        "relaxed", "wide", 6f, 0, -1)


    };
    public Dialogue[] enemyEncounterDialogue = new Dialogue[]
 {
         new Dialogue ("Cat",
                        "This is a lot of beasts. You should use the oxes to hide from them",
                        "worried", "iffy", 4f, 0, 1),

         new Dialogue ("Bridget",
                        "It might be easier just to run. *Hold down 'leftshift' to sprint.*",
                        "worried", "angry", 7f, 1, -1)

 };

    public Dialogue[] bridgeDialogue = new Dialogue[]
{
         new Dialogue ("Cat",
                        "This brridge seems extrremely frragile",
                        "worried", "iffy", 4f, 0, 1),

         new Dialogue ("Bridget",
                        "Don't worry, it won't break. You don't have to worry about getting wet.",
                        "relaxed", "angry", 5f, 1, -1)

};
    public Dialogue[] enterFarm = new Dialogue[]
{
         new Dialogue ("Cat",
                        "Wait! This smells-",
                        "worried", "iffy", 4f, 0, 1),

         new Dialogue ("Bridget",
                        "I have no time for your smells! This place was clearly inhabited once, and we need to know what happened. This is the fastest way.",
                        "excited", "angry", 6f, 1, -1)

};
    public Dialogue[] labyrinthDialogue = new Dialogue[]
{
         new Dialogue ("Cat",
                        "Seems like they barricaded themselves.",
                        "worried", "iffy", 3f, 0, 1),

         new Dialogue ("Bridget",
                        "Seems like it was pointless.",
                        "relaxed", "iffy", 3f, 1, -1)

};
    public Dialogue[] testPickupDialogue = new Dialogue[]
{
         new Dialogue ("Cat",
                        "Good. Now bring this to the beach!",
                        "worried", "iffy", 3f, 0, -1),


};
    public Dialogue[] beachDialogue = new Dialogue[]
{
         new Dialogue ("Cat",
                        "Congratulations tester! You won!",
                        "worried", "iffy", 10f, 0, -1),


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
                                                              Random.Range(-288.0f + 4f, -221.8f - 1f),
                                                              0),
                                                  Quaternion.identity);
                }
            }

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
                    
                    recapDialogue.text         = recapDialogue.text + "\n\n" + passedDialogue[nextDialogue].characterTalking + ":\n" + passedDialogue[nextDialogue].text;

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