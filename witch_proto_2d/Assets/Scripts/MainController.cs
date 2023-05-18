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
    public TextMeshProUGUI dialogueCharacterName;
    public TextMeshProUGUI dialogue;


    // constants
    public int numberOfEnemies = 0;

    // other
    bool mapOnScreen = false;
    float timer      = 0f;

    void Start() {
        if (SceneManager.GetActiveScene().name == "MistTest") {
            timer = 2f;
        }
        
        Dialogue[] testDialogue = {   new Dialogue("Bridget",
                                                   "Hi, this is Bridget talking! This text is maybe a bit boring... Hmm...",
                                                   4f, 1, 2),
                                      new Dialogue("CAT",
                                                   "...well, THIS text is on the screen for a long time...",
                                                   8f, 2, 3),
                                      new Dialogue("CAT",
                                                   "short exclamation!",
                                                   1f, 3, 4)   };

        if (dialogueCharacterName != null && dialogue != null) {
            dialogueCharacterName.text = testDialogue[0].characterTalking;
            dialogue.text              = testDialogue[0].text;            
        }

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

        timer -= Time.deltaTime;

        // spawning gray men and mist
        if (timer <= 0f) timer = 0f;
        if (SceneManager.GetActiveScene().name == "MistTest" && timer <= 0f) {

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
            if (timer <= -30f) {
                timer = Random.Range(30f, 120f);
                Debug.Log("timer set to " + timer);
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
        id               = _id;
        nextId           = _nextId;
    }

}