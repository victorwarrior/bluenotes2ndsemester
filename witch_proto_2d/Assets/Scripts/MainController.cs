using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {
    
    // references
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject graymanPrefab;
    public GameObject simpleMistPrefab;
    public GameObject mapCanvas;

    // constants
    public int numberOfEnemies = 48;

    // other
    bool mapOnScreen = false;

    void Start() {
        //for (int i = 0; i < numberOfEnemies; i++) {
        //    GameObject inst = Instantiate(enemyPrefab); // right now it figures out a random position on its own
        //    inst.GetComponent<Enemy>().player = player;            
        //}
    }

    void Update() {
        if (Input.GetKeyDown("m")) {
            mapOnScreen = !mapOnScreen;
            mapCanvas.SetActive(mapOnScreen);
        }
        
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
