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
    public int numberOfEnemies = 0;

    // other
    bool mapOnScreen = false;
    float timer      = 0f;

    void Start() {
        if (SceneManager.GetActiveScene().name == "MistTest") {
            timer = 10f;
        }
        
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

    void FixedUpdate() {

        timer -= Time.deltaTime;
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
            /*mistTimer -= Time.deltaTime;
            if (mistTimer <= 0) {

                GameObject inst = Instantiate(simpleMistPrefab,
                                              new Vector3(player.transform.position.x + Random.Range(-36f, 36f),
                                                          player.transform.position.y + Random.Range(-36f, 36f),
                                                          player.transform.position.z + 100),
                                              Quaternion.identity);
                inst.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0f);

                mistTimer = Random.Range(0.02f, 0.06f);

            }*/
            if (timer <= -90f) timer = Random.Range(30f, 120f);
        }
    }
}
